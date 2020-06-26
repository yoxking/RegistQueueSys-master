using EntFrm.Business.BLL;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntFrm.MainService.Services
{
    public class DbaseService
    {
        private volatile static DbaseService _instance = null;
        private static readonly object lockHelper = new object();
        public static DbaseService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new DbaseService();
                }
            }
            return _instance;
        }


        public void doClearQueueData()
        {
            try
            {
                string where = " BranchNo='"+IUserContext.GetBranchNo()+"' And AddDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                EvaluateFlowsBLL evalBoss = new EvaluateFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例   
                ProcessFlowsBLL procBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                RegistFlowsBLL regBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                TicketFlowsBLL ticBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  

                evalBoss.HardDeleteByCondition(where); 
                procBoss.HardDeleteByCondition(where);
                regBoss.HardDeleteByCondition(where);
                ticBoss.HardDeleteByCondition(where);

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "清空排队数据完成..."); 
            }
            catch (Exception ex)
            { 
            }
        } 

        public void doClearAllData()
        {
            try
            {
                //创建任务
                Task task = new Task(() =>
                {
                    //获得文件的完整路径（包括名字后后缀）
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("EntFrm.MainService.Resources.clearalldata.csql");


                    ArrayList mylist = IDbaseHelper.GetSqlFile(stream);
                    IDbaseHelper.ExecuteCmd(mylist, IUserContext.GetConnStr());

                });
                //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
                task.Start();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "清空所有数据完成...");
                 
            }
            catch (Exception ex)
            { 
            }
        }

        public void doBakupData()
        {
            try
            {
                //string localFilePath, fileNameExt, newFileName, FilePath; 
                SaveFileDialog sfd = new SaveFileDialog();
                //设置文件类型 
                sfd.Filter = "数据库备份文件（*.bak）|*.bak";

                //设置默认文件类型显示顺序 
                sfd.FilterIndex = 1;

                //保存对话框是否记忆上次打开的目录 
                sfd.RestoreDirectory = true;

                //点了保存按钮进入 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName.ToString(); //获得文件路径 
                    string dbaseName = IDbaseHelper.GetDataBaseName(IUserContext.GetConnStr());
                    string cmdText = @"backup database " + dbaseName + " to disk='" + fileName + "'";
                    IDbaseHelper.BakReductSql(IUserContext.GetConnStr(), dbaseName, cmdText, true);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "备份数据库完成..."); 
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "备份数据库失败：" + ex.Message); 
            }
        }

        public void doRecoverData()
        {
            try
            {
                //创建一个对话框对象
                OpenFileDialog ofd = new OpenFileDialog();
                //为对话框设置标题
                ofd.Title = "请选择bak文件";
                //设置筛选的图片格式
                ofd.Filter = "bak格式|*.bak";
                //设置是否允许多选
                ofd.Multiselect = false;
                //如果你点了“确定”按钮
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //获得文件的完整路径（包括名字后后缀） 
                    string fileName = ofd.FileName;
                    string dbaseName = IDbaseHelper.GetDataBaseName(IUserContext.GetConnStr());
                    string cmdText = @"restore database " + dbaseName + " from disk='" + fileName + "' WITH REPLACE";
                    IDbaseHelper.BakReductSql(IUserContext.GetConnStr(), dbaseName, cmdText, false);

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "恢复数据库完成..."); 
                }
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "恢复数据库失败：" + ex.Message);
            }
        }
    }
}
