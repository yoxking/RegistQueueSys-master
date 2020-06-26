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
    public class ServiceInfoController : frmMainController
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


        //[(Message = "业务信息表(List)")]
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

                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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

        //
        // GET: /System/Role/Search
        //[(Message = "信息查询(Search)")]
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
        //[(Message = "业务信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                ServiceInfo info = new ServiceInfo();
                info.sServiceNo = CommonHelper.Get_New12ByteGuid();
                info.sServiceType = "A";
                info.iStartNum = 1;
                info.iDigitLength = 3;
                info.iEndNum = 1000;
                info.iAlarmNum = 1000;

                ViewBag.StackHolder = info;
                ViewBag.ServiceList = getServiceList();
                ViewBag.TStyleList = getTStyleList();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "业务信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.ServiceList = getServiceList();
                ViewBag.TStyleList = getTStyleList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private ServiceInfoCollections getServiceList()
        { 
            int count = 0;
            ServiceInfoBLL infoBll = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBll.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");
        }

        private TicketStyleCollections getTStyleList()
        {
            int count = 0;
            TicketStyleBLL infoBll = new TicketStyleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBll.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "'");
        }

        [HttpPost]
        //[(Message = "业务信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string ServiceNo = Request.Form["ServiceNo"].ToString();
                string ServiceName = Request.Form["ServiceName"].ToString();
                string ServiceAlias = Request.Form["ServiceAlias"].ToString();
                string ServiceType = Request.Form["ServiceType"].ToString();
                int StartNum = int.Parse(Request.Form["StartNum"].ToString());
                int EndNum = int.Parse(Request.Form["EndNum"].ToString());
                int AlarmNum = int.Parse(Request.Form["AlarmNum"].ToString());
                int DigitLength = int.Parse(Request.Form["DigitLength"].ToString()); 
                string TicketStyleNo = Request.Form["TicketStyleNo"].ToString(); 
                string ParentNo = Request.Form["ParentNo"].ToString(); 
                string Comments = Request.Form["Comments"].ToString();

                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfo info = infoBLL.GetRecordByNo(ServiceNo);

                //新增操作
                if (info == null)
                {
                    info = new ServiceInfo();

                    info.sServiceNo = ServiceNo;
                    info.sServiceName = ServiceName;
                    info.sServiceAlias = ServiceAlias;
                    info.sServiceType = ServiceType;
                    info.iStartNum = StartNum;
                    info.iEndNum = EndNum;
                    info.iAlarmNum = AlarmNum;
                    info.iDigitLength = DigitLength;
                    info.sWorkflowValue = "";
                    info.sWorkflowText = "";
                    info.sTicketButtonFmt = "";
                    info.sTicketStyleNo = TicketStyleNo;
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
                    info.sParentNo = ParentNo;
                    info.iHaveChild = 0;
                    info.iIsShowDialog = 0;
                    info.sShowDialogName = "Null";
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
                    info.sServiceName = ServiceName;
                    info.sServiceAlias = ServiceAlias;
                    info.sServiceType = ServiceType;
                    info.iStartNum = StartNum;
                    info.iEndNum = EndNum;
                    info.iAlarmNum = AlarmNum;
                    info.iDigitLength = DigitLength; 
                    info.sTicketStyleNo = TicketStyleNo; 
                    info.sParentNo = ParentNo; 
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
        //[(Message = "员工信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "业务组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}