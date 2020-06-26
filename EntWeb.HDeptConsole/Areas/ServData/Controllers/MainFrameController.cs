using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Text;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.ServData.Controllers
{
    public class MainFrameController : frmMainController
    {
        public MainFrameController()
        {
            viewExpress = PublicHelper.Get_ViewExpress();
            viewExpress.ViewTag = "ServMFrame";
            ViewBag.ViewExpress = viewExpress;
        }

        // GET: BussData/MainFrame 
        override
        public ActionResult Index()
        {
                ViewBag.ItemList = PageService.GetServiceList(true);
                return View();
            
        }

        public ActionResult getDataList_Queuing(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                DateTime workDate = DateTime.Now;
                TableData tdata = new TableData();

                string strWhere = " DataFlag=0 And BranchNo='" + PublicHelper.Get_BranchNo() + "' And ProcessState Between " + PublicConsts.PROCSTATE_OUTQUEUE + " And " + PublicConsts.PROCSTATE_WAITING + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    //strWhere += " And ServiceNo='" + condition + "' ";
                    strWhere += " And " + condition ;
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = pageIndex;
                s_model.iPageSize = pageSize;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsCollections infoColl = infoBLL.GetRecordsByPaging(s_model);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertTicketFlows(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getDataList_Waiting(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                DateTime workDate = DateTime.Now;
                TableData tdata = new TableData();

                string strWhere = " DataFlag=0 And  BranchNo='" + PublicHelper.Get_BranchNo() + "' And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_CALLING + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    //strWhere += " And ServiceNo='" + condition + "' ";
                    strWhere += " And " + condition;
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = pageIndex;
                s_model.iPageSize = pageSize;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsCollections infoColl = infoBLL.GetRecordsByPaging(s_model);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertTicketFlows(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getDataList_Finished(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                DateTime workDate = DateTime.Now;
                TableData tdata = new TableData();

                string strWhere = " DataFlag=1 And  BranchNo='" + PublicHelper.Get_BranchNo() + "' And ProcessState Between " + PublicConsts.PROCSTATE_PROCESSING + " And " + PublicConsts.PROCSTATE_ARCHIVE + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    //strWhere += " And ServiceNo='" + condition + "' ";
                    strWhere += " And " + condition;
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = pageIndex;
                s_model.iPageSize = pageSize;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = "ProcessedTime";
                s_model.sOrderType = "Desc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewTicketFlowsCollections infoColl = infoBLL.GetRecordsByPaging(s_model);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertTicketFlows(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getDataList_Regist(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                TableData tdata = new TableData();
                //string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "'  And RegistState=0 And  RegistDate Between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "'  And RegistState=0 ";

                if (!string.IsNullOrEmpty(condition) && !("99999999").Equals(condition))
                {
                    //strWhere += " And ServiceNo='" + condition + "' ";
                    strWhere += " And " + condition;
                }

                ViewRegistFlowsBLL infoBLL = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, pageIndex, pageSize, strWhere);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertRegistList(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        #region 选择窗口 
        public ActionResult PriorityType()
        {
            ViewBag.InfoList = PageService.GetPriorityList();
            return View();
        }

        public ActionResult SelectService()
        {
            ViewBag.InfoList = PageService.GetServiceList();
            return View();
        }

        public ActionResult DelayTime()
        {
            return View();
        }

        public ActionResult RegistUser()
        {
            ViewBag.ServiceList = PageService.GetServiceList();
            ViewBag.WTimeList = PageService.GetWorkTimeList();
            return View();
        }
        #endregion

        #region  等候人员操作

        //优先
        public ActionResult doSetPriority(string ids, string priorityType)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.iWAreaIndex = 0;
                        info.iOrderWeight = 9;
                        info.iPriorityType = int.Parse(priorityType);
                        info.iDelayTimeValue = 0;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info);

                        //过号优先=》过号处理
                        /////////////////////////// 
                        if (info.iPriorityType == PublicConsts.PRIORITY_TYPE2)
                        {
                            info = infoBLL.GetRecordByNo(id);
                            info.iDataFlag = 1;
                            info.iWAreaIndex = 0;
                            info.iOrderWeight = 0;
                            info.iPriorityType = int.Parse(priorityType);
                            info.iDelayTimeValue = 0;
                            info.dFinishTime = DateTime.Now;
                            info.iProcessState = PublicConsts.PROCSTATE_NONARRIVAL;
                            info.dProcessedTime = DateTime.Now;
                            info.dModDate = DateTime.Now;

                            infoBLL.UpdateRecord(info);
                        }
                        ////////////////////////////
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //分诊
        public ActionResult doSetTriage(string ids, string serviceNo)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;
                string sCounterNos = PageService.GetCounterNosByServiceNo(serviceNo, PublicHelper.Get_BranchNo());

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.iWAreaIndex = 0;
                        info.iProcessState = PublicConsts.PROCSTATE_TRIAGE;
                        info.sServiceNo = serviceNo;
                        info.sCounterNos = sCounterNos;
                        info.sStafferNo = "";
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //取消分诊
        public ActionResult doSetUntriage(string ids)
        {
            try
            {
                TableData tdata = new TableData();
                int count = 0;

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = null;

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //转诊
        public ActionResult doSetExchange(string ids, string serviceNo)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;
                string sCounterNos = PageService.GetCounterNosByServiceNo(serviceNo, PublicHelper.Get_BranchNo());

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.iWAreaIndex = 0;
                        info.iProcessState = PublicConsts.PROCSTATE_EXCHANGE;
                        info.sServiceNo = serviceNo;
                        info.sCounterNos = sCounterNos;
                        info.sStafferNo = "";
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //挂起
        public ActionResult doSetHangup(string ids)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 1;
                        info.iWAreaIndex = 0;
                        info.dFinishTime = DateTime.Now;
                        info.iProcessState = PublicConsts.PROCSTATE_HANGUP;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //延迟
        public ActionResult doSetDelayTime(string ids, string delayTime)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.iWAreaIndex = 0;
                        //info.iProcessState = PublicConsts.PROCSTATE_DELAY;
                        info.iDelayType = 0;
                        info.iDelayTimeValue = int.Parse(delayTime);
                        info.dProcessedTime = DateTime.Now.AddMinutes(int.Parse(delayTime));
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //过号
        public ActionResult doSetPassticket(string ids)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 1;
                        info.iWAreaIndex = 0;
                        info.dFinishTime = DateTime.Now;
                        info.iProcessState = PublicConsts.PROCSTATE_NONARRIVAL;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //绿色通道
        public ActionResult doSetGreenChannel(string ids)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 1;
                        info.iWAreaIndex = 0;
                        info.dFinishTime = DateTime.Now;
                        info.iProcessState = PublicConsts.PROCSTATE_GREENCHANNEL;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //按序叫号
        public ActionResult doAutoWaitticket(string ids)
        {
            try
            {
                TableData tdata = new TableData();
                int count = 0;

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = null;

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //指定叫号
        public ActionResult doCallSpecTicket(string ids)
        {
            try
            {
                //string sResult = IUserContext.OnExecuteCommand_Xp("doCallSpecTicket", new string[] { ids });

                //if (string.IsNullOrEmpty(sResult))
                //{
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region  已就诊人员操作
        //复诊
        public ActionResult doSetRediagnosis(string ids)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.sWaitAreaNo = PageService.GetWaitAreaNoByWAreaIndex(0).sWAreaNo;
                        info.iWAreaIndex = 0;
                        info.iOrderWeight = 2;
                        info.iPriorityType = PublicConsts.PRIORITY_TYPE0;
                        info.iDelayTimeValue = 0;
                        info.iProcessState = PublicConsts.PROCSTATE_REDIAGNOSIS;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }

        //初诊
        public ActionResult doSetDiagnosis(string ids)
        {
            try
            {
                ProcessFlowsBLL infoBLL = new ProcessFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ProcessFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        info.iDataFlag = 0;
                        info.sWaitAreaNo = PageService.GetWaitAreaNoByWAreaIndex(0).sWAreaNo;
                        info.iWAreaIndex = 0;
                        info.iOrderWeight = 0;
                        info.iPriorityType = PublicConsts.PRIORITY_TYPE0;
                        info.iDelayTimeValue = 0;
                        info.iProcessState = PublicConsts.PROCSTATE_DIAGNOSIS;
                        info.dProcessedTime = DateTime.Now;
                        info.dModDate = DateTime.Now;

                        infoBLL.UpdateRecord(info); 
                    }
                }

                return Json(new ResultData("200", "操作成功!"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region  未报到人员操作
        //手工录入
        public ActionResult doUpdateRUser(string serviceNo, string workTime, string ruserName, string ruserSex, string idNo, string age,string telphone)
        {
            try
            {
                string ticketNo = "";
                string SuNo = ((LoginerInfo)Session["loginUser"]).UserNo;

                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RegistFlows info = new RegistFlows();

                info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                info.sTicketNo = "";
                info.iDataFlag = 0;
                info.sRUserNo = BusinessHelper.AddRUserInfo("aa",ruserName, ruserSex, idNo, age, telphone, SuNo);
                info.iRegistType = PublicConsts.REGISTETYPE1;
                info.sDataFrom = "手动录入";
                info.dRegistDate = DateTime.Now;
                info.sServiceNo = serviceNo; 
                info.sStafferNo = "";
                info.iWorkTime = int.Parse(workTime);
                info.dStartDate = DateTime.Now;
                info.dEnditDate = DateTime.Now;
                info.iRegistState = 0;

                info.sBranchNo = PublicHelper.Get_BranchNo();
                info.sAddOptor = SuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = SuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sComments = "";
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                if (infoBLL.AddNewRecord(info))
                {
                    ticketNo = BusinessHelper.EnqueueRegistUser(info, serviceNo, "");

                    PrintTicket(PageService.GetServiceName(info.sServiceNo)+ticketNo);

                    return Json(new ResultData("200", "操作成功!您的票号是:" + ticketNo), JsonRequestBehavior.AllowGet);
                }

                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }

        //批量报到
        public ActionResult doRegisterMore(string ids, string serviceNo, string StafferNo)
        {
            try
            {
                string ticketNo = "";
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RegistFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        ticketNo=BusinessHelper.EnqueueRegistUser(info, serviceNo, StafferNo);

                        PrintTicket(PageService.GetServiceName(serviceNo) + ticketNo);
                    }
                }


                return Json(new ResultData("200", "操作成功!您的票号是:" + ticketNo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //扫描报到
        public ActionResult doRegisterScan(string ids,string serviceNo,string StafferNo)
        {
            try
            {
                string ticketNo = "";
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RegistFlows info = null;

                string[] idArr = ids.Split(';');
                foreach (string id in idArr)
                {
                    info = infoBLL.GetRecordByNo(id);
                    if (info != null)
                    {
                        ticketNo = BusinessHelper.EnqueueRegistUser(info, serviceNo, StafferNo);

                        PrintTicket(PageService.GetServiceName(serviceNo) + ticketNo);
                    }
                }
                 
                return Json(new ResultData("200", "操作成功!您的票号是:" + ticketNo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResultData("400", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        private void PrintTicket(string tickerNo)
        {
            bool printFlag = bool.Parse(PublicHelper.GetConfigValue("PrintFlag"));
            if (printFlag)
            {
                string printerName = PublicHelper.GetConfigValue("PrintName");

                StringBuilder sb = new StringBuilder();
                //sb.Append("-------------------------------------\n");
                sb.Append("\n\n");
                sb.Append(" 您的票号：" + tickerNo + "\n");
                sb.Append(" -------------------------------------\n");
                sb.Append(" 请在等候区耐心等候叫号！\n");
                sb.Append("\n\n");

                PrintHelper.PrintString(sb.ToString(), printerName);
            }
        }
    }
}