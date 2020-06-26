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
    public class ContentClassController : frmMainController
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

                ContentClassBLL infoBll=new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentClassCollections infoColl = infoBll.GetRecordsByPaging(ref PageCount, PageIndex, PageSize, Condition);
                int totalCount = infoBll.GetCountByCondition(Condition);
                 
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
                ContentClass info = new ContentClass();
                info.sClassNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info;
                ViewBag.CntClassList = getCntClassList();
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
                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentClass info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.CntClassList = getCntClassList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private ContentClassCollections getCntClassList()
        {
            ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetAllRecordsByParentNoOrder("00000000", "--");
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string ClassNo = Request.Form["ClassNo"].ToString();
                string ClassName = Request.Form["ClassName"].ToString();
                string ParentNo = Request.Form["ParentNo"].ToString();
                int OrderNo = int.Parse(Request.Form["OrderNo"].ToString());
                string Comments = Request.Form["Comments"].ToString();


                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentClass info = infoBLL.GetRecordByNo(ClassNo);

                //新增操作
                if (info == null)
                {
                    info = new ContentClass();


                    info.sClassNo = ClassNo;
                    info.sClassName = ClassName;
                    info.sParentNo = ParentNo;
                    info.iOrderNo = OrderNo;
                    info.sBranchNo = "";
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
                     
                    info.sClassName = ClassName;
                    info.sParentNo = ParentNo;
                    info.iOrderNo = OrderNo;
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
                json.Message = "保存内容类型发生内部错误！" + ex.Message;
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
                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentClass info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

    }
}