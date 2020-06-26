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
    public class RUsersInfoController : frmMainController
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


        //[(Message = "病人信息表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = sWhere;

                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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

            string sKeyword = Request.Form["sKeyword"].ToString();

            if (!string.IsNullOrEmpty(sKeyword))
            {
                sWhere += " And (CnName like '%" + sKeyword + "%'  OR IdCardNo like '%" + sKeyword + "%' )";
            }

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "病人信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                RUsersInfo info = new RUsersInfo();
                info.sRUserNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "病人信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RUsersInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "病人信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string RUserNo = Request.Form["RUserNo"].ToString();
                string CnName = Request.Form["CnName"].ToString();
                string EnName = "";
                string Age = Request.Form["Age"].ToString();
                string Sex = Request.Form["Sex"].ToString();
                string Nation = Request.Form["Nation"].ToString();
                int CardType = 1;
                string IdCardNo = Request.Form["IdCardNo"].ToString();
                string Address = Request.Form["Address"].ToString();
                string PostCode = Request.Form["PostCode"].ToString();
                string Telphone = Request.Form["Telphone"].ToString();
                string HeadPhoto = PublicConsts.DEFAULT_PHOTOFILE;
                string Summary ="";
                string Comments = Request.Form["Comments"].ToString();

                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RUsersInfo info = infoBLL.GetRecordByNo(RUserNo);

                //新增操作
                if (info == null)
                {
                    info = new RUsersInfo();

                    info.sRUserNo = RUserNo;
                    info.sCnName = CnName;
                    info.sEnName = EnName;
                    info.iAge = int.Parse(Age);
                    info.iSex = int.Parse(Sex);
                    info.sNation = Nation;
                    info.iCardType = CardType;
                    info.sIdCardNo = IdCardNo;
                    info.sIdCardNo = IdCardNo;
                    info.sAddress = Address;
                    info.sPostCode = PostCode;
                    info.sTelphone = Telphone;
                    info.sHeadPhoto = HeadPhoto;
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
                    info.sCnName = CnName;
                    info.sEnName = EnName;
                    info.iAge = int.Parse(Age);
                    info.iSex = int.Parse(Sex);
                    info.sNation = Nation;
                    info.iCardType = CardType;
                    info.sIdCardNo = IdCardNo;
                    info.sIdCardNo = IdCardNo;
                    info.sAddress = Address;
                    info.sPostCode = PostCode;
                    info.sTelphone = Telphone;
                    info.sHeadPhoto = HeadPhoto;
                    info.sSummary = Summary;
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
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = "病人组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RUsersInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}