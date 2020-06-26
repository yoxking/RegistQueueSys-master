using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class IRegistService
    {
        private volatile static IRegistService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;

        public static IRegistService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IRegistService();
                }
            }
            return _instance;
        }

        public void StartRegistFlows()
        {
            string registeMode = IUserContext.GetParamValue(IPublicConsts.DEF_REGISTEMODE, "Others");

            //if (registeMode.Equals("AUTO"))
            //{
            //    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And RegistState=0 ";
                //string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And RegistType=1 And RegistState=0 ";

                SqlModel s_model = null;
                RegistFlowsBLL registBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlowsCollections registColl = null;
            //RegistFlows tempFlow = null;

            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And  RegistType=2 And  RegistState=0  ";
            //自动报到模式
            if (registeMode.Equals("AUTO"))
            {
                sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And  RegistState=0 ";
            }

            MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "注册服务启动完成...");

                while (!isQuitFlag)
                { 

                    s_model = new SqlModel();

                    s_model.iPageNo = 1;
                    s_model.iPageSize = 100;
                    s_model.sFields = "*";
                    s_model.sCondition = sWhere + " And RegistDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    s_model.sOrderField = " RegistDate Asc,ID ";
                    s_model.sOrderType = "Asc";
                    s_model.sTableName = "RegistFlows";

                    registColl = registBLL.GetRecordsByPaging(s_model);

                    if (registColl != null && registColl.Count > 0)
                    {
                        foreach (RegistFlows registFlow in registColl)
                        {
                        //网上预约信息医生 =》业务
                            if (registFlow.iRegistType == 2 && (!string.IsNullOrEmpty(registFlow.sStafferNo)))
                        {
                            registFlow.sServiceNo = IBusinessHelper.getServiceNoByStafferNo(registFlow.sStafferNo);
                        }

                        //registFlow.sStafferNo = "from:" + IUserContext.GetBranchNo();

                        if (!hasProcessFlowsByRUserNo(registFlow.sServiceNo, registFlow.sStafferNo, registFlow.sRUserNo, registFlow.sBranchNo))
                            {
                                IBusinessHelper.EnqueueRegistUser(registFlow);

                                ///////////////////////////璧山
                                //tempFlow = getRegistFlowByRegistType();
                                //if (tempFlow != null)
                                //{
                                //    IBusinessHelper.EnqueueRegistUser(tempFlow);
                                //}
                                //////////////////////////
                            }
                        }
                    }

                    Thread.Sleep(3000);
                } 

            //string registeMode = IUserContext.GetParamValue(IPublicConsts.DEF_REGISTEMODE, "Others");

            //if (registeMode.Equals("AUTO"))
            //{
            //    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And RegistState=0 ";
            //    //string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And RegistType=1 And RegistState=0 ";

            //    SqlModel s_model = null;
            //    RegistFlowsBLL registBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            //    RegistFlowsCollections registColl = null;
            //    //RegistFlows tempFlow = null;

            //    //string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And  RegistType=2 And  RegistState=0  ";
            //    ////自动报到模式
            //    //if (registeMode.Equals("AUTO"))
            //    //{
            //    //    sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And  RegistState=0 ";
            //    //}

            //    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "注册服务启动完成...");

            //    while (true)
            //    {
            //        if (isQuitFlag)
            //        {
            //            break;
            //        }

            //        s_model = new SqlModel();

            //        s_model.iPageNo = 1;
            //        s_model.iPageSize = 100;
            //        s_model.sFields = "*";
            //        s_model.sCondition = sWhere + " And RegistDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            //        s_model.sOrderField = " RegistDate Asc,ID ";
            //        s_model.sOrderType = "Asc";
            //        s_model.sTableName = "RegistFlows";

            //        registColl = registBLL.GetRecordsByPaging(s_model);

            //        if (registColl != null && registColl.Count > 0)
            //        {
            //            foreach (RegistFlows registFlow in registColl)
            //            {
            //                //网上预约信息医生=》业务
            //                //if (registFlow.iRegistType == 2 && (!string.IsNullOrEmpty(registFlow.sStafferNo)))
            //                //{
            //                //    registFlow.sServiceNo = IBusinessHelper.getServiceNoByStafferNo(registFlow.sStafferNo);
            //                //}

            //                //registFlow.sStafferNo = "from:" + IUserContext.GetBranchNo();

            //                if (!hasProcessFlowsByRUserNo(registFlow.sServiceNo, registFlow.sStafferNo, registFlow.sRUserNo, registFlow.sBranchNo))
            //                {
            //                    IBusinessHelper.EnqueueRegistUser(registFlow);

            //                    ///////////////////////////璧山
            //                    //tempFlow = getRegistFlowByRegistType();
            //                    //if (tempFlow != null)
            //                    //{
            //                    //    IBusinessHelper.EnqueueRegistUser(tempFlow);
            //                    //}
            //                    //////////////////////////
            //                }
            //            }
            //        }

            //        Thread.Sleep(3000);
            //    }
            //}
        }

        #region  返回预约 璧山
        private RegistFlows getRegistFlowByRegistType()
        {
            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And RegistType=2 And RegistState=0 ";
            SqlModel s_model = new SqlModel();

            s_model.iPageNo = 1;
            s_model.iPageSize = 1;
            s_model.sFields = "*";
            s_model.sCondition = sWhere + " And RegistDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            s_model.sOrderField = " RegistDate Asc,ID ";
            s_model.sOrderType = "Asc";
            s_model.sTableName = "RegistFlows";


            RegistFlowsBLL registBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            RegistFlowsCollections registColl =  registBLL.GetRecordsByPaging(s_model);

            if (registColl != null && registColl.Count > 0)
            {
                return registColl.GetFirstOne();
            }

            return null;
        }
        #endregion


        private bool hasProcessFlowsByRUserNo(string serviceNo, string stafferNo, string ruserNo, string branchNo)
        {
            try
            {
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                int count = infoBoss.GetCountByCondition("  ServiceNo='" + serviceNo + "' And  StafferNo='" + stafferNo + "' And RUserNo='" + ruserNo + "' And BranchNo='" + branchNo + "' And AddDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ");

                 return (count > 0 ? true : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void StopRegistFlows()
        {
            isQuitFlag = true;
        }


        //按预约编号批量扫描报到
        public string doRegistScan(string ids)
        {
            try
            {
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null && info.iRegistState < 1)
                    {
                        if (!string.IsNullOrEmpty(IBusinessHelper.EnqueueRegistUser(info)))
                        {
                            info.iRegistState = 1;
                            info.dModDate = DateTime.Now;

                            infoBLL.UpdateRecord(info);
                        }
                    }
                }
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }


        //按预约编号扫描报到
        public string doRegistScanByRFlowNo(string rflowNo)
        {
            try
            {  
                string sResult = "";
                int count = 0;
                RegistFlowsCollections infoColl = null;
                RegistFlowsBLL rflowBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlows regFlow = rflowBLL.GetRecordByNo(rflowNo);

                if (regFlow == null)
                {
                    infoColl = rflowBLL.GetRecordsByPaging(ref count, 1, 1, "Comments='" + rflowNo + "'");
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        regFlow = infoColl.GetFirstOne(); 
                    }
                }

                if (regFlow != null)
                {
                    sResult = IBusinessHelper.EnqueueRegistUser(regFlow);
                    if (!string.IsNullOrEmpty(sResult))
                    { 
                        regFlow.iRegistState = 1;
                        regFlow.dModDate = DateTime.Now;

                        rflowBLL.UpdateRecord(regFlow);
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //扫描报到业务（按病人编号，业务名称）
        public string doRegistScanByRuserNo(string rflowNo, string serviceName, string StafferName)
        {
            try
            {
                string sResult = "";
                int count = 0;
                string sBranchNo = IUserContext.GetBranchNo();

                ServiceInfo serviceInfo = null;
                StafferInfo stafferInfo = null;
                RegistFlowsCollections infoColl = null;
                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlowsBLL rflowBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                RegistFlows regFlow = rflowBLL.GetRecordByNo(rflowNo);
                if (regFlow == null)
                {
                    infoColl = rflowBLL.GetRecordsByPaging(ref count, 1, 1, "Comments='" + rflowNo + "'");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        regFlow = infoColl.GetFirstOne();
                    }
                }

                if (regFlow != null)
                {
                    RUsersInfoBLL ruserBLL = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RUsersInfo ruser = ruserBLL.GetRecordByNo(regFlow.sRUserNo);

                    if (ruser != null)
                    {
                        serviceInfo = IBusinessHelper.getServiceNoByServiceName(serviceName);
                        stafferInfo = IBusinessHelper.getStafferNoByStafferName(StafferName);
                        string sRUserNo = ruser.sRUserNo;

                        string sTicketNo = "";
                        string sTFlowNo = "";
                        string sPFlowNo = "";
                        string sWAreaNo = "";
                        string serviceNo = "";
                        string stafferNo = "";
                        string counterNos = "";
                        DateTime workDate = DateTime.Now.AddMinutes(30);

                        WaitArea wareaInfo = IBusinessHelper.getWaitAreaNoByWAreaIndex(0, sBranchNo);
                        if (wareaInfo != null)
                        {
                            sWAreaNo = wareaInfo.sWAreaNo;
                        }

                        if (serviceInfo != null)
                        {
                            serviceNo = serviceInfo.sServiceNo;
                            counterNos = IPublicHelper.GetCounterNosByServiceNo(serviceNo, serviceInfo.sBranchNo);
                        }
                        if (stafferInfo != null)
                        {
                            stafferNo = stafferInfo.sStafferNo;
                        }

                        sTicketNo = IBusinessHelper.doGenerateTicketNo(serviceNo, stafferNo, sBranchNo);

                        sTFlowNo = IBusinessHelper.InsertTicketFlow(sTicketNo, sRUserNo, sBranchNo);
                        if (!string.IsNullOrEmpty(sTFlowNo))
                        {
                            sPFlowNo = IBusinessHelper.InsertProcessFlow(sTFlowNo, serviceNo, counterNos, stafferNo, IPublicConsts.REGISTETYPE1, sWAreaNo, 0, sBranchNo);
                            sResult = sPFlowNo;
                        }
                    }

                    if (!string.IsNullOrEmpty(sResult))
                    {
                        regFlow.iRegistState = 1;
                        regFlow.dModDate = DateTime.Now;

                        rflowBLL.UpdateRecord(regFlow);
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        //按预约编号药房扫描报到
        public string doRegistScanByRecipeNo(string recipeNo)
        {
            try
            {
                string sResult = "";
                int count = 0;
                RecipeFlowsCollections infoColl = null;
                RecipeFlowsBLL rflowBLL = new RecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RecipeFlows recFlow = rflowBLL.GetRecordByNo(recipeNo);

                if (recFlow == null)
                {
                    infoColl = rflowBLL.GetRecordsByPaging(ref count, 1, 10, "RegistNo='" + recipeNo + "' And RecipeState=0 And ProcessState=0");
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        foreach (RecipeFlows rflow in infoColl)
                        {
                            rflow.iRecipeState = 1;
                            rflow.iProcessState = 0;
                            rflow.dModDate = DateTime.Now;

                            if (rflowBLL.UpdateRecord(rflow))
                            {
                                sResult += rflow.sRFlowNo + ",";
                            }
                        }
                    }
                }
                else
                {
                    recFlow.iRecipeState = 1;
                    recFlow.iProcessState = 0;
                    recFlow.dModDate = DateTime.Now;

                    rflowBLL.UpdateRecord(recFlow);

                    sResult = recFlow.sRFlowNo;
                }

                sResult = sResult.Trim(',');
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
