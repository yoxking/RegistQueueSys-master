using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace EntFrm.AutoUpdate.Service
{
    class UpdateService : IUpdateService
    {
        private string parentPath;
        private string targetFiles;
        private string programName;
        private string ignoreFiles;
        private LocalVersion localVersion;
        private ServVersion servVersion;
        private string versionCode;

        private static readonly string VersionTempsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdateTemps");
        private static readonly string VersionBakupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdateBakup");

        
        public event EventHandler UpdateStarted;
        public event EventHandler<UpdateProgressArgs> UpdateProgressChanged;
        public event EventHandler<UpdateEndedArgs> UpdateEnded;


        public UpdateService()
        { 
            this.targetFiles = AppDomain.CurrentDomain.BaseDirectory; 
            this.parentPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

            GetProgramName();
            GetIgnoreFiles();
        } 

        public bool DetectVersion()
        {
            GetLocalVersion();
            GetServVersion();

            if (localVersion != null && servVersion != null)
            { 
                return true;
            }

            return false;
        }

        public string GetVersionCode()
        {
            return this.versionCode;
        }

        public bool TryUpdateNow()
        { 
            this.RaiseUpdateStarted();

            try
            {
                //结束主程序进程
                Process[] processes = Process.GetProcessesByName(programName);
                foreach (Process p in processes)
                {
                    p.Kill();
                    p.Close();
                }

                this.RaiseUpdateProgress(0.1f); 
            }
            catch (Exception exc)
            {
                string msg = "结束进程时出错!";
                Exception nexc = new CreateVersionInfoException(msg, exc);
                this.RaiseUpdateEnded(msg, nexc);
                throw nexc;
            }

            try
            {
                //创建备份目录信息
                DirectoryInfo bakupinfo = new DirectoryInfo(VersionBakupFolder);
                if (bakupinfo.Exists == false)
                {
                    bakupinfo.Create();
                }
                //创建临时目录信息
                DirectoryInfo tempsinfo = new DirectoryInfo(VersionTempsFolder);
                if (tempsinfo.Exists == false)
                {
                    tempsinfo.Create();
                }

                this.RaiseUpdateProgress(0.2f); 
            }
            catch (Exception exc)
            {
                string msg = "创建临时文件夹时出错!";
                Exception nexc = new CreateVersionInfoException(msg, exc);
                this.RaiseUpdateEnded(msg, nexc);
                throw nexc;
            }

            string fileName = servVersion.FileUrl.Substring(servVersion.FileUrl.LastIndexOf("/") + 1);
            try
            {  

                if(HttpDwload.DownloadFile(servVersion.FileUrl, VersionTempsFolder+"\\" + fileName))
                {
                    this.RaiseUpdateProgress(0.5f); 
                }
                else
                {
                    throw new Exception("下载文件时出错");
                }
            }
            catch (Exception exc)
            {
                string msg = string.Format("下载新版文件出错!");
                Exception nexc = new DownFileException(msg, exc);
                this.RaiseUpdateEnded(msg, nexc);
                throw nexc;
            }

            //备份当前版本，占比 3%
            try
            {
                CopyDirectory(this.parentPath, VersionBakupFolder);
                this.RaiseUpdateProgress(0.7f); 
            }
            catch (Exception exc)
            {
                string msg = "版本当前版本出错";
                Exception nexc = new BackupFileException(msg, exc);
                this.RaiseUpdateEnded(msg, nexc);
                throw nexc;
            }

            //新版覆盖当前版，占比 4%
            try
            {
                string msg = "";
                SharpzipUtils zipUtil = new SharpzipUtils();
                if (zipUtil.UnZipFile(VersionTempsFolder+"\\" + fileName, this.parentPath, out msg))
                {
                    this.RaiseUpdateProgress(0.8f); 
                }
            }
            catch (Exception exc)
            {
                //恢复备份文件
                CopyDirectory(VersionBakupFolder,this.parentPath);
            }

            //删除临时文件，占比 1%
            try
            {
                Directory.Delete(VersionTempsFolder, true);
                Directory.Delete(VersionBakupFolder, true);
                this.RaiseUpdateProgress(0.9f);
                Thread.Sleep(1000);
            }
            catch (Exception exc)
            {
                string msg = string.Format("删除临时文件出错");
                Exception nexc = new DeleteTempFileException(msg, exc);
                UpdateLogUtil.Log(nexc, "升级忽略错误");
            } 
            this.RaiseUpdateProgress(1f);
            this.RaiseUpdateEnded();

            return true;
        } 

        #region 

        /// <summary>
        /// 获取更新的服务器端的数据信息
        /// </summary>
        /// <returns></returns>
        private void GetLocalVersion()
        {
            localVersion = new LocalVersion();
            localVersion.VerType = IPublicHelper.Get_ConfigValue("VerType");
            localVersion.VerCode = IPublicHelper.Get_ConfigValue("VerCode");
        }

        /// <summary>
        /// 获取更新的服务器端的数据信息
        /// </summary>
        /// <returns></returns>
        private void GetServVersion()
        {
            string s = IUserContext.OnExecuteCommand_Xp("getVersionInfo", new string[] { localVersion.VerType, localVersion.VerCode });
            if (!string.IsNullOrEmpty(s))
            {
                servVersion = JsonConvert.DeserializeObject<ServVersion>(s);
                this.versionCode = servVersion.VerCode;
            }
            else
            {
                servVersion = null;
            }
        }

        /// <summary>
        /// 获取更新程序名称
        /// </summary>
        /// <returns></returns>
        private void GetProgramName()
        {
            programName = IPublicHelper.Get_ConfigValue("AppName");
        }

        /// <summary>
        /// 获取不更新文件名称
        /// </summary>
        /// <returns></returns>
        private void GetIgnoreFiles()
        {
            ignoreFiles = IPublicHelper.Get_ConfigValue("Ignores");
        }
        
        /// <summary>
        /// 复制文件夹（及文件夹下所有子文件夹和文件） 
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="targetPath">目标路径</param>
        private void CopyDirectory(String sourcePath, String targetPath)
        {
            String destName = "";
            DirectoryInfo info = new DirectoryInfo(sourcePath);

            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                if (ignoreFiles.IndexOf(fsi.Name + ";") > -1)
                    continue;

                destName = Path.Combine(targetPath, fsi.Name);

                if (fsi is System.IO.FileInfo)
                {
                    File.Copy(fsi.FullName, destName, true);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }
        
        #endregion

        private void RaiseUpdateStarted()
        { 
            EventHandler handler = this.UpdateStarted;
            if (handler != null)
            {
                handler.BeginInvoke(this, EventArgs.Empty, null, null);
            }
        }
        private void RaiseUpdateEnded()
        { 
            EventHandler<UpdateEndedArgs> handler = this.UpdateEnded;
            if (handler != null)
            {
                handler.BeginInvoke(this, new UpdateEndedArgs(), null, null);
            }
        }
        private void RaiseUpdateEnded(string errorMessage, Exception errorException)
        {
            EventHandler<UpdateEndedArgs> handler = this.UpdateEnded;
            if (handler != null)
            {
                handler.BeginInvoke(this, new UpdateEndedArgs(errorMessage, errorException), null, null);
            }
        }
        private void RaiseUpdateProgress(float percent)
        {
            this.UpdateProgressChanged(this, new UpdateProgressArgs() { ProgressPercent = percent });
            //EventHandler<UpdateProgressArgs> handler = this.UpdateProgressChanged;
            //if (handler != null)
            //{
            //    handler.BeginInvoke(this, new UpdateProgressArgs() { ProgressPercent = percent }, null, null);
            //}
        }
    }
}