using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace EntFrm.MainService
{
    public class IPublicHelper
    { 
        public static void DoAutoUpdate()
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                string updateFile = AppDomain.CurrentDomain.BaseDirectory + "\\AutoUpdate\\EntFrm.AutoUpdate.exe";
                //判断文件的存在
                if (File.Exists(updateFile))
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = updateFile;
                    startInfo.Arguments = "";
                    System.Diagnostics.Process.Start(startInfo);
                }

            }))).Start();
        }

        public static string GetBranchNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                BranchInfoBLL infoBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static BranchInfo GetBranchByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                BranchInfoBLL infoBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetServiceNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static ServiceInfo GetServiceByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetCounterNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static CounterInfo GetCounterByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static VoiceInfo GetVoiceInfoByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                VoiceInfoBLL infoBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CounterInfo GetCounterByStafferNo(string sStafferNo)
        {
            if (sStafferNo != null && sStafferNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl[0];
                }
            }
            return null;
        }

        public static string GetCounterNosByServiceNo(string sServiceNo, string sBranchNo)
        {
            try
            {
                int count = 0;
                string sResult = "";
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 10, " BranchNo='" + sBranchNo + "' And  ServiceGroupValue Like '%" + sServiceNo + "%' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sResult += info.sCounterNo + ";";
                    }

                    sResult.Trim(';');
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CounterInfo GetCounterByCallerAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count=0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1,1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if(callerColl!=null&&callerColl.Count>0)
                {
                    CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And CallerNo='" + callerColl[0].sCallerNo+"'");
                
                    if(counterColl!=null&&counterColl.Count>0)
                    {
                        return counterColl[0];
                    }
                }

            }
            return null;
        }

        public static CounterInfo GetCounterByCallerNo(string sCallerNo)
        {
            if (!string.IsNullOrEmpty(sCallerNo))
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And CallerNo='" + sCallerNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl.GetFirstOne();
                }
            }
            return null;
        }

        public static CounterInfo GetCounterByDisplayNo(string sLedNo)
        {
            if (sLedNo != null && sLedNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And LedDisplayNo='" + sLedNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl[0];
                }
            }
            return null;
        }

        public static string getCounterGroupByServiceNo(string sServiceNo)
        {
            StringBuilder sb = new StringBuilder();
            if (sServiceNo != null && sServiceNo.Length > 0)
            {
                int count = 0;

                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1000, " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceGroupValue Like '%" + sServiceNo + ":%'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sb.Append(info.sCounterNo + ";");
                    }
                }
            }
            return sb.ToString();
        }


        public static string GetStafferNameById(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            { 
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return   infoBoss.GetRecordNameByNo(sNo); 
            }
            return "";
        }


        public static string GetRUserNameById(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }


        public static StafferInfo GetStaffByLoginId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "  BranchNo = '" + IUserContext.GetBranchNo() + "' And LoginId='" + sNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
            }
            return null;
        }

        public static CallerInfo GetCallerByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CallerInfoBLL infoBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CallerInfo GetCallerByPhyAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count = 0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if (callerColl != null && callerColl.Count > 0)
                {
                    return callerColl.GetFirstOne();
                }

            }
            return null;
        }

        public static EvaluatorInfo GetEvaluatorByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                EvaluatorInfoBLL infoBoss = new EvaluatorInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static LEDDisplay GetLEDDisplayByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                LEDDisplayBLL infoBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static ProcessFlows GetProcessFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ProcessFlowsBLL infoBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static TicketFlows GetTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                TicketFlowsBLL infoBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static ViewTicketFlows GetVTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static WaitArea GetWaitAreaNoByWAreaIndex(int index)
        {
            try
            {
                int count = 0;
                WaitAreaBLL infoBoss = new WaitAreaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                WaitAreaCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + IUserContext.GetBranchNo() + "' And AreaIndex=" + index);

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ReplaceVariables(string sFormatStr, string sPFlowNo)
        {
            try
            {
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ViewTicketFlows vTicketFlow = infoBoss.GetRecordByNo(sPFlowNo);

                if(vTicketFlow!=null)
                {
                    CounterInfo counter = GetCounterByNo(vTicketFlow.sProcessedCounterNo);

                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                    }

                    //string waiterNum = OpenTicketBLL.getVTicketCountByServiceNo(vTicketFlow.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0");
                    //string where = "ProcessStatus=0 And ComeTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                    //string allWaiterNum = OpenTicketBLL.getVTicketCountByCondition(where);
                    string waiterNum = "";
                    string where = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessStatus=0 And ComeTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                    string allWaiterNum ="";


                    sFormatStr = sFormatStr.Replace("ServiceName", GetServiceNameByNo(vTicketFlow.sServiceNo));
                    sFormatStr = sFormatStr.Replace("TicketNo", vTicketFlow.sTicketNo);
                    sFormatStr = sFormatStr.Replace("ServiceWaiterNumber", waiterNum);
                    sFormatStr = sFormatStr.Replace("AllWaitingNumber", allWaiterNum);
                    sFormatStr = sFormatStr.Replace("FullName", vTicketFlow.sCnName);
                    sFormatStr = sFormatStr.Replace("IdNumber", vTicketFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("CardNumber", vTicketFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("Telephone", vTicketFlow.sTelphone);
                    sFormatStr = sFormatStr.Replace("yyyy-MM-dd-HH:mm:ss", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("yyyy/MM/dd-HH:mm:ss", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("HH:mm:ss", DateTime.Now.ToString("HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("hh:mm:ss", DateTime.Now.ToString("hh:mm:ss"));
                     
                }
                return sFormatStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetPriorityType(int priorityType)
        {
            string sResult = "普通";
            switch (priorityType)
            {
                case IPublicConsts.PRIORITY_TYPE1:
                    sResult = "预约";
                    break;
                case IPublicConsts.PRIORITY_TYPE2:
                    sResult = "过号";
                    break;
                case IPublicConsts.PRIORITY_TYPE3:
                    sResult = "军人";
                    break;
                case IPublicConsts.PRIORITY_TYPE4:
                    sResult = "离休";
                    break;
                case IPublicConsts.PRIORITY_TYPE5:
                    sResult = "幼儿";
                    break;
                case IPublicConsts.PRIORITY_TYPE6:
                    sResult = "老人";
                    break;
                case IPublicConsts.PRIORITY_TYPE7:
                    sResult = "急诊";
                    break;
                default:
                    break;
            }

            return sResult;
        }

        public static string GetProcessState(int processState)
        {
            string sResult = "";
            switch (processState)
            {
                case IPublicConsts.PROCSTATE_OUTQUEUE:
                    sResult = "未入队";
                    break;
                case IPublicConsts.PROCSTATE_DIAGNOSIS:
                    sResult = "初诊";
                    break;
                case IPublicConsts.PROCSTATE_TRIAGE:
                    sResult = "分诊";
                    break;
                case IPublicConsts.PROCSTATE_EXCHANGE:
                    sResult = "转诊";
                    break;
                case IPublicConsts.PROCSTATE_REDIAGNOSIS:
                    sResult = "复诊";
                    break;
                case IPublicConsts.PROCSTATE_PASSTICKET:
                    sResult = "初诊";
                    break;
                case IPublicConsts.PROCSTATE_DELAY:
                    sResult = "延迟";
                    break;
                case IPublicConsts.PROCSTATE_WAITING:
                    sResult = "等候";
                    break;
                case IPublicConsts.PROCSTATE_WAITAREA1:
                    sResult = "等候中";
                    break;
                case IPublicConsts.PROCSTATE_WAITAREA2:
                    sResult = "等候中";
                    break;
                case IPublicConsts.PROCSTATE_WAITAREA3:
                    sResult = "等候中";
                    break;
                case IPublicConsts.PROCSTATE_CALLING:
                    sResult = "叫号中";
                    break;
                case IPublicConsts.PROCSTATE_PROCESSING:
                    sResult = "就诊中";
                    break;
                case IPublicConsts.PROCSTATE_FINISHED:
                    sResult = "已就诊";
                    break;
                case IPublicConsts.PROCSTATE_NONARRIVAL:
                    sResult = "过号";
                    break;
                case IPublicConsts.PROCSTATE_HANGUP:
                    sResult = "挂起";
                    break;
                case IPublicConsts.PROCSTATE_GREENCHANNEL:
                    sResult = "绿色通道";
                    break;
                case IPublicConsts.PROCSTATE_ARCHIVE:
                    sResult = "归档";
                    break;
                default:
                    break;
            }

            return sResult;
        }

        public static string ReplaceVariables_Recipe(string sFormatStr, string sRFlowNo)
        {
            try
            {
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");

                ViewRecipeFlowsBLL infoBoss = new ViewRecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ViewRecipeFlows recipeFlow = infoBoss.GetRecordByNo(sRFlowNo);

                if (recipeFlow != null)
                {
                    CounterInfo counter = GetCounterByNo(recipeFlow.sCounterNo);

                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                    }
                     
                    sFormatStr = sFormatStr.Replace("TicketNo", recipeFlow.sTicketNo); 
                    sFormatStr = sFormatStr.Replace("FullName", recipeFlow.sCnName);
                    sFormatStr = sFormatStr.Replace("IdNumber", recipeFlow.sIdCardNo); 
                    sFormatStr = sFormatStr.Replace("yyyy-MM-dd-HH:mm:ss", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("yyyy/MM/dd-HH:mm:ss", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("HH:mm:ss", DateTime.Now.ToString("HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("hh:mm:ss", DateTime.Now.ToString("hh:mm:ss"));

                }
                return sFormatStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
