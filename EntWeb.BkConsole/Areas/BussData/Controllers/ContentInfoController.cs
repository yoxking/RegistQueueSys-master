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
    public class ContentInfoController : frmMainController
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

                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
                ContentInfo info = new ContentInfo();
                info.sContNo = CommonHelper.Get_New12ByteGuid();
                info.dPublicDate = DateTime.Now;

                ViewBag.StackHolder = info;
                ViewBag.CntClassList = getCntClassList();

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
                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBLL.GetRecordByNo(id);

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

        [ValidateInput(false)]
        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string ContNo = Request.Form["ContNo"].ToString();
                string ClassNo = Request.Form["ClassNo"].ToString();
                string Title = Request.Form["Title"].ToString();
                string Author = Request.Form["Author"].ToString();
                DateTime PublicDate = DateTime.Parse(Request.Form["PublicDate"].ToString());
                string PostPicture = Request.Form["PostPicture"].ToString();
                string Abstract = "";
                string NContent = Request.Form["NContent"].ToString();
                int IsTop = int.Parse(Request.Form["IsTop"].ToString());
                int IsHot = int.Parse( Request.Form["IsHot"].ToString());
                int IsPop = int.Parse(Request.Form["IsPop"].ToString());
                string Comments = Request.Form["Comments"].ToString();

                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBLL.GetRecordByNo(ContNo);

                //新增操作
                if (info == null)
                {
                    info = new ContentInfo();

                    info.sContNo = ContNo;
                    info.sClassNo = ClassNo;
                    info.sTitle = Title;
                    info.sAuthor = Author;
                    info.dPublicDate = PublicDate;                    
                    info.sPostPicture = PostPicture;
                    info.sAbstract = Abstract;
                    info.sNContent = NContent;
                    info.iIsTop = IsTop;
                    info.iIsHot = IsHot;
                    info.iIsPop = IsPop;
                    info.iHitCount = 0;
                    info.dAuditDate = DateTime.Now;
                    info.sAuditor = "";
                    info.iCheckState = 1;
                    info.sBranchNos = "";
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
                    info.sClassNo = ClassNo;
                    info.sTitle = Title;
                    info.sAuthor = Author;
                    info.dPublicDate = PublicDate;
                    info.sPostPicture = PostPicture;
                    info.sAbstract = Abstract;
                    info.sNContent = NContent;
                    info.iIsTop = IsTop;
                    info.iIsHot = IsHot;
                    info.iIsPop = IsPop;
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
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ContentInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}