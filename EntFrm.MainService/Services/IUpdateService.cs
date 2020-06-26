using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntFrm.MainService.Services
{
    public class IUpdateService
    {
        private volatile static IUpdateService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;

        public static IUpdateService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IUpdateService();
                }
            }
            return _instance;
        }

        public void StartUpdateFlows()
        {
            int count = 0;
            ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            ProcessFlowsCollections infoColl = null;

            MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "过号服务启动完成...");

            while (true)
            {
                if (isQuitFlag)
                {
                    break;
                } 


                infoColl = infoBLL.GetRecordsByPaging(ref count,1,100, " EnqueueTime<'"+DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss")+"' And PriorityType=" + IPublicConsts.PRIORITY_TYPE1+" And ProcessState="+IPublicConsts.PROCSTATE_DIAGNOSIS);

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ProcessFlows processFlow in infoColl)
                    {
                        processFlow.iDataFlag = 1;
                        processFlow.iProcessState = IPublicConsts.PROCSTATE_NONARRIVAL;
                        processFlow.dFinishTime = DateTime.Now;
                        processFlow.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(processFlow);
                    }
                }

                Thread.Sleep(30000);
            }
        }

        public void StopUpdateFlows()
        {
            isQuitFlag = true;
        }
    }
}
