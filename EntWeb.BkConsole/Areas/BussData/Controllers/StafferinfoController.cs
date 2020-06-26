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
    public class StafferinfoController : frmMainController
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


        //[(Message = "医生信息表(List)")]
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

                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
            string keyword = Request.Form["Keyword"].ToString();

            if (!string.IsNullOrEmpty(keyword))
            {
                sWhere += " And (StafferName like '%" + keyword + "%'  OR LoginId like '%" + keyword + "%' )";
            }

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "医生信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                StafferInfo info = new StafferInfo();
                info.sStafferNo = CommonHelper.Get_New12ByteGuid();
                info.sBranchNo = PublicHelper.Get_BranchNo();
                info.sHeadPhoto = "/Uploads/Photo/nopic.jpg";

                ViewBag.StackHolder = info;
                ViewBag.OrganizList = getOrganizs();
                ViewBag.BranchList = getBranchs();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "医生信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.OrganizList = getOrganizs();
                ViewBag.BranchList = getBranchs();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private OrganizInfoCollections getOrganizs()
        {
            int count = 0;
            string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
            OrganizInfoBLL infoBLL = new OrganizInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);
        }

        private BranchInfoCollections getBranchs()
        { 
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetAllRecords();
        }

        [HttpPost]
        [ValidateInput(false)]
        //[(Message = "医生信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {

                string sAppUrl = PublicHelper.GetConfigValue("AppUrl");
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                //string StafferNo = Request.Form["StafferNo"].ToString();
                string StafferNo = Request.Form["LoginId"].ToString();
                string StafferName = Request.Form["StafferName"].ToString();
                string LoginId = Request.Form["LoginId"].ToString();
                string Password = Request.Form["Password"].ToString();
                string OrganizNo = Request.Form["OrganizNo"].ToString();
                string OrganizName = PageHelper.getOrganizInfoNameByNo(OrganizNo);
                string StarLevel = Request.Form["StarLevel"].ToString();
                string HeadPhoto = Request.Form["HeadPhoto"].ToString();
                string Ranks = Request.Form["Ranks"].ToString();
                string Posts = Request.Form["Posts"].ToString();
                string Summary = Request.Form["Summary"].ToString();
                string BranchNo = Request.Form["BranchNo"].ToString();
                string Comments = Request.Form["Comments"].ToString();


                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo info = infoBLL.GetRecordByNo(StafferNo);

                //新增操作
                if (info == null)
                {
                    info = new StafferInfo();
                    info.sStafferNo = StafferNo;
                    info.sStafferName = StafferName;
                    info.sLoginId = LoginId;
                    info.sPassword = (string.IsNullOrEmpty(Password)) ? LoginId : Password;
                    info.sCounterNo = "00000000";
                    info.sOrganizNo = OrganizNo;
                    info.sOrganizName = OrganizName;
                    info.sStarLevel = StarLevel;
                    info.sHeadPhoto = HeadPhoto;
                    info.sRanks = Ranks;
                    info.sPosts = Posts;
                    info.sSummary = Summary;
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
                    info.sStafferName = StafferName;
                    info.sLoginId = LoginId;
                    if (!string.IsNullOrEmpty(Password))
                    {
                        info.sPassword = Password;
                    }
                    info.sOrganizNo = OrganizNo;
                    info.sOrganizName = OrganizName;
                    info.sStarLevel = StarLevel;
                    info.sHeadPhoto = HeadPhoto;
                    info.sRanks = Ranks;
                    info.sPosts = Posts;
                    info.sSummary = Summary;
                    info.sBranchNo = BranchNo;
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
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "医生组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}