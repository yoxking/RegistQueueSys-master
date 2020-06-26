using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.TicketConsole.MPublicUtils
{

    /// <summary>
    /// 文件操作处理
    /// </summary>
    public class FileProcessor
    {

        #region 文件夹操作

        /// <summary>
        /// 删除路径末尾多余的斜杠
        /// </summary>
        /// <param name="path"></param>
        public static void RemovePathRightRepeatSlash(ref String path)
        {
            //2014-07-08 zhengjf 删除末尾多余的 
            for (Int32 charidx = path.Length - 1; charidx > 0; --charidx)
            {
                if (path[charidx] == '\\')
                {
                    continue;
                }
                else
                {
                    //从右往左找到第一个不是斜杠的字符,把之后字符删除
                    path = path.Substring(0, charidx + 1);
                    break;
                }
            }
            path = path + "\\";
        }

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="filename">文件名,带路径</param>
        /// <returns></returns>
        public static Boolean DirectoryExist(String directname)
        {
            return Directory.Exists(directname);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="filename">文件名,带路径</param>
        /// <returns></returns>
        public static Boolean CreateDirectory(String directname)
        {
            if (Directory.Exists(directname))
            {
                return true;
            }
            Directory.CreateDirectory(directname);
            return Directory.Exists(directname);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="filename">文件名,带路径</param>
        /// <param name="recursive">若要移除 path 中的目录、子目录和文件，则为 true；否则为 false。</param>
        /// <returns></returns>
        public static Boolean DeleteDirectory(String directname)
        {
            if (!Directory.Exists(directname))
            {
                return true;
            }

            //把所有子文件夹及文件夹设置为可写
            SetDirectoryAndFilesReadOnly(directname, false);

            try
            {
                Directory.Delete(directname, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteDirectory error:" + ex.Message);
                return false;
            }

            Boolean bl = Directory.Exists(directname);
            return !bl;
        }

        /// <summary>
        /// 获取文件夹是否为只读
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static Boolean GetDirectoryReadOnly(String dirpath)
        {
            if (!DirectoryExist(dirpath))
            {
                return false;
            }

            DirectoryInfo DirInfo = new DirectoryInfo(dirpath);

            Int32 attrval = (Int32)DirInfo.Attributes;
            Int32 readonlyval = (Int32)FileAttributes.ReadOnly;
            if ((attrval & readonlyval) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 设置文件夹是否为只读
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static Boolean SetDirectoryReadOnly(String dirpath, Boolean isreadonly)
        {
            if (!DirectoryExist(dirpath))
            {
                return false;
            }

            DirectoryInfo DirInfo = new DirectoryInfo(dirpath);

            try
            {
                if (isreadonly)
                {
                    DirInfo.Attributes = (FileAttributes)((Int32)DirInfo.Attributes | (Int32)FileAttributes.ReadOnly);
                }
                else
                {
                    DirInfo.Attributes = (FileAttributes)((Int32)FileAttributes.Normal | (Int32)FileAttributes.Directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetDirectoryReadOnly error:" + ex.Message);
                return false;
            }

            //核对是否设置成功
            Boolean newattr = GetDirectoryReadOnly(dirpath);
            if (isreadonly == newattr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置文件夹以及文件夹里所有子文件夹和文件是否为只读
        /// </summary>
        /// <param name="directname">文件夹名</param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static void SetDirectoryAndFilesReadOnly(String directname, Boolean isreadonly)
        {
            //把所有子文件设置为可写
            List<String> strlist = GetAllFileNameInDirectoryRecursive(directname);
            if (strlist != null && strlist.Count > 0)
            {
                foreach (String filename in strlist)
                {
                    SetFileReadOnly(filename, isreadonly);
                }
            }

            //把所有子文件夹设置为可写
            strlist = GetAllChildFolderNameRecursive(directname);
            if (strlist != null && strlist.Count > 0)
            {
                foreach (String folder in strlist)
                {
                    SetDirectoryReadOnly(folder, isreadonly);
                }
            }
        }

        /// <summary>
        /// 获取指定文件夹下,所有文件的文件名
        /// </summary>
        /// <param name="directoryname"></param>
        /// <param name="filenamelist"></param>
        /// <returns></returns>
        public static List<String> GetFileNameInDirectory(String directoryname)
        {
            bool bl = Directory.Exists(directoryname);
            if (!bl)
            {
                return null;
            }

            String[] filenameary = Directory.GetFiles(directoryname);
            if (filenameary == null || filenameary.Length < 1)
            {
                return null;
            }

            List<String> ret = new List<String>();
            ret.AddRange(filenameary);
            return ret;
        }


        /// <summary>
        /// 获取指定文件夹及其所有子目录下,所有文件的文件名
        /// </summary>
        /// <param name="directoryname"></param>
        /// <param name="filenamelist"></param>
        /// <returns></returns>
        public static List<String> GetAllFileNameInDirectoryRecursive(String directoryname)
        {
            bool bl = DirectoryExist(directoryname);
            if (!bl)
            {
                return null;
            }

            List<String> ret = new List<String>();

            //先把本文件夹下的所有 文件 添加到结果列表
            ret = GetFileNameInDirectory(directoryname);
            if (ret == null)
            {
                ret = new List<string>();
            }
            string[] directories = Directory.GetDirectories(directoryname);
            //不含子文件夹,直接返回当前结果
            if (directories == null || directories.Length < 1)
            {
                return ret;
            }

            //包含子文件夹，递归读取子文件夹中的内容

            //当前子文件夹里的所有文件内容
            List<String> listincurdir = null;
            foreach (string suffix in directories)
            {
                listincurdir = GetAllFileNameInDirectoryRecursive(suffix);
                //添加到最终结果列表里
                ret.AddRange(listincurdir);
            }

            return ret;
        }


        /// <summary>
        /// 获取指定文件夹下所有子目录名
        /// </summary>
        /// <param name="directoryname"></param>
        /// <param name="filenamelist"></param>
        /// <returns></returns>
        public static List<String> GetAllChildFolderNameRecursive(String directoryname)
        {
            bool bl = DirectoryExist(directoryname);
            if (!bl)
            {
                return null;
            }

            List<String> ret = new List<String>();

            string[] directories = Directory.GetDirectories(directoryname);
            //不含子文件夹,直接返回当前结果
            if (directories == null || directories.Length < 1)
            {
                return ret;
            }

            //包含子文件夹，递归读取每个子文件夹中的文件夹
            ret.AddRange(directories);

            List<String> folderlist = null;
            foreach (string suffix in directories)
            {
                folderlist = GetAllChildFolderNameRecursive(suffix);
                ret.AddRange(folderlist);
            }

            return ret;
        }

        /// <summary>
        /// 获取指定文件夹下,指定后缀名的文件名
        /// </summary>
        /// <param name="directoryname"></param>
        /// <param name="suffix">后缀名,不含点号.如"jpg"</param>
        /// <returns></returns>
        public static List<String> GetFileNameInDirectory(String directoryname, String suffix)
        {
            bool bl = Directory.Exists(directoryname);
            if (!bl)
            {
                return null;
            }

            suffix = suffix.Replace("*", "");
            suffix = suffix.Replace(".", "");
            if (suffix.Length > 0)
            {
                suffix = "*." + suffix;
            }

            String[] filenameary = null;
            if (suffix.Length > 2)
            {
                filenameary = Directory.GetFiles(directoryname, suffix);
            }
            else
            {
                filenameary = Directory.GetFiles(directoryname);
            }
            if (filenameary == null || filenameary.Length < 1)
            {
                return null;
            }

            List<String> ret = new List<String>();
            ret.AddRange(filenameary);
            return ret;
        }

        /// <summary>
        /// 复制整个文件夹中的内容覆盖到另一个文件夹中
        /// </summary>
        /// <param name="srcDirectory"></param>
        /// <param name="destDirectory"></param>
        public static Boolean CopyAllDirecotryFiles(string srcDirectory, string destDirectory)
        {

            //如果最后一个字符不是斜杠,加上斜杠
            if (destDirectory[destDirectory.Length - 1] != '\\')
            {
                destDirectory = destDirectory + "\\";
            }

            if (srcDirectory.Equals(destDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //当目标文件夹不存在时，新建文件夹
            if (!DirectoryExist(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }

            //目标文件夹名和起始目标文件名不存在，直接返回
            if (!DirectoryExist(srcDirectory) || !DirectoryExist(destDirectory))
            {
                return false;
            }

            //-----------------------------------------------------
            //先把当前目录下的所有文件复制到目标目录
            string[] filenamelist = Directory.GetFiles(srcDirectory);
            Boolean bl = false;
            //如果最后一个字符是斜杠,去掉斜杠.以便替换路径，生成目标文件名
            if (destDirectory[destDirectory.Length - 1] == '\\')
            {
                destDirectory = destDirectory.Substring(0, destDirectory.Length - 1);
            }
            String destfilename = String.Empty;
            if (filenamelist.Length > 0)
            {
                foreach (string srcfilename in filenamelist)
                {
                    destfilename = destDirectory + srcfilename.Substring(srcfilename.LastIndexOf("\\"));
                    bl = CopyFile(srcfilename, destfilename);
                }
            }

            //-----------------------------------------
            //再把每个子目录下的文件复制到目标路径
            string[] directories = Directory.GetDirectories(srcDirectory);
            if (directories == null || directories.Length < 1)
            {
                return true;
            }

            String destchilddir = String.Empty;
            foreach (string srcchilddir in directories)
            {
                destchilddir = destDirectory + srcchilddir.Substring(srcchilddir.LastIndexOf("\\"));
                CopyAllDirecotryFiles(srcchilddir, destchilddir);
            }

            return true;
        }

        /// <summary>
        /// 替换文件名中的路径名
        /// </summary>
        /// <param name="filename">修改修改的文件名</param>
        /// <param name="oldpath">原始路径</param>
        /// <param name="newpath">新路径</param>
        /// <returns></returns>
        public static String ReplaceFileName(String filename, String oldpath, String newpath)
        {
            //如果最后一个字符不是斜杠,添加斜杠
            if (oldpath[oldpath.Length - 1] != '\\')
            {
                oldpath += "\\";
            }

            //如果最后一个字符不是斜杠,添加斜杠
            if (newpath[newpath.Length - 1] != '\\')
            {
                newpath += "\\";
            }
            String newfilename = "";
            newfilename = filename.Replace(oldpath, newpath);
            return newfilename;
        }
        #endregion


        #region 文件操作(非文件内容)

        /// <summary>
        /// 获取指定文件的版本号，如果文件不存在，或则版本号为空，返回false
        /// </summary>
        /// <param name="filename">带路径的文件名</param>
        /// <param name="fileversion">文件的版本号</param>
        /// <returns></returns>
        public static Boolean GetFileVersion(String filename, ref String fileversion)
        {
            fileversion = "";
            if (!FileExist(filename))
            {
                return false;
            }
            FileVersionInfo versioninfo = FileVersionInfo.GetVersionInfo(filename);
            fileversion = versioninfo.FileVersion;
            if (fileversion.Length < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取文件是否为只读
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static Boolean GetFileReadOnly(String filename)
        {
            if (!FileExist(filename))
            {
                return false;
            }

            FileAttributes fileattr = File.GetAttributes(filename);
            Int32 attrval = (Int32)fileattr;

            Int32 readonlyval = (Int32)FileAttributes.ReadOnly;
            if ((attrval & readonlyval) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 设置文件是否为只读
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isreadonly"></param>
        /// <returns></returns>
        public static Boolean SetFileReadOnly(String filename, Boolean isreadonly)
        {
            if (!FileExist(filename))
            {
                return false;
            }

            try
            {
                if (isreadonly)
                {
                    File.SetAttributes(filename, FileAttributes.ReadOnly);
                }
                else
                {
                    File.SetAttributes(filename, FileAttributes.Normal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetFileReadOnly Error:" + ex.Message);
                return false;
            }


            //核对是否设置成功
            Boolean newattr = GetFileReadOnly(filename);
            if (isreadonly == newattr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找指定文件夹下包含通配符的文件名
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="matchname"></param>
        /// <returns></returns>
        public static List<String> FindFiles(String directory, String matchname)
        {
            List<String> filenamelist = new List<string>();
            Boolean bl = DirectoryExist(directory);
            if (!bl)
            {
                return filenamelist;
            }
            String[] filenameary = null;
            try
            {
                filenameary = Directory.GetFiles(directory, matchname);
            }
            catch (System.Exception ex)
            {
                Console.Write("FindFiles error:" + ex.Message);
                return null;
            }
            if (filenameary != null && filenameary.Length > 0)
            {
                filenamelist.AddRange(filenameary);
            }

            return filenamelist;
        }

        /// <summary>
        /// 删除指定文件夹下包含通配符的文件
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="matchname"></param>
        /// <returns></returns>
        public static Int32 DeleteFiles(String directory, String matchname)
        {
            List<String> filelist = FindFiles(directory, matchname);
            if (filelist == null || filelist.Count < 1)
            {
                return 0;
            }

            Int32 ret = 0;
            Boolean bl = false;
            foreach (string filename in filelist)
            {
                bl = DeleteFile(filename);
                if (bl)
                {
                    ++ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filename">文件名,带路径</param>
        /// <returns></returns>
        public static Boolean FileExist(String filename)
        {
            return File.Exists(filename);
        }

        #region  判断文件是否已经被打开

        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int OF_READWRITE = 2;
        private const int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 判断文件是否已经被其他程序或系统用户打开
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>true-文件已经被打开;false-文件不存在或未被打开</returns>
        public static Boolean FileIsOpened(String filename)
        {
            if (!FileExist(filename))
            {
                return false;
            }

            IntPtr handle = _lopen(filename, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (handle == HFILE_ERROR)
            {
                return true;
            }

            CloseHandle(handle);
            return false;
        }

        #endregion


        /// <summary>
        /// 获取 该文件的最后修改日期
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean GetFileEditTime(String filename, ref DateTime edittime)
        {
            Boolean bl = FileProcessor.FileExist(filename);
            if (!bl)
            {
                return false;
            }

            FileInfo fi = new FileInfo(filename);
            //fi.CreationTime 
            //fi.LastAccessTime 
            //重新创建对象,忽略毫秒部分
            edittime = new DateTime(fi.LastWriteTime.Year, fi.LastWriteTime.Month, fi.LastWriteTime.Day,
                fi.LastWriteTime.Hour, fi.LastWriteTime.Minute, fi.LastWriteTime.Second);
            return true;
        }

        /// <summary>
        /// 获取文件长度
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>文件不存在,返回-1.存在,返回文件长度</returns>
        public static Int32 GetFileLength(String filename)
        {
            Boolean blret = File.Exists(filename);
            if (!blret)
            {
                return -1;
            }

            FileInfo fi = new FileInfo(filename);
            Int32 ret = (Int32)fi.Length;
            return ret;
        }

        /// <summary>
        /// 无效时间
        /// </summary>
        public static DateTime Const_InvalidDate = new DateTime(2000, 1, 1);

        /// <summary>
        /// 获取 该文件的最后修改日期
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DateTime GetLatestEditTime(String filename)
        {
            Boolean bl = FileProcessor.FileExist(filename);
            if (!bl)
            {
                return Const_InvalidDate;
            }

            FileInfo fi = new FileInfo(filename);
            //fi.CreationTime 
            //fi.LastAccessTime 
            DateTime ret = new DateTime(fi.LastWriteTime.Year, fi.LastWriteTime.Month, fi.LastWriteTime.Day,
                fi.LastWriteTime.Hour, fi.LastWriteTime.Minute, fi.LastWriteTime.Second);
            return ret;
        }
        /// <summary>
        /// 从文件名里,获取后缀名 
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>后缀名,不带点号</returns>
        public static String GetFileSuffixName(String filename)
        {
            if (filename.Length < 1)
                return String.Empty;

            Int32 index_lastpoint = filename.LastIndexOf('.');

            if ((index_lastpoint < 0) || (index_lastpoint >= filename.Length))
                return String.Empty;

            String ret = filename.Substring(index_lastpoint + 1);
            return ret;
        }

        /// <summary>
        /// 从带路径的文件名里,获取路径名 
        /// </summary>
        /// <param name="pathfilename">带路径的文件名</param>
        /// <returns>不带路径,不带后缀名的纯文件名</returns>
        public static String GetDirectoryName(String pathfilename)
        {
            if (pathfilename.Length < 1)
            {
                return String.Empty;
            }

            Int32 index_lastbias = pathfilename.LastIndexOf('\\');
            String dir = pathfilename.Substring(0, index_lastbias);
            dir = dir + "\\";//2014-09-09 zhengjf
            return dir;
        }

        /// <summary>
        /// 获取文件路径所在的磁盘
        /// </summary>
        /// <param name="pathfilename"></param>
        /// <param name="dirvename"></param>
        /// <returns></returns>
        public static Boolean GetDriveName(String pathfilename, ref String dirvename)
        {
            Int32 firstindex = pathfilename.IndexOf(':');
            if (firstindex < 0 || firstindex > pathfilename.Length)
            {
                return false;
            }
            dirvename = pathfilename.Substring(0, firstindex + 1);
            return DirectoryExist(dirvename);
        }
        /// <summary>
        /// 获取当前文件夹的上一级文件夹
        /// </summary>
        /// <param name="curpath">当前文件夹</param>
        /// <returns>上一级文件夹</returns>
        public static String GetParentDirectoryName(String curpath)
        {
            //至少是 c:\a
            if (curpath.Length < 4)
            {
                return String.Empty;
            }

            //判断是否为文件名
            Boolean bl = FileExist(curpath);
            if (bl)
            {
                curpath = GetDirectoryName(curpath);
            }

            //如果最后一个字符是斜杠,去掉斜杠
            if (curpath[curpath.Length - 1] == '\\')
            {
                curpath = curpath.Substring(0, curpath.Length - 1);
            }

            Int32 index_lastbias = curpath.LastIndexOf('\\');
            // c:\a
            String parentdir = curpath.Substring(0, index_lastbias + 1);
            if (parentdir[parentdir.Length - 1] != '\\')
            {
                parentdir = parentdir + "\\";
            }
            return parentdir;
        }

        /// <summary>
        /// 从带路径的文件名里,获取带后缀名的纯文件名 
        /// </summary>
        /// <param name="pathfilename">带路径的文件名</param>
        /// <returns>不带路径,带后缀名的纯文件名</returns>
        public static String GetPureFileNameWithSuffix(String pathfilename)
        {
            if (pathfilename.Length < 1)
            {
                return String.Empty;
            }
            Int32 index_lastbias = pathfilename.LastIndexOf('\\');
            if (index_lastbias < 0) //不存在\\,说明不含路径
            {
                index_lastbias = -1;
            }

            if (index_lastbias >= pathfilename.Length)
            {
                return String.Empty;
            }

            String ret = pathfilename.Substring(index_lastbias + 1);
            return ret;

        }

        /// <summary>
        /// 从带路径的文件名里,获取不带后缀名的文件名 
        /// 举例: C:\dirA\fileA.txt 得到 fileA
        /// </summary>
        /// <param name="pathfilename">带路径的文件名</param>
        /// <returns>不带路径,不带后缀名的纯文件名</returns>
        public static String GetPureFileNameNoSuffix(String pathfilename)
        {
            if (pathfilename.Length < 1)
            {
                return String.Empty;
            }

            Int32 index_lastbias = pathfilename.LastIndexOf('\\');
            Int32 index_lastpoint = pathfilename.LastIndexOf('.');

            if (index_lastbias < 0) //不存在\\,说明不含路径
            {
                index_lastbias = -1;
            }

            if (index_lastpoint < 0)//不存在. 说明不含后缀名
            {
                index_lastpoint = pathfilename.Length - 1;
            }
            if (index_lastbias >= index_lastpoint)
            {
                return String.Empty;
            }

            Int32 pfnlen = index_lastpoint - index_lastbias - 1; //纯文件名长度
            if (pfnlen < 1)
            {
                return String.Empty;
            }

            String ret = pathfilename.Substring(index_lastbias + 1, pfnlen);
            return ret;
        }

        /// <summary>
        /// 获取 文件后缀名 
        /// 举例: C:\dirA\fileA.txt 得到 txt
        /// </summary>
        /// <param name="filename">带后缀名的文件名</param>
        /// <returns>后缀名</returns>
        public static String GetSuffixName(String filename)
        {
            if (filename.Length < 1)
            {
                return String.Empty;
            }

            Int32 index_lastpoint = filename.LastIndexOf('.');

            if (index_lastpoint < 0)
            {
                return String.Empty;
            }

            //0       8 
            //C:\fileA.txt 
            //start = 8+1, len = 12 - 8 - 1
            String suffixname = filename.Substring(index_lastpoint + 1, filename.Length - index_lastpoint - 1);
            return suffixname;
        }
        /// <summary>
        /// 选择1个文件
        /// </summary>
        /// <param name="title">选择窗口的标题</param>
        /// <param name="defaultpath"></param>
        /// <param name="filter">Image Files(*.bmp;*.jpg;*.gif;*.png;*.ico)|*.bmp;*.jpg;*.gif;*.png;*.ico|All files (*.*)|*.* </param>
        /// <param name="filename">选中的文件名</param>
        /// <returns></returns>
        public static Boolean SelectFile(IWin32Window win, String title, String defaultpath, String filter, out String filename)
        {
            //Text files (*.txt)|*.txt|All files (*.*)|*.*
            //Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*

            filename = String.Empty;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Title = title;
            dlg.InitialDirectory = defaultpath;
            dlg.Filter = filter;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;
            dlg.FilterIndex = 0;
            DialogResult ret = dlg.ShowDialog(win);
            if (ret != DialogResult.OK)
            {
                return false;
            }

            filename = dlg.FileName;
            return true;
        }

        /// <summary>
        /// 选择1个文件
        /// </summary>
        /// <param name="title">选择窗口的标题</param>
        /// <param name="defaultpath"></param>
        /// <param name="filter">Image Files(*.bmp;*.jpg;*.gif;*.png;*.ico)|*.bmp;*.jpg;*.gif;*.png;*.ico|All files (*.*)|*.* </param>
        /// <param name="filename">选中的文件名</param>
        /// <returns></returns>
        public static Boolean SelectFile(String title, String defaultpath, String filter, out String filename)
        {
            filename = String.Empty;
            Boolean bl = SelectFile(null, title, defaultpath, filter, out filename);
            return bl;
        }

        /// <summary>
        /// 选择多个文件
        /// </summary>
        /// <param name="win">父窗口</param>
        /// <param name="title">选择窗口的标题</param>
        /// <param name="defaultpath">默认路径</param>
        /// <param name="filter">Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* </param>
        /// <param name="filename">选中的文件名称数组</param>
        /// <returns></returns>
        public static Boolean SelectFiles(IWin32Window win, String title, String defaultpath, String filter, ref List<String> filenames)
        {
            filenames = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Title = title;
            dlg.Filter = filter;
            dlg.InitialDirectory = defaultpath;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = true;
            dlg.FilterIndex = 0;
            DialogResult ret = dlg.ShowDialog(win);
            if (ret != DialogResult.OK)
            {
                return false;
            }

            if (dlg.FileNames == null || dlg.FileNames.Length < 1)
            {
                return false;
            }

            filenames = new List<string>(dlg.FileNames);
            return true;
        }

        /// <summary>
        /// 选择多个文件
        /// </summary>
        /// <param name="title">选择窗口的标题</param>
        /// <param name="defaultpath">默认路径</param>
        /// <param name="filter">Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* </param>
        /// <param name="filename">选中的文件名称数组</param>
        /// <returns></returns>
        public static Boolean SelectFiles(String title, String defaultpath, String filter, out String[] filenames)
        {
            filenames = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Title = title;
            dlg.Filter = filter;
            dlg.InitialDirectory = defaultpath;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = true;
            dlg.FilterIndex = 0;
            DialogResult ret = dlg.ShowDialog();
            if (ret != DialogResult.OK)
            {
                return false;
            }

            filenames = dlg.FileNames;
            return true;
        }

        /// <summary>
        /// 选择一个文件夹
        /// </summary>
        /// <param name="title">选择窗口的标题</param>
        /// <param name="defaultpath">默认路径</param>
        /// <param name="directory">选中的文件夹</param>
        /// <returns></returns>
        public static Boolean SelectDirectory(String title, String defaultpath, out String directory)
        {
            directory = defaultpath;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = title;
            dlg.SelectedPath = defaultpath;
            dlg.ShowNewFolderButton = true;
            DialogResult ret = dlg.ShowDialog();
            if (ret != DialogResult.OK)
            {
                return false;
            }

            directory = dlg.SelectedPath;
            return true;
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="win">父窗体</param>
        /// <param name="title">窗体标题</param>
        /// <param name="defaultpath"></param>
        /// <param name="filter">Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* </param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean GetSaveFileName(IWin32Window win, String title, String defaultpath, String filter, out String filename)
        {
            //Text files (*.txt)|*.txt|All files (*.*)|*.*
            //Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*
            filename = String.Empty;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = title;
            dlg.Filter = filter;
            dlg.InitialDirectory = defaultpath;
            dlg.RestoreDirectory = true;
            dlg.FilterIndex = 0;
            DialogResult ret = dlg.ShowDialog(win);
            if (ret != DialogResult.OK)
            {
                return false;
            }

            filename = dlg.FileName;
            return true;
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="defaultpath"></param>
        /// <param name="filter">Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* </param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean GetSaveFileName(String title, String defaultpath, String filter, out String filename)
        {
            filename = String.Empty;
            Boolean bl = GetSaveFileName(null, title, defaultpath, filter, out filename);
            return bl;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean DeleteFile(String filename)
        {
            Boolean bl = FileExist(filename);
            if (!bl)
            {
                return true;
            }

            SetFileReadOnly(filename, false);
            try
            {
                File.Delete(filename);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Delete file error:" + ex.Message);
                return false;
            }

            bl = FileExist(filename);
            return !bl;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcfilename"></param>
        /// <param name="destfilename"></param>
        /// <returns></returns>
        public static Boolean CopyFile(String srcFileName, String destFileName)
        {
            Boolean bl = FileExist(srcFileName);
            if (!bl)
            {
                return false;
            }
            if (FileExist(destFileName))
            {
                DeleteFile(destFileName);
            }
            try
            {
                File.Copy(srcFileName, destFileName, true);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CopyFile Error:" + ex.Message);
                return false;
            }
            bl = FileExist(destFileName);
            return bl;
        }


        /// <summary>
        /// 剪切文件
        /// </summary>
        /// <param name="srcfilename"></param>
        /// <param name="destfilename"></param>
        /// <returns></returns>
        public static Boolean MoveFile(String srcFileName, String destFileName)
        {
            Boolean bl = FileExist(srcFileName);
            if (!bl)
            {
                return false;
            }
            if (FileExist(destFileName))
            {
                DeleteFile(destFileName);
            }
            File.Move(srcFileName, destFileName);
            bl = FileExist(destFileName);
            return bl;
        }

        #endregion


        #region 文件内容操作


        /// <summary>
        /// 读取文件里所有内容到文本里
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filestring">读取到的文件内容</param>
        /// <param name="asUTF8">以utf8格式读取</param>
        /// <returns>装载成功返回True;失败返回False</returns>
        public static Boolean ReadFileString(String filename, out String filestring, Boolean asUTF8 = false)
        {
            filestring = String.Empty;
            Int32 filelen = GetFileLength(filename);
            if (filelen < 1)
            {
                return false;
            }

            if (asUTF8)
            {
                filestring = File.ReadAllText(filename, Encoding.UTF8);
            }
            else
            {
                filestring = File.ReadAllText(filename, Encoding.Default);
            }
            return true;
        }

        /// <summary>
        /// 读取文件里所有内容到文本列表里
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filestring">读取到的文件内容</param>
        /// <param name="excludeempty">排除空字符串</param>
        /// <param name="asUTF8">以utf8格式读取</param>
        /// <returns>装载成功返回True;失败返回False</returns>
        public static Boolean ReadFileString(String filename, out List<String> filestring,
            Boolean excludeempty = true, Boolean asUTF8 = true)
        {
            filestring = new List<String>();
            Int32 filelen = GetFileLength(filename);
            if (filelen < 1)
            {
                return false;
            }

            String[] ary = null;
            if (asUTF8)
            {
                ary = File.ReadAllLines(filename, Encoding.UTF8);
            }
            else
            {
                ary = File.ReadAllLines(filename, Encoding.Default);
            }
            if ((ary == null) && (ary.Length < 1))
            {
                return false;
            }

            String tmpstr = String.Empty;
            if (excludeempty)
            {
                foreach (String item in ary)
                {
                    tmpstr = item.Trim();
                    if (tmpstr.Length > 0)
                    {
                        filestring.Add(tmpstr);
                    }
                }
            }
            else
            {
                filestring.AddRange(ary);
            }
            return (filestring.Count > 0);
        }

        /// <summary>
        /// 保存文本到文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filestring">文件内容</param>
        /// <param name="saveasUTF8">true保存为utf8,false保存为ANSI</param>
        /// <returns>保存成功返回True;失败返回False</returns>
        public static Boolean WriteFileString(String filename, List<String> filestring, Boolean saveasUTF8 = true)
        {
            if ((filestring == null) || (filestring.Count < 1))
            {
                return false;
            }

            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //先删除当前文件
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            String[] strary = filestring.ToArray();
            if (saveasUTF8)
            {
                File.WriteAllLines(filename, strary, Encoding.UTF8);
            }
            else
            {
                File.WriteAllLines(filename, strary, Encoding.Default);
            }
            return true;
        }

        /// <summary>
        /// 保存文本到文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filestring">文件内容</param>
        /// <param name="saveasUTF8">true保存为utf8,false保存为ANSI</param>
        /// <returns>保存成功返回True;失败返回False</returns>
        public static Boolean WriteFileString(String filename, String filestring, Boolean saveasUTF8 = true)
        {
            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //先删除当前文件
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            if (saveasUTF8)
            {
                File.WriteAllText(filename, filestring, Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(filename, filestring, Encoding.Default);
            }
            return true;
        }


        /// <summary>
        /// 读取文件里所有内容到字节数组里
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filedata">目标缓冲区</param>
        /// <returns>装载成功返回True;失败返回False</returns>
        public static Boolean ReadFileBytes(String filename, out Byte[] filedata)
        {
            filedata = null;
            Int32 filelen = GetFileLength(filename);
            if (filelen < 1)
            {
                return false;
            }

            filedata = File.ReadAllBytes(filename);
            return true;
        }


        /// <summary>
        /// 把字节数组写入文件里.该文件如果存在,则先删除
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param> 
        /// <returns></returns>
        public static Boolean WriteFileBytes(String filename, Byte[] filedata)
        {
            if ((filedata == null) || (filedata.Length < 1))
            {
                return false;
            }

            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //先删除当前文件
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            //再创建新文件 
            File.WriteAllBytes(filename, filedata);
            return true;
        }

        /// <summary>
        /// 把数据写入文件里指定位置
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="startidx">保存的起始位置</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="filelength">文件内容数据长度</param>
        /// <returns></returns>
        public static Boolean SaveDataToFile(String filename, Int32 startidx, Byte[] filedata, Int32 filelength)
        {
            if ((filedata == null) || (filedata.Length < 1) || (filelength < 1))
            {
                return false;
            }

            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //文件不存在,则新建 
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            fs.Seek(startidx, SeekOrigin.Current);
            fs.Write(filedata, 0, filelength);
            fs.Close();
            return true;
        }

        /// <summary>
        /// 把数据追加到文件末尾
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="start">文件内容从哪个字节开始准备复制到文件</param>
        /// <param name="filelength">文件数据长度</param>
        /// <returns></returns>
        public static Boolean AppendDataToFile(String filename, Byte[] filedata, Int32 start, Int32 datalength)
        {
            if ((filedata == null) || (filedata.Length < 1) || (datalength < 1) ||
                start < 0 || start + datalength > filedata.Length)
            {
                return false;
            }

            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //打开文件
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            fs.Seek(0, SeekOrigin.End);
            fs.Write(filedata, start, datalength);
            fs.Close();

            return true;
        }


        /// <summary>
        /// 把数据追加到文件末尾
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="filelength">文件数据长度</param>
        /// <returns></returns>
        public static Boolean AppendDataToFile(String filename, Byte[] filedata, Int32 datalength)
        {
            Boolean bl = AppendDataToFile(filename, filedata, 0, datalength);
            return true;
        }


        /// <summary>
        /// 把数据追加到文件末尾
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="filelength">文件数据长度</param>
        /// <returns></returns>
        public static Boolean AppendDataToFile(String filename, Byte[] filedata)
        {
            if (filedata == null || filedata.Length < 1)
            {
                return false;
            }
            Boolean bl = AppendDataToFile(filename, filedata, 0, filedata.Length);
            return true;
        }

        /// <summary>
        /// 把字符串追加到文件末尾
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="saveasUTF8">true保存为utf8,false保存为ANSI</param>
        /// <returns></returns>
        public static Boolean AppendStringToFile(String filename, String filedata, Encoding encode)
        {
            if ((filedata == null) || (filedata.Length < 1))
            {
                return false;
            }

            String dir = GetDirectoryName(filename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //打开文件
            FileStream fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            fs.Seek(0, SeekOrigin.End);
            StreamWriter streamWriter = null;

            streamWriter = new StreamWriter(fs, encode);
            streamWriter.Write(filedata);
            streamWriter.Flush();
            streamWriter.Close();
            fs.Close();
            return true;
        }
        /// <summary>
        /// 把字符串追加到文件末尾
        /// </summary>
        /// <param name="filename">文件名,带完整路径</param>
        /// <param name="filedata">文件内容数据</param>
        /// <param name="saveasUTF8">true保存为utf8,false保存为ANSI</param>
        /// <returns></returns>
        public static Boolean AppendStringToFile(String filename, String filedata, Boolean saveasUTF8 = true)
        {
            Boolean bl = false;
            if (saveasUTF8)
            {
                bl = AppendStringToFile(filename, filedata, Encoding.UTF8);
            }
            else
            {
                bl = AppendStringToFile(filename, filedata, Encoding.Default);
            }
            return bl;
        }


        #endregion

        #region 图像文件装载

        /// <summary>
        /// 装载图像文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Image LoadImage(String filename)
        {
            Byte[] photodata = null;
            Boolean bl = FileExist(filename);
            if (!bl)
            {
                return null;
            }

            bl = ReadFileBytes(filename, out photodata);

            if (!bl)
            {
                return null;
            }

            MemoryStream ms = null;
            Image myImage = null;
            try
            {
                ms = new MemoryStream(photodata);
                myImage = Image.FromStream(ms);
                ms.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("LoadImage error:" + ex.Message);
                myImage = null;
            }
            return myImage;
        }

        #endregion

        #region 磁盘剩余空间
        /// <summary>
        /// 获取磁盘剩余可用空间的大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long GetDriveFreeSpace(String path)
        {
            String dirvename = String.Empty;
            Boolean bl = GetDriveName(path, ref dirvename);
            if (!bl)
            {
                return -1;
            }
            System.IO.DriveInfo drive = new System.IO.DriveInfo(dirvename);
            long freeSpace = drive.AvailableFreeSpace;
            return freeSpace;
        }
        #endregion
    }
}
