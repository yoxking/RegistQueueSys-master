using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;

namespace EntWeb.HDeptConsole
{
    public class BusinessHelper
    { 
        public static string AddRUserInfo(string RUserNo,string RUserName, string RUserSex, string IdNo, string Age, string Telphone,string SuNo, string RiCardNo = "")
        {
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RUsersInfo info = infoBLL.GetRecordByNo(RUserNo);

                if (info == null)
                {
                    RUserNo = CommonHelper.Get_New12ByteGuid();
                    info = new RUsersInfo();
                    info.sRUserNo = RUserNo;
                    info.sCnName = RUserName;
                    info.sEnName = PinYinHelper.GetPinYinCode(RUserName).ToLower();
                    info.iAge = int.Parse(Age);
                    info.iSex = int.Parse(RUserSex);
                    info.sNation = "";
                    info.iCardType = 1;
                    info.sIdCardNo = IdNo;
                    info.sRiCardNo = RiCardNo;
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

                    infoBLL.AddNewRecord(info);
                }

                return RUserNo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //挂号信息队列报道
        public static string EnqueueRegistUser(RegistFlows regFlow, string serviceNo,string StafferNo)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "";
                string sCounterNos = "";
                string sWAreaNo = "";
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                RegistFlowsBLL rflowBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); 

                if (regFlow != null)
                {
                    regFlow = rflowBLL.GetRecordByNo(regFlow.sRFlowNo);
                    regFlow.sServiceNo = serviceNo;
                    regFlow.sStafferNo = StafferNo;

                    WaitArea wareaInfo = getWaitAreaNoByWAreaIndex(0, regFlow.sBranchNo);
                    if (wareaInfo != null)
                    {
                        sWAreaNo = wareaInfo.sWAreaNo;
                    }

                    sTicketNo = doGenerateTicketNo(regFlow.sServiceNo, regFlow.sStafferNo, regFlow.sBranchNo);

                    if (!string.IsNullOrEmpty(regFlow.sServiceNo))
                    {
                        sCounterNos = PageService.GetCounterNosByServiceNo(regFlow.sServiceNo, regFlow.sBranchNo);
                    }

                    sTFlowNo = InsertTicketFlow(sTicketNo, regFlow.sRUserNo, regFlow.sBranchNo);
                    if (!string.IsNullOrEmpty(sTFlowNo))
                    {
                        sPFlowNo = InsertProcessFlow(sTFlowNo, regFlow.sServiceNo, sCounterNos, regFlow.sStafferNo, regFlow.iRegistType,sWAreaNo, 0, regFlow.sBranchNo);
                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            regFlow.iRegistState = 1;
                            rflowBLL.UpdateRecord(regFlow);
                            return sTicketNo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        //更新数据显示状态
        public static void UpdateShowState1(string serviceNo, string StafferNo, string svalue = "1111111111")
        {
            if (!string.IsNullOrEmpty(serviceNo))
            {
                PublicHelper.SetStateValue(serviceNo, svalue);
            }
            if (!string.IsNullOrEmpty(StafferNo))
            {
                PublicHelper.SetStateValue(StafferNo, svalue);
            }
        }

        private static string InsertTicketFlow(string sTicketNo, string sRUserNo, string sBranchNo)
        {
            try
            {
                TicketFlowsBLL flowBoss = new TicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

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
                flowInfo.sAppCode = PublicHelper.Get_AppCode() + ";";

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

        private static string InsertProcessFlow(string sTFlowNo, string sServiceNo, string sCounterNos, string sStafferNo,int iRegistType, string sWaitAreaNo, int iWAreaIndex, string sBranchNo)
        {
            try
            {
                string sPFlowNo = CommonHelper.Get_New12ByteGuid();
                ProcessFlowsBLL flowBoss = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                flowInfo.iProcessState = PublicConsts.PROCSTATE_DIAGNOSIS;
                flowInfo.sProcessFormat = "";
                flowInfo.iProcessIndex = 0;
                flowInfo.iPriorityType = iRegistType - 1;
                flowInfo.iOrderWeight = 0;
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
                flowInfo.sAppCode = PublicHelper.Get_AppCode() + ";";

                if (flowBoss.AddNewRecord(flowInfo))
                {
                    //UpdateShowState(sServiceNo,sStafferNo);
                    return sPFlowNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static WaitArea getWaitAreaNoByWAreaIndex(int index, string sBranchNo)
        {
            try
            {
                int count = 0;
                WaitAreaBLL infoBoss = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
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
        private static string getCounterNosByServiceNo(string sServiceNo, string sBranchNo)
        {
            try
            {
                int count = 0;
                string sResult = "";
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
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

        private static string doGenerateTicketNo(string sServiceNo, string sStafferNo, string sBranchNo)
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
                SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsBLL vflowBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                if (!string.IsNullOrEmpty(sServiceNo))
                {
                    serviceInfo = PageService.GetServiceInfo(sServiceNo);
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
                        info.sAppCode = PublicHelper.Get_AppCode() + ";";

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
                        info.sAppCode = PublicHelper.Get_AppCode() + ";";

                        infoBLL.AddNewRecord(info);

                        sTicketNo = String.Format("{0:D3}", staffNum);
                    }
                }
                else
                {
                    //默认生成票号
                    ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
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


    }
}