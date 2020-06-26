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
    public class SUsersController : frmMainController
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


        //[(Message = "管理员信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                PageSize = 20;
                Condition = sWhere;

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
            //sWhere = "1=1 "; 

            //if (!string.IsNullOrEmpty(sTrueName))
            //{
            //    sWhere += " And (TrueName like '%" + sTrueName + "%'  OR LoginId like '%" + sTrueName + "%' )";
            //}

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "管理员信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                SUsersInfo info = new SUsersInfo();
                info.sSuNo = CommonHelper.Get_New12ByteGuid();
                info.iCheckState = 1; 

                ViewBag.StackHolder = info;
                ViewBag.ItemList = getBranchList();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "管理员信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.ItemList = getBranchList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private List<ItemObject> getBranchList()
        {
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            BranchInfoCollections infoColl = infoBLL.GetAllRecords();

            if (infoColl != null && infoColl.Count > 0)
            {
                List<ItemObject> itemList = new List<ItemObject>();
                foreach(BranchInfo info in infoColl)
                {
                    itemList.Add(new ItemObject(info.sBranchNo, info.sBranchName));
                }

                return itemList;
            }

            return null;
        }

        [HttpPost]
        //[(Message = "管理员信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sSuNo"].ToString();
                string sBranchNo = Request.Form["sBranchNo"].ToString();
                string sLoginID = Request.Form["sLoginID"].ToString();
                string sPassword ="123456";
                string sTrueName = Request.Form["sTrueName"].ToString(); 
                int iSex = Request.Form["iSex"].ToInt(); 
                string sUIDNo = Request.Form["sUIDNo"].ToString();
                string sTelphone = Request.Form["sTelphone"].ToString();
                string sEMail = Request.Form["sEMail"].ToString(); 
                int iAdminFlag = Request.Form["iAdminFlag"].ToInt(); 
                string sComments = Request.Form["sComments"].ToString(); 

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new SUsersInfo();
                    info.sSuNo = sNo;
                    info.sLoginID = sLoginID;
                    info.sPassword = sPassword;
                    info.sTrueName = sTrueName; 
                    info.sDeptNo = "00000000";
                    info.iSex = iSex; 
                    info.sUIDNo = sUIDNo;
                    info.sTelphone = sTelphone; 
                    info.iCheckState = 1;
                    info.iLockState = 0; 
                    info.sEMail = sEMail;
                    info.iAdminFlag = iAdminFlag;
                    info.sBranchNo = sBranchNo;
                    info.sComments = sComments; 


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
                    info.sTrueName = sTrueName; 
                    info.iSex = iSex;
                    info.sUIDNo = sUIDNo;
                    info.sTelphone = sTelphone;
                    info.iCheckState = 1;
                    info.iLockState = 0;
                    info.sEMail = sEMail;
                    info.iAdminFlag = iAdminFlag;
                    info.sBranchNo = sBranchNo;

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
        //[(Message = "重置管理员密码(ResetPwd)")]
        public ActionResult ResetPwds(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
                string[] sNos = ids.Split(';');
                string sPassword ="123456";

                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = null;

                foreach (string sNo in sNos)
                {
                    info = infoBLL.GetRecordByNo(sNo);

                    if (info != null)
                    {
                        info.sPassword = sPassword;
                        info.dModDate = DateTime.Now;
                        info.sModOptor = sSuNo;

                        infoBLL.UpdateRecord(info);
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /System/Role/Delete/5
        //[(Message = "管理员信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                SUsersInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}