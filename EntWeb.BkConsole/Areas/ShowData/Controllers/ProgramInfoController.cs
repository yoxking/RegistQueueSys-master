using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntFrm.Framework.Web.Controls;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.ShowData.Controllers
{
    public class ProgramInfoController : frmMainController
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


        //[(Message = "信息列表(List)")]
        public override ActionResult List()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString());
                Condition = " IsTemplate=0";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition = " And " + sWhere;
                }

                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
                int totalCount = infoBLL.GetCountByCondition(Condition);

                PagerHelper pager = new PagerHelper(PageIndex, PageSize, totalCount);

                Dictionary<string, object> stackHolder = new Dictionary<string, object>();
                stackHolder.Add("infoList", infoColl);
                stackHolder.Add("pager", pager);
                ViewBag.StackHolder = stackHolder;
                ViewBag.ClassList = getProgramClasses();
            }
            catch (Exception ex)
            { }
            return View();
        }

        //[(Message = "信息列表(List)")]
        public ActionResult Flows()
        {
            try
            {
                PageIndex = int.Parse(Request.Form["pageIndex"] == null ? "1" : Request.Form["pageIndex"].ToString()); 
                Condition = " CheckState=0 ";

                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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

            string pclassNo = Request.Form["PClassNo"].ToString();
            string keyword = Request.Form["Keyword"].ToString();

            if (!pclassNo.Equals("00000000"))
            {
                sWhere += " And PClassNo='" + pclassNo + "' ";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                sWhere += " And ( ProgmName like '%" + keyword + "%'  )";
            }

            return RedirectToAction("List");
        }

        //
        // GET: /System/Role/Add
        //[(Message = "信息添加(Add)")]
        public override ActionResult Add()
        {
            try
            {
                DsProgramInfo info = new DsProgramInfo();
                info.sProgmNo = CommonHelper.Get_New12ByteGuid();

                ViewBag.StackHolder = info;
                ViewBag.ClassList = getProgramClasses();
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }

        ////
        //// GET: /System/Role/Edit/5
        //[(Message = "信息编辑(Edit)")]
        public override ActionResult Edit(string id)
        {
            try
            {
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
                ViewBag.ClassList = getProgramClasses();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        private DsProgramClassCollections getProgramClasses()
        {
            DsProgramClassBLL infoBLL = new DsProgramClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            return infoBLL.GetAllRecords();
        }

        [HttpPost]
        //[(Message = "信息保存(Save)")]
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string sNo = Request.Form["ProgmNo"].ToString();
                string sProgmName = Request.Form["ProgmName"].ToString();
                string sPClassNo = Request.Form["PClassNo"].ToString();
                string sPFilePath = Request.Form["PFilePath"].ToString();
                string sPWebUrl = Request.Form["PWebUrl"].ToString();
                string sPContent = Request.Form["PContent"].ToString();
                string sComments = Request.Form["Comments"].ToString();

                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = infoBLL.GetRecordByNo(sNo);

                //新增操作
                if (info == null)
                {
                    info = new DsProgramInfo();
                    info.sProgmNo = sNo;
                    info.sProgmName = sProgmName;
                    info.sPClassNo = sPClassNo;
                    info.sPosterUrl = "";
                    info.iIsTemplate = 0;
                    info.sPFilePath = sPFilePath;
                    info.sPWebUrl = sPWebUrl;
                    info.sPContent = sPContent;
                    info.iSlideNum = 1;
                    info.iPVersion = 1;
                    info.iDuration = 100;
                    info.sResolution = "1920*1080"; 
                    info.iCheckState = 0;
                    info.sCheckOptor = "";
                    info.dCheckDate = DateTime.Now;
                    info.sBranchNo = "";
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
                    info.sProgmName = sProgmName;
                    info.sPClassNo = sPClassNo;
                    info.sPFilePath = sPFilePath;
                    info.sPWebUrl = sPWebUrl;
                    info.sPContent = sPContent;
                    info.iSlideNum = 1;

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
                json.Message = "信息发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }


        // GET: /System/Role/Delete/5
        //[(Message = "信息删除(Delete)")]
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                infoBLL.SoftDeleteRecord(sNos);
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /System/Role/Delete/5
        //[(Message = "信息审核(Valid)")]
        public ActionResult Valid(string ids)
        {
            string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = null;

                foreach (string m in sNos)
                {
                    info = infoBLL.GetRecordByNo(m);
                    info.iCheckState = 1;
                    info.dCheckDate = DateTime.Now;
                    info.sCheckOptor = sSuNo;
                    info.dModDate = DateTime.Now;

                    infoBLL.UpdateRecord(info);
                } 
            }
            catch (Exception ex)
            {
                json.Message = "操作时发生内部错误！" + ex.Message;
                json.Status = "Failure";
            }
            return Json(json);
        }

        // GET: /PubsData/Content/Detail/5
        //[(Message = "信息详细(Detail)")]
        public override ActionResult Detail(string id)
        {
            try
            {
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        
        // GET: /System/Role/Delete/5
        //[(Message = "信息删除(Delete)")]
        public ActionResult Template(string id)
        {
            string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            { 
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    info.iIsTemplate = 1;
                    info.dModDate = DateTime.Now;
                    info.sModOptor = sSuNo;

                    infoBLL.UpdateRecord(info);
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