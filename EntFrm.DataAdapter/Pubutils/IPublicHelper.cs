using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntFrm.DataAdapter
{
    public class IPublicHelper
    { 

        public static string SubStringEx(string sSource,int length)
        {
            if (!string.IsNullOrEmpty(sSource)&&sSource.Length>length)
            {
                return sSource.Substring(0,length);
            }
            else  
            {
                return sSource;
            } 
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

        public static string GetBranchNoByName(string sName)
        {
            if (sName != null && sName.Length > 0)
            {
                int count = 0;
                BranchInfoBLL infoBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                BranchInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "BranchName='"+ sName + "'");
                if(infoColl!=null&& infoColl.Count > 0)
                {
                    return infoColl[0].sBranchNo;
                }
                return "";
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

        public static string GetStafferNoByCounterNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                try
                {
                    CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    CounterInfo info = infoBoss.GetRecordByNo(sNo);
                    if (info != null && info.iLogonState == 1)
                    {
                        return info.sLogonStafferNo;
                    }
                }
                catch(Exception ex)
                {
                    return "";
                }
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

        public static CounterInfo GetCounterByStafferNo(string sStafferNo)
        {
            if (sStafferNo != null && sStafferNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, "StafferNo='" + sStafferNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl[0];
                }
            }
            return null;
        }

        public static CounterInfo GetCounterByCallerAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count=0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1,1, "SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if(callerColl!=null&&callerColl.Count>0)
                {
                    CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1,"CallerNo='"+callerColl[0].sCallerNo+"'");
                
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
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1,"CallerNo='" + sCallerNo + "'");

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
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1,"LedDisplayNo='" + sLedNo + "'");

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
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1000, "ServiceGroupValue Like '%" + sServiceNo + ":%'");

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


        public static StafferInfo GetStafferByLoginId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " LoginId='" + sNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
            }
            return null;
        }


        public static string  GetStaffIdByCounterId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            { 
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo info = infoBoss.GetRecordByNo(sNo);

                if (info != null )
                {
                    return info.sLogonStafferNo;
                }
            }
            return "";
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
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 1,"SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if (callerColl != null && callerColl.Count > 0)
                {
                    return callerColl.GetFirstOne();
                }

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

        public static string getPlayerNoByCode(string playerCode)
        {
            if (playerCode != null && playerCode.Length > 0)
            {
                int i = 0;
                DsPlayerInfoBLL infoBoss = new DsPlayerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                DsPlayerInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref i, 1, 1, "PlayerCode='" + playerCode + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sPlayerNo;
                }
            }

            return "";
        }

        public static int getChnAge(string cnAge)
        { 
                int i = 1;
                string s = "0";
                if (cnAge.Contains("岁"))
                {
                    cnAge = cnAge.Substring(0, cnAge.IndexOf("岁"));
                    s= System.Text.RegularExpressions.Regex.Replace(cnAge, @"[^0-9]+", "");
                }
                else
                {
                    s = cnAge;
                }

                try
                {
                    i = int.Parse(s);
                }
                catch (Exception ex) { }

                return i; 
        }


        public static int getWorkTimeType(string cnType)
        {
            int iresult = 0;
            switch (cnType)
            {
                case "休息": iresult = 0; break;
                case "全日": iresult = 1; break;
                case "上午": iresult = 2; break;
                case "下午": iresult = 3; break;
                default: break;
            }
            return iresult;
        }

        public static bool getOnlineState(string sPlayerNo)
        {
            DsHrtbeatFlowsBLL infoBLL = new DsHrtbeatFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

            int count = infoBLL.GetCountByCondition("PlayerNo='" + sPlayerNo + "' And RegistDate>'" + DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            return (count > 0) ? true : false;
        }
        public static string GetPriorityType(int priorityType)
        {
            string sResult = "普通";
            switch (priorityType)
            {
                case IPublicConsts.PRIORITY_TYPE0:
                    sResult = "普通";
                    break;
                case IPublicConsts.PRIORITY_TYPE1:
                    sResult = "老人优先";
                    break;
                case IPublicConsts.PRIORITY_TYPE2:
                    sResult = "幼儿优先";
                    break;
                case IPublicConsts.PRIORITY_TYPE3:
                    sResult = "军人优先";
                    break;
                case IPublicConsts.PRIORITY_TYPE4:
                    sResult = "离休优先";
                    break;
                case IPublicConsts.PRIORITY_TYPE5:
                    sResult = "过号优先";
                    break;
                case IPublicConsts.PRIORITY_TYPE6:
                    sResult = "其他优先";
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
                    sResult = "过号初诊";
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
    }
}
