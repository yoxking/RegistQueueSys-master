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
    public class StafferRotaController : frmMainController
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


        //[(Message = " 医生排班信息表(List)")]
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

                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferRotaCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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
        //[(Message = " 医生排班信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                StafferRota info = new StafferRota();
                info.sRotaNo = CommonHelper.Get_New12ByteGuid();
                info.dStartDate = DateTime.Now;
                info.dEnditDate = DateTime.Now;

                ViewBag.StackHolder = info;
                ViewBag.ItemList = getStaffList();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = " 医生排班信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferRota info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.ItemList = getStaffList();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private List<ItemObject> getStaffList()
        {
            int count = 0;
            StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1000, " BranchNo='" + PublicHelper.Get_BranchNo() + "' ");

            if (infoColl != null && infoColl.Count > 0)
            {
                List<ItemObject> itemList = new List<ItemObject>();
                foreach (StafferInfo info in infoColl)
                {
                    itemList.Add(new ItemObject(info.sStafferNo, info.sStafferName));
                }

                return itemList;
            }

            return null;
        }

        [HttpPost]
        //[(Message = " 医生排班信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string RotaNo = Request.Form["RotaNo"].ToString();
                string StafferNo = Request.Form["StafferNo"].ToString();
                int RotaType = int.Parse(Request.Form["RotaType"].ToString());
                int WeekDay1 = int.Parse(Request.Form["WeekDay1"].ToString());
                int WeekDay2 = int.Parse(Request.Form["WeekDay2"].ToString());
                int WeekDay3 = int.Parse(Request.Form["WeekDay3"].ToString());
                int WeekDay4 = int.Parse(Request.Form["WeekDay4"].ToString());
                int WeekDay5 = int.Parse(Request.Form["WeekDay5"].ToString());
                int WeekDay6 = int.Parse(Request.Form["WeekDay6"].ToString());
                int WeekDay7 = int.Parse(Request.Form["WeekDay7"].ToString());
                //string RotaFormat = Request.Form["RotaFormat"].ToString();
                double RegisteFees = double.Parse(Request.Form["RegisteFees"].ToString());
                string Comments = Request.Form["Comments"].ToString();

                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferRota info = infoBLL.GetRecordByNo(RotaNo);

                //新增操作
                if (info == null)
                {
                    info = new StafferRota();

                    info.sRotaNo = RotaNo;
                    info.sStafferNo = StafferNo;
                    info.iRotaType = RotaType;
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iWeekDay1 = WeekDay1;
                    info.iWeekDay2 = WeekDay2;
                    info.iWeekDay3 = WeekDay3;
                    info.iWeekDay4 = WeekDay4;
                    info.iWeekDay5 = WeekDay5;
                    info.iWeekDay6 = WeekDay6;
                    info.iWeekDay7 = WeekDay7;
                    info.sRotaFormat = "1";
                    info.dRegisteFees = RegisteFees;
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
                    info.sStafferNo = StafferNo;
                    info.iRotaType = RotaType;
                    info.iWeekDay1 = WeekDay1;
                    info.iWeekDay2 = WeekDay2;
                    info.iWeekDay3 = WeekDay3;
                    info.iWeekDay4 = WeekDay4;
                    info.iWeekDay5 = WeekDay5;
                    info.iWeekDay6 = WeekDay6;
                    info.iWeekDay7 = WeekDay7;
                    info.sRotaFormat = "1";
                    info.dRegisteFees = RegisteFees;
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
                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        //[(Message = " 医生排班组信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                StafferRotaBLL infoBLL = new StafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferRota info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}