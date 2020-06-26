using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EntWeb.BkConsole.Common
{
    public class AdapterUtil
    {
        public static List<T> ToViewList<T>(CollectionBase infoColl)
        {
            if (infoColl != null && infoColl.Count > 0)
            {
                List<T> infoList = new List<T>();
                foreach (T info in infoColl)
                {
                    infoList.Add(info);
                }

                return infoList;
            }
            else
            {
                return null;
            }
        }
        public static string SubStringEx(string sSource, int length)
        {
            if (!string.IsNullOrEmpty(sSource) && sSource.Length > length)
            {
                return sSource.Substring(0, length);
            }
            else
            {
                return sSource;
            }
        }

        public static string AddRUserInfo(string RUserName, string RUserSex, string IdNo, string Age, string Telphone, string SuNo, string PatRiNo = "")
        {
            try
            {
                int count = 0;
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                //RUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + IdNo + "' ");
                RUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " CnName='" + RUserName + "' ");
                RUsersInfo info = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    info.sCnName = RUserName;
                    info.sEnName = PinYinHelper.GetPinYinCode(RUserName).ToLower();
                    info.iAge = int.Parse(Age);
                    info.iSex = int.Parse(RUserSex);
                    info.sIdCardNo = IdNo;
                    info.sTelphone = Telphone;
                    info.dModDate = DateTime.Now;
                    info.sModOptor = SuNo;

                    infoBLL.UpdateRecord(info);

                    return info.sRUserNo;
                }
                else
                {
                    string sNo = CommonHelper.Get_New12ByteGuid();
                    info = new RUsersInfo();
                    info.sRUserNo = sNo;
                    info.sCnName = RUserName;
                    info.sEnName = PinYinHelper.GetPinYinCode(RUserName).ToLower();
                    info.iAge = int.Parse(Age);
                    info.iSex = int.Parse(RUserSex);
                    info.sNation = "";
                    info.iCardType = 1;
                    info.sIdCardNo = IdNo;
                    info.sRiCardNo = PatRiNo;
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = Telphone;
                    info.sHeadPhoto = "";
                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sSummary = "";

                    info.sAddOptor = SuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = SuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        return sNo;
                    }
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetBranchNoByServiceNo(string serviceNo)
        {
            ServiceInfo info = PageHelper.getServiceInfoByNo(serviceNo);
            if (info != null)
            {
                return info.sBranchNo;
            }
            return "";
        }

        public static string GetServiceNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static ServiceInfo GetServiceByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetCounterNameById(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
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
                    CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                    CounterInfo info = infoBoss.GetRecordByNo(sNo);
                    if (info != null && info.iLogonState == 1)
                    {
                        return info.sLogonStafferNo;
                    }
                }
                catch (Exception ex)
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
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CounterInfo GetCounterByStafferNo(string sStafferNo)
        {
            if (sStafferNo != null && sStafferNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, "LogonStafferNo='" + sStafferNo + "'");

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
                int count = 0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 1, "SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if (callerColl != null && callerColl.Count > 0)
                {
                    CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                    CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, "CallerNo='" + callerColl[0].sCallerNo + "'");

                    if (counterColl != null && counterColl.Count > 0)
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
                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, "CallerNo='" + sCallerNo + "'");

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
                CounterInfoBLL counterBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, "LedDisplayNo='" + sLedNo + "'");

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

                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
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
                StafferInfoBLL infoBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }


        public static StafferInfo GetStafferByLoginId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " LoginId='" + sNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
            }
            return null;
        }


        public static string GetStafferIdByCounterId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CounterInfo info = infoBoss.GetRecordByNo(sNo);

                if (info != null)
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
                CallerInfoBLL infoBoss = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CallerInfo GetCallerByPhyAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count = 0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 1, "SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

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
                LEDDisplayBLL infoBoss = new LEDDisplayBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static ProcessFlows GetProcessFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ProcessFlowsBLL infoBoss = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static TicketFlows GetTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                TicketFlowsBLL infoBoss = new TicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static ViewTicketFlows GetVTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }
    }
}