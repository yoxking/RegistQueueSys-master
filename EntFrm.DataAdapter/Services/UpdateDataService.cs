using EntFrm.DataAdapter.Business;
using EntFrm.Framework.Utility;
using System;
using System.Threading;

namespace EntFrm.DataAdapter.Services
{
    public class UpdateDataService
    {
        private volatile static UpdateDataService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;

        public static UpdateDataService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new UpdateDataService();
                }
            }
            return _instance;
        }
        private UpdateDataService() { }

        public void StartUpdateTask()
        {

            MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "数据采集服务启动完成...");
            IAdapterBusiness adapterBoss = AdapterFactory.Create();

            while (adapterBoss != null)
            {
                if (isQuitFlag)
                {
                    break;
                }
                Thread.Sleep(30000);

                try
                {
                    if (!adapterBoss.updateRecipeList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "取药病人信息更新失败...");
                    }

                    if (!adapterBoss.updatePatientList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "挂号病人信息更新失败...");
                    }

                    if (!adapterBoss.updateRegisteList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "预约挂号信息更新失败...");
                    }

                    if (!adapterBoss.updatePhexamList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "检查病人信息更新失败...");
                    }

                    if (!adapterBoss.updateInspectList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "检验病人信息更新失败...");
                    }

                    if (!adapterBoss.updateOperateList())
                    {
                        MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "手术病人信息更新失败...");
                    }

                }
                catch (Exception ex)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "病人信息更新失败," + ex.Message);
                    //MyFileHelper.WriteLog(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "错误:" + ex.Message);
                }
            }
        }

        public void StopUpdateTask()
        {
            isQuitFlag = true;
        }
    }
}
