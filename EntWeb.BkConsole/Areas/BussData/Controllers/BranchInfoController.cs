using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.BussData.Controllers
{
    public class BranchInfoController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return "";
                }
                return temp.ToString();
            }
        }

        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            return RedirectToAction("List");
        }

         
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = sWhere;

                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                BranchInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }
         
        public override ActionResult Search()
        {
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }
         
        public override ActionResult Add()
        {
            try
            {
                BranchInfo info = new BranchInfo();
                info.sBranchNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }
         
        public override ActionResult Edit(string id)
        {
            try
            {
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                BranchInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sBranchNo"].ToString();
                string sBranchName = Request.Form["sBranchName"].ToString();
                string sBranchCode = Request.Form["sBranchNo"].ToString();
                string iBranchType = Request.Form["iBranchType"].ToString();
                string sContacts = Request.Form["sContacts"].ToString();
                string sTelphone = Request.Form["sTelphone"].ToString();
                string sSummary = Request.Form["sSummary"].ToString();

                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                BranchInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new BranchInfo();
                    info.sBranchNo = sNo;
                    info.sBranchName = sBranchName;
                    info.sBranchCode = sBranchCode;
                    info.iBranchType = int.Parse(iBranchType);
                    info.sContacts = sContacts;
                    info.sTelphone = sTelphone;
                    info.sSummary = sSummary;


                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {

                        Init_SysParams(sNo);
                        Init_WaitingArea(sNo);
                        Init_OrganizInfo(sNo);
                        Init_TicketStyle(sNo);
                        Init_VoiceInfo(sNo);
                        Init_LEDDisplay(sNo);
                        Init_CallerInfo(sNo);
                        Init_CounterInfo(sNo);
                        Init_ServiceInfo(sNo);
                        Init_StafferInfo(sNo);

                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    info.sBranchName = sBranchName;
                    info.sBranchCode = sBranchCode;
                    info.iBranchType = int.Parse(iBranchType);
                    info.sContacts = sContacts;
                    info.sTelphone = sTelphone;
                    info.sSummary = sSummary;

                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
         
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        } 
        public override ActionResult Detail(string id)
        {
            try
            {
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                BranchInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private void Init_SysParams(string branchNo)
        {
            PublicHelper.SetParamValue(branchNo, "CallVoiceEnable", "1", "Others");
            PublicHelper.SetParamValue(branchNo, "CallVoiceFormat", "请护士到[科室名称]号窗口", "Others");
            PublicHelper.SetParamValue(branchNo, "CallVoiceStyle", "Microsoft Huihui Desktop - Chinese (Simplified)", "Others");
            PublicHelper.SetParamValue(branchNo, "CallVoiceVolume", "100", "Others");
            PublicHelper.SetParamValue(branchNo, "CallVoiceRate", "0", "Others");
            PublicHelper.SetParamValue(branchNo, "IdRdModel", "华旭", "Others");
            PublicHelper.SetParamValue(branchNo, "IdRdSPort", "USB1", "Others");
            PublicHelper.SetParamValue(branchNo, "IdRdBaudrate", "9600", "Others");
             
            PublicHelper.SetParamValue(branchNo, "RediagnosisInterval", "3", "Others");
            PublicHelper.SetParamValue(branchNo, "WorkingMode", "SERVICE", "Others");
            PublicHelper.SetParamValue(branchNo, "WaitAreaMode", "1", "Others");
            PublicHelper.SetParamValue(branchNo, "RegisteMode", "MANUAL", "Others");
            PublicHelper.SetParamValue(branchNo, "CallingMode", "MANUAL", "Others");
        }
        private void Init_WaitingArea(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                WaitArea info = new WaitArea();
                info.sWAreaNo = CommonHelper.Get_New12ByteGuid();
                info.sWAreaName = "综合等候大厅";
                info.iAreaIndex = 0;
                info.iAreaScale = 100;
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);

                info = new WaitArea();
                info.sWAreaNo = CommonHelper.Get_New12ByteGuid();
                info.sWAreaName = "科室等候区域";
                info.iAreaIndex = 1;
                info.iAreaScale = 5;
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch(Exception ex) { }
        }

        private void Init_OrganizInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                OrganizInfoBLL infoBLL = new OrganizInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                OrganizInfo info = new OrganizInfo();
                info.sOrganizNo = CommonHelper.Get_New12ByteGuid();
                info.sOrganizName = "默认科室";
                info.sParentNo = "00000000";
                info.iOrderNo = 1;
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";"; 

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_TicketStyle(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                TicketStyleBLL infoBLL = new TicketStyleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                TicketStyle info = new TicketStyle();
                info.sStyleNo = CommonHelper.Get_New12ByteGuid();
                info.sStyleName = "默认样式";
                info.iIsTemplet = 0;
                info.sTicketFormat = "";
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_VoiceInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                VoiceInfo info = new VoiceInfo();
                info.sTtsNo = CommonHelper.Get_New12ByteGuid();
                info.sTtsName = "默认语音";
                info.sVoice = "Microsoft Huihui Desktop - Chinese (Simplified)";
                info.iRate = 0;
                info.iVolume = 100;
                info.sFormatCalling = "请[TicketNo]到[CounterName]科室就诊";
                info.sFormatWaiting = "请[TicketNo]等候就诊";
                info.sPreMusic = ".\\Uploads\\dingdong.wav";
                info.sPostMusic = ".\\Uploads\\dingdong.wav";
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_LEDDisplay(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                LEDDisplayBLL infoBLL = new LEDDisplayBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LEDDisplay info = new LEDDisplay();
                info.sDisplayNo = CommonHelper.Get_New12ByteGuid();
                info.sDisplayName = "LED显示1";
                info.sCounterNos = "";
                info.sLedModel = "Hzhx13";
                info.iPhyAddr = 1;
                info.sProtocol = "NET";
                info.sSerialPort = "COM1";
                info.iBaudrate = 9600;
                info.sIpAddress = "192.168.1.10";
                info.sLocalPort = "1";
                info.sParamFormat = "";
                info.iTimeoutSec = 10;
                info.sDisplayFormat = "";
                info.iDisplayLength = 30;
                info.sPowerOnTip = "";
                info.sInServiceTip = "";
                info.sOnPauseTip = "";
                info.sTimeoutTip = "";
                info.iUpdateFlag = 0;
                info.dUpdateTime = DateTime.Now;
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_CallerInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CallerInfo info = new CallerInfo();
                info.sCallerNo = CommonHelper.Get_New12ByteGuid();
                info.sCallerName = "呼叫器1";
                info.sProtocol = "中意";
                info.sSerialPort = "COM1";
                info.sCommMode = "serialport"; 
                info.iBaudrate = 9600;
                info.iPhyAddr = 1;
                info.sEvalorNo = ""; 
                info.iTimeoutSec = 30; 
                info.iUpdateFlag = 0;
                info.dUpdateTime = DateTime.Now;
                info.iCheckState = 0;
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_CounterInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfo info = new CounterInfo();
                info.sCounterNo = CommonHelper.Get_New12ByteGuid();
                info.sCounterName = "1";
                info.sCounterAlias = "科室1";
                info.sServiceGroupValue = "";
                info.sServiceGroupText = "";
                info.sVoiceStyleNos = "";
                info.sLedDisplayNo = "";
                info.iLedAddress = 1;
                info.sCallerNo = "";
                info.iCallerAddress = 0;
                info.iIsAutoLogon = 0;
                info.iLogonState = 0;
                info.sLogonStafferNo = "";
                info.iPauseState =0;
                info.iCalledNum = 0; 
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_ServiceInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfo info = new ServiceInfo();
                info.sServiceNo = CommonHelper.Get_New12ByteGuid();
                info.sServiceName = "业务1";
                info.sServiceAlias = "1";
                info.sServiceType = "A";
                info.iStartNum = 1;
                info.iEndNum = 0;
                info.iAlarmNum = 0;
                info.iDigitLength = 3;
                info.sWorkflowValue ="";
                info.sWorkflowText ="";
                info.sTicketButtonFmt = "";
                info.sTicketStyleNo = "";
                info.iAMLimit = 0;
                info.dAMStartTime = DateTime.Now;
                info.dAMEndTime = DateTime.Now;
                info.iAMTotal = 0;
                info.iPMLimit = 0;
                info.dPMStartTime = DateTime.Now;
                info.dPMEndTime = DateTime.Now;
                info.iPMTotal = 0;
                info.iWeekLimit = 0;
                info.sWeekDays = "";
                info.iPrintPause = 0;
                info.sParentNo = "00000000";
                info.iHaveChild = 0;
                info.iIsShowDialog = 0;
                info.sShowDialogName = ""; 
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }

        private void Init_StafferInfo(string branchNo)
        {
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo info = new StafferInfo();
                info.sStafferNo = CommonHelper.Get_New12ByteGuid();
                info.sStafferName = "护士员1";
                info.sLoginId = "1001";
                info.sPassword = "1001";
                info.sCounterNo = "";
                info.sOrganizNo = "";
                info.sOrganizName = "";
                info.sStarLevel = "五星";
                info.sHeadPhoto = "";
                info.sRanks = "";
                info.sPosts = "";
                info.sSummary = ""; 
                info.sBranchNo = branchNo;
                info.sComments = "";

                info.sAddOptor = sSuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = sSuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                infoBLL.AddNewRecord(info);
            }
            catch (Exception ex) { }
        }
    }
}