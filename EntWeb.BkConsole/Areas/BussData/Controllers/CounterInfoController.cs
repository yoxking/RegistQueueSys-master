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
    public class CounterInfoController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return " 1=1 ";
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
                Condition = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition += " And " + sWhere;
                }

                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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

        //语音
        public  VoiceInfoCollections getTtsList()
        {
            int count = 0;
            VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            VoiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");

            return infoColl;
        }
        //Led屏
        public LEDDisplayCollections getLedList()
        {
            int count = 0;
            LEDDisplayBLL infoBLL = new LEDDisplayBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            LEDDisplayCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");

            return infoColl;
        }
        //呼叫器
        public CallerInfoCollections getCallerList()
        {
            int count = 0;
            CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            CallerInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");

            return infoColl;
        }

        //
        // GET: /System/Role/Search 
        public override ActionResult Search()
        {
            sWhere = "1=1 ";

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add 
        public override ActionResult Add()
        {
            try
            {
                CounterInfo info = new CounterInfo();
                info.sCounterNo = CommonHelper.Get_New12ByteGuid();
                info.iCallerAddress = 1;
                info.iLedAddress = 1;

                ViewBag.StackHolder = info;
                ViewBag.TtsList = getTtsList();
                ViewBag.LedList = getLedList();
                ViewBag.CallerList = getCallerList();

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5 
        public override ActionResult Edit(string id)
        {
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.TtsList = getTtsList();
                ViewBag.LedList = getLedList();
                ViewBag.CallerList = getCallerList();
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

                string CounterNo = Request.Form["CounterNo"].ToString();
                string CounterName = Request.Form["CounterName"].ToString();
                string CounterAlias = Request.Form["CounterAlias"].ToString(); 
                string VoiceStylesNo = Request.Form["TtsStylesNo"].ToString();  
                string LedDisplayNo = Request.Form["LedDisplayNo"].ToString();
                int LedAddress = Request.Form["LedAddress"].ToInt();
                string CallerNo = Request.Form["CallerNo"].ToString();
                int CallerAddress = Request.Form["CallerAddress"].ToInt(); 
                int IsAutoLogon = Request.Form["IsAutoLogon"].ToInt(); 
                string Comments = Request.Form["Comments"].ToString();


                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfo info = infoBLL.GetRecordByNo(CounterNo);

                //新增操作
                if (info == null)
                {
                    info = new CounterInfo();
                    info.sCounterNo = CounterNo;
                    info.sCounterName = CounterName;
                    info.sCounterAlias = CounterAlias;
                    info.sServiceGroupValue = "";
                    info.sServiceGroupText = "";
                    info.sVoiceStyleNos = VoiceStylesNo; 
                    info.sLedDisplayNo = LedDisplayNo;
                    info.iLedAddress = LedAddress;
                    info.sCallerNo = CallerNo;
                    info.iCallerAddress = CallerAddress; 
                    info.iIsAutoLogon = IsAutoLogon;
                    info.sLogonStafferNo = "";
                    info.iLogonState = 0;
                    info.iPauseState = 0; 
                    info.sBranchNo = PublicHelper.Get_BranchNo();

                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";
                    info.sComments = Comments;

                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {
                    
                    info.sCounterName = CounterName;
                    info.sCounterAlias = CounterAlias; 
                    info.sVoiceStyleNos = VoiceStylesNo; 
                    info.sLedDisplayNo = LedDisplayNo;
                    info.iLedAddress = LedAddress;
                    info.sCallerNo = CallerNo;
                    info.iCallerAddress = CallerAddress; 
                    info.iIsAutoLogon = IsAutoLogon; 
                    info.sComments = Comments;

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
                json.Message = "保存信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5 
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /PubsData/Content/Detail/5 
        public override ActionResult Detail(string id)
        {
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}