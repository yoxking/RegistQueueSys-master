using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.System.Controllers
{
    public class LogsController : frmMainController
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


        //[(Message = "日志信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString()); 
                Condition = sWhere;

                LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LogsInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
        //[(Message = "日志信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                //LogsInfo info = new LogsInfo();
                //info.sLogNo = CommonHelper.Get_New12ByteGuid(); 

                //ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "日志信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                //LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                //LogsInfo info = infoBLL.GetRecordByNo(id);

                //ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "日志信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                //string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
                 
                //SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                //SUsersInfo info = infoBLL.GetRecordByNo(sNo);

                ////新增操作
                //if (info == null)
                //{
                //    info = new SUsersInfo();
                //    info.sSuNo = sNo; 

                //    info.sAddOptor = sSuNo;
                //    info.dAddDate = DateTime.Now;
                //    info.sModOptor = sSuNo;
                //    info.dModDate = DateTime.Now;
                //    info.iValidityState = 1;
                //    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                //    if (infoBLL.AddNewRecord(info))
                //    {
                //        json.Message = "保存成功";
                //        json.Status = "Success";
                //    }
                //}
                ////更新操作
                //else
                //{
                //    info.sLoginID = sLoginID; 

                //    info.sModOptor = sSuNo;
                //    info.dModDate = DateTime.Now;

                //    if (infoBLL.UpdateRecord(info))
                //    {
                //        json.Message = "保存成功";
                //        json.Status = "Success";
                //    }
                //}
            }
            catch (Exception ex)
            {
                json.Message = "保存人员信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

         

        // GET: /System/Role/Delete/5
        //[(Message = "日志信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "日志信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                LogsInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}