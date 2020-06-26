using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EntFrm.MainService.Services
{
    public class IBusinessHelper
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

        public static string getServiceNoByStafferNo(string stafferNo)
        {
            try
            {
                int count = 0;
                string sResult = "";
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 10, " BranchNo='" + IUserContext.GetBranchNo() + "' And  LogonStafferNo = '" + stafferNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    sResult = infoColl.GetFirstOne().sServiceGroupValue.Substring(0, 12); 
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static StafferInfo getStafferNoByStafferName(string stafferName)
        {
            try
            {
                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "StafferName = '" + stafferName + "'");

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

        public static ServiceInfo getServiceNoByServiceName(string serviceName)
        {
            try
            {
                int count = 0;
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "ServiceName = '" + serviceName + "'");

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

        public static string EnqueueRegistUser(RegistFlows regFlow)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "";
                string sCounterNos = "";
                string sWAreaNo = ""; 
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlowsBLL rflowBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                //RegistFlows regFlow = rflowBLL.GetRecordByNo(sRFlowNo);

                if (regFlow != null)
                {
                    WaitArea wareaInfo = getWaitAreaNoByWAreaIndex(0, regFlow.sBranchNo);
                    if (wareaInfo != null)
                    {
                        sWAreaNo = wareaInfo.sWAreaNo;
                    }

                    if (string.IsNullOrEmpty(regFlow.sTicketNo))
                    {
                        sTicketNo = doGenerateTicketNo(regFlow.sServiceNo, regFlow.sStafferNo, regFlow.sBranchNo);
                    }
                    else
                    {
                        sTicketNo = regFlow.sTicketNo;
                    }

                    if (!string.IsNullOrEmpty(regFlow.sServiceNo))
                    {
                        sCounterNos = IPublicHelper.GetCounterNosByServiceNo(regFlow.sServiceNo, regFlow.sBranchNo);
                    }

                    sTFlowNo = InsertTicketFlow(sTicketNo, regFlow.sRUserNo, regFlow.sBranchNo);
                    if (!string.IsNullOrEmpty(sTFlowNo))
                    {
                        sPFlowNo = InsertProcessFlow(sTFlowNo, regFlow.sServiceNo, sCounterNos, regFlow.sStafferNo, regFlow.iRegistType, sWAreaNo, 0, regFlow.sBranchNo);
                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            regFlow.iRegistState = 1;
                            regFlow.dModDate = DateTime.Now;
                            rflowBLL.UpdateRecord(regFlow);
                            return sPFlowNo;
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
            }
            return "";
        }
         
        public static WaitArea getWaitAreaNoByWAreaIndex(int index, string sBranchNo)
        {
            try
            {
                int count = 0;
                WaitAreaBLL infoBoss = new WaitAreaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                WaitAreaCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And AreaIndex=" + index);

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

        public static string getCounterNosByServiceNo(string sServiceNo, string sBranchNo)
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

                    sResult=sResult.Trim(';');
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertTicketFlow(string sTicketNo, string sRUserNo, string sBranchNo)
        {
            try
            {
                TicketFlowsBLL flowBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                string sTFlowNo = CommonHelper.Get_New12ByteGuid();

                TicketFlows flowInfo = new TicketFlows();

                flowInfo.sTFlowNo = sTFlowNo;
                flowInfo.iDataFlag = 0;
                flowInfo.sTicketNo = sTicketNo;
                flowInfo.sRUserNo = sRUserNo;

                flowInfo.sBranchNo = sBranchNo;
                flowInfo.sComments = "";
                flowInfo.sAddOptor = "00000000";
                flowInfo.dAddDate = DateTime.Now;
                flowInfo.sModOptor = "00000000";
                flowInfo.dModDate = DateTime.Now;
                flowInfo.iValidityState = 1;
                flowInfo.sAppCode = IUserContext.GetAppCode() + ";";

                if (flowBoss.AddNewRecord(flowInfo))
                {
                    return sTFlowNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertProcessFlow(string sTFlowNo, string sServiceNo, string sCounterNos, string sStafferNo, int iRegistType,string sWaitAreaNo, int iWAreaIndex, string sBranchNo)
        {
            try
            {
                string sPFlowNo = CommonHelper.Get_New12ByteGuid();
                ProcessFlowsBLL flowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ProcessFlows flowInfo = new ProcessFlows();

                flowInfo.sPFlowNo = sPFlowNo;
                flowInfo.sTFlowNo = sTFlowNo;
                flowInfo.iDataFlag = 0;
                flowInfo.sServiceNo = sServiceNo;
                flowInfo.sCounterNos = sCounterNos;
                flowInfo.sStafferNo = sStafferNo;
                flowInfo.sWaitAreaNo = sWaitAreaNo;
                flowInfo.iWAreaIndex = iWAreaIndex;
                flowInfo.sWorkFlowsNo = "";
                flowInfo.iWFlowsIndex = 0;
                flowInfo.dEnqueueTime = DateTime.Now;
                flowInfo.dBeginTime = DateTime.Parse("1970-1-1");
                flowInfo.dFinishTime = DateTime.Parse("1970-1-1");
                flowInfo.iProcessState = IPublicConsts.PROCSTATE_DIAGNOSIS;
                flowInfo.sProcessFormat = "";
                flowInfo.iProcessIndex = 0;
                flowInfo.iPriorityType = iRegistType-1;
                flowInfo.iOrderWeight = (iRegistType == IPublicConsts.REGISTETYPE2) ? 9 : 0;
                //flowInfo.iOrderWeight = 0;
                flowInfo.iPauseState = 0;
                flowInfo.iDelayType = 0;
                flowInfo.iDelayTimeValue = 0;
                flowInfo.iDelayStepValue = 0;
                flowInfo.dProcessedTime = DateTime.Now;
                flowInfo.sProcessedCounterNo = "";
                flowInfo.sProcessedStafferNo = "";

                flowInfo.sComments = "";
                flowInfo.sBranchNo = sBranchNo;
                flowInfo.sAddOptor = "00000000";
                flowInfo.dAddDate = DateTime.Now;
                flowInfo.sModOptor = "00000000";
                flowInfo.dModDate = DateTime.Now;
                flowInfo.iValidityState = 1;
                flowInfo.sAppCode = IUserContext.GetAppCode() + ";";

                if (flowBoss.AddNewRecord(flowInfo))
                {
                    return sPFlowNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertRUserInfo(string sUName, string sIdCardNo, string sTelphone, string PatRiNo = "")
        {
            try
            {
                int count = 0;

                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RUsersInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + sIdCardNo + "' ");
                RUsersInfo info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    info.sCnName = sUName;
                    info.sTelphone = sTelphone;

                    info.dModDate = DateTime.Now;

                    if (infoBoss.UpdateRecord(info))
                    {
                        return info.sRUserNo;
                    }
                }
                else
                {
                    string sRUserNo = CommonHelper.Get_New12ByteGuid();
                    info = new RUsersInfo();

                    info.sRUserNo = sRUserNo;
                    info.sCnName = sUName;
                    info.sEnName = "";
                    info.iAge = 0;
                    info.iSex = 1;
                    info.sNation = "汉";
                    info.iCardType = 1;
                    info.sIdCardNo = sIdCardNo;
                    info.sRiCardNo = PatRiNo;
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = sTelphone;
                    info.sHeadPhoto = "";
                    info.sSummary = "";

                    info.sBranchNo = IUserContext.GetBranchNo();
                    info.sComments = "";
                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (infoBoss.AddNewRecord(info))
                    {
                        return sRUserNo;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        public static string doGenerateTicketNo(string sServiceNo, string sStafferNo, string sBranchNo)
        {
            int count = 0;
            int serviceNum = 0;
            int staffNum = 0;
            string sTicketNo = "001";
            string sKeyType = "Ticket";
            DateTime workDate = DateTime.Now;
            ServiceInfo serviceInfo = null;
            SysParams info = null;
            SysParamsCollections infoColl = null;

            try
            {
                SysParamsBLL infoBLL = new SysParamsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewTicketFlowsBLL vflowBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                if (!string.IsNullOrEmpty(sServiceNo))
                {
                    serviceInfo = IPublicHelper.GetServiceByNo(sServiceNo);
                    serviceNum = vflowBLL.GetCountByCondition(" BranchNo='" + sBranchNo + "' And  ServiceNo='" + sServiceNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ") + 1;
                    infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sServiceNo + "' And KeyType='" + sKeyType + "' ");

                    if (serviceInfo != null && infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();
                        DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                        if (info.dModDate < dt)
                        {
                            info.sKeyValue = 0 + "";
                        }

                        count = int.Parse(info.sKeyValue);
                        count++;
                        if (serviceNum > count)
                        {
                            count = serviceNum;
                        }

                        info.sKeyValue = count.ToString();
                        info.dModDate = DateTime.Now;
                        infoBLL.UpdateRecord(info);

                        sTicketNo = serviceInfo.sServiceType + String.Format("{0:D" + serviceInfo.iDigitLength + "}", count);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sServiceNo;
                        info.sKeyValue = serviceNum.ToString();
                        info.sKeyType = sKeyType;
                        info.sBranchNo = sBranchNo;

                        info.sAddOptor = "";
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = "";
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";
                        info.sAppCode = IUserContext.GetAppCode() + ";";

                        infoBLL.AddNewRecord(info);

                        if (serviceInfo != null)
                        {
                            sTicketNo = serviceInfo.sServiceType + String.Format("{0:D" + serviceInfo.iDigitLength + "}", serviceNum);
                        }
                        else
                        {
                            sTicketNo = String.Format("{0:D" + serviceInfo.iDigitLength + "}", serviceNum);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(sStafferNo))
                {
                    staffNum = vflowBLL.GetCountByCondition(" BranchNo='" + sBranchNo + "' And  StafferNo='" + sStafferNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ") + 1;
                    infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sStafferNo + "' And KeyType='" + sKeyType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();
                        DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                        if (info.dModDate < dt)
                        {
                            info.sKeyValue = 0 + "";
                        }

                        count = int.Parse(info.sKeyValue);
                        count++;
                        if (staffNum > count)
                        {
                            count = staffNum;
                        }

                        info.sKeyValue = count.ToString();
                        info.dModDate = DateTime.Now;
                        infoBLL.UpdateRecord(info);

                        sTicketNo = String.Format("{0:D3}", count);

                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sStafferNo;
                        info.sKeyValue = staffNum.ToString();
                        info.sKeyType = sKeyType;
                        info.sBranchNo = sBranchNo;
                        info.sAddOptor = "";
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = "";
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";
                        info.sAppCode = IUserContext.GetAppCode() + ";";

                        infoBLL.AddNewRecord(info);

                        sTicketNo = String.Format("{0:D3}", staffNum);
                    }
                }
                else
                {
                    //默认生成票号
                    ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    string sWhere = " BranchNo='" + sBranchNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    int countNum = vticketBLL.GetCountByCondition(sWhere) + 1;
                    sTicketNo = String.Format("{0:D3}", countNum);
                }
            }
            catch (Exception ex)
            {
            }
            return sTicketNo;
        }
          
        public static ViewTicketFlows getVTicketFlowBy(string sCounterNo, string sWorkDate, string sProcessState, string sOrderType = "Asc")
        {
            try
            {
                DateTime workDate = DateTime.Parse(sWorkDate);
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                string sWhere = " ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + sProcessState + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = "ID ";
                s_model.sOrderType = sOrderType;
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                if (infoColl != null)
                {
                    return infoColl.GetFirstOne();
                }
            }
            catch (Exception ex) { }
            return null;

        }
    }
}