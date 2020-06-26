using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.System.Controllers
{
    public class RolesController : frmMainController
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


        //[(Message = "角色信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString()); 
                Condition = sWhere;

                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RoleInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
        //[(Message = "角色信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                RoleInfo info = new RoleInfo();
                info.sRoleNo = CommonHelper.Get_New12ByteGuid(); 

                ViewBag.StackHolder = info;

            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "角色信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RoleInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost]
        //[(Message = "角色信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["sRoleNo"].ToString();
                string sRoleName = Request.Form["sRoleName"].ToString();
                string sComments = Request.Form["sComments"].ToString();

                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RoleInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new RoleInfo(); 
                    info.sRoleNo = sNo;
                    info.sRoleName = sRoleName;
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
                    info.sRoleName = sRoleName;
                    info.sComments = sComments;

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
        //[(Message = "角色信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RoleInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult SelectSUsers(string id)
        {
            SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            SUsersInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, " AdminFlag=1 "); 
               
            ViewBag.InfoList = infoColl;
            ViewBag.SUsersList = getSUsersByRoleNo(id);
            ViewBag.RoleNo = id;
            ViewBag.RoleName = PageHelper.getRoleInfoNameByNo(id);

            return View();
        }

        private string getSUsersByRoleNo(string roleNo)
        {
            StringBuilder sb = new StringBuilder();
            UserRoleBLL infoBLL= new UserRoleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            UserRoleCollections infoColl = infoBLL.GetRecordsByRoleNo(roleNo);
            if (infoColl != null && infoColl.Count > 0)
            {
                foreach(UserRole info in infoColl)
                {
                    sb.Append(info.sUserNo + ",");
                }
            }

            return sb.ToString();
        }

        public ActionResult SelectPermits(string id)
        {
            PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            PermitInfoCollections infoColl = infoBLL.GetAllRecordsByParentNoOrder("00000000", HttpUtility.HtmlDecode("---"));

            ViewBag.InfoList = infoColl;
            ViewBag.PermitsList = getPermitsByRoleNo(id);
            ViewBag.RoleNo = id;
            ViewBag.RoleName = PageHelper.getRoleInfoNameByNo(id);

            return View();
        }

        private string getPermitsByRoleNo(string roleNo)
        {
            StringBuilder sb = new StringBuilder();
            RolePermitBLL infoBLL = new RolePermitBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            RolePermitCollections infoColl = infoBLL.GetRecordsByRoleNo(roleNo);
            if (infoColl != null && infoColl.Count > 0)
            {
                foreach (RolePermit info in infoColl)
                {
                    sb.Append(info.sPermitNo + ",");
                }
            }
            return sb.ToString();
        }

        public ActionResult AllotSUsers(string roleNo,string suserIds)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                UserRoleBLL uroleBLL = new UserRoleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                UserRole urole = null;
                string[] suserNos = suserIds.Split(';');

                uroleBLL.DeleteRecordByRoleNo(roleNo);

                foreach (string suser in suserNos)
                {
                    urole = new UserRole();
                    urole.sRoleNo = roleNo;
                    urole.sUserNo = suser;
                    urole.sAppCode = PublicHelper.Get_AppCode() + ";";

                    uroleBLL.AddNewRecord(urole);
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        public ActionResult AllotPermits(string roleNo, string permitIds)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                RolePermitBLL proleBLL = new RolePermitBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RolePermit prole = null;
                string[] permitNos = permitIds.Split(';');

                proleBLL.DeleteRecordByRoleNo(roleNo);

                foreach (string permit in permitNos)
                {
                    prole = new RolePermit();
                    prole.sRoleNo = roleNo;
                    prole.sPermitNo = permit;
                    prole.sAppCode = PublicHelper.Get_AppCode() + ";";

                    proleBLL.AddNewRecord(prole);
                }
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }
    }
}