using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.System.Controllers
{
    public class PermitsController : frmMainController
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


        //[(Message = "权限信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString()); 
                Condition = sWhere;

                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                PermitInfoCollections infoColl = infoBLL.GetAllRecordsByParentNoOrder("00000000", HttpUtility.HtmlDecode("---"));
                int totalCount = 1;

                PagerHelper pager = new PagerHelper(PageIndex, 1000, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();

                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
            }
            catch (Exception ex)
            { }
            return View();
        }


        private PermitInfoCollections GetPermitList()
        {
            PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            PermitInfoCollections infoColl = infoBLL.GetAllRecordsByParentNoOrder("00000000", HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;"));
              
            return infoColl;
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
        //[(Message = "权限信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                PermitInfo info = new PermitInfo();
                info.sPermitNo = CommonHelper.Get_New12ByteGuid(); 

                ViewBag.StackHolder = info;
                ViewBag.DownList = GetPermitList();

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "权限信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                PermitInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.DownList = GetPermitList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "权限信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sPermitNo"].ToString();
                string sPermitName = Request.Form["sPermitName"].ToString(); 
                string sParentNo = Request.Form["sParentNo"].ToString(); 
                int iOrderNo = Request.Form["iOrderNo"].ToInt(); 
                string sPFunction = Request.Form["sPFunction"].ToString();
                string sPPicture = Request.Form["sPPicture"].ToString(); 

                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                PermitInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new PermitInfo(); 
                    info.sPermitNo = sNo;
                    info.sPermitName = sPermitName;
                    info.sPermitCode = sNo;
                    info.sParentNo = sParentNo;
                    info.iOrderNo = iOrderNo;
                    info.sPFunction = sPFunction;
                    info.sPPicture = sPPicture; 

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
                    info.sPermitName = sPermitName;
                    info.sParentNo = sParentNo;
                    info.iOrderNo = iOrderNo;
                    info.sPFunction = sPFunction;
                    info.sPPicture = sPPicture;

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
        //[(Message = "权限信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "用户组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                PermitInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}