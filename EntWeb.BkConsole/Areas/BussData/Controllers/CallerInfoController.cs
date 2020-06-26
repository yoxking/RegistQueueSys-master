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
    public class CallerInfoController : frmMainController
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

                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CallerInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
                CallerInfo info = new CallerInfo();
                info.sCallerNo = CommonHelper.Get_New12ByteGuid();
                info.iPhyAddr = 1;
                info.iBaudrate = 9600;

                ViewBag.StackHolder = info;
                ViewBag.EvalList = getEvaluatorList();
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
                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CallerInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.EvalList = getEvaluatorList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private EvaluatorInfoCollections getEvaluatorList()
        {
            int count = 0;
            EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string CallerNo = Request.Form["CallerNo"].ToString();
                string CallerName = Request.Form["CallerName"].ToString();
                string Protocol = Request.Form["Protocol"].ToString();
                string SerialPort = Request.Form["SerialPort"].ToString();
                string CommMode = Request.Form["CommMode"].ToString();
                int Baudrate = Request.Form["Baudrate"].ToInt();
                int PhyAddr = Request.Form["PhyAddr"].ToInt(); 
                string EvalorNo = Request.Form["EvalorNo"].ToString();  
                string Comments = Request.Form["Comments"].ToString();


                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CallerInfo info = infoBLL.GetRecordByNo(CallerNo);

                //新增操作
                if (info == null)
                {
                    info = new CallerInfo();
                    info.sCallerNo = CallerNo;
                    info.sCallerName = CallerName;
                    info.sProtocol = Protocol;
                    info.sSerialPort = SerialPort;
                    info.sCommMode = CommMode;
                    info.iBaudrate = Baudrate;
                    info.iPhyAddr = PhyAddr;
                    info.sEvalorNo = EvalorNo; 
                    info.iTimeoutSec = 30;
                    info.iUpdateFlag = 0;
                    info.dUpdateTime = DateTime.Now;
                    info.iCheckState = 1;
                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sComments = Comments;


                    info.sAddOptor = sSuNo;
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = sSuNo;
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        json.Message = "保存成功";
                        json.Status = "Success";
                    }
                }
                //更新操作
                else
                {

                    info.sCallerName = CallerName;
                    info.sProtocol = Protocol;
                    info.sSerialPort = SerialPort;
                    info.sCommMode = CommMode;
                    info.iBaudrate = Baudrate;
                    info.iPhyAddr = PhyAddr;
                    info.iTimeoutSec = 30;
                    info.sEvalorNo = EvalorNo;
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
                json.Message = "保存人员信息发生内部错误！" + ex.Message;
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
                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CallerInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}