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
    public class EvaluatorInfoController : frmMainController
    {
        private string sWhere
        {
            set { TempData["Where_" + RouteData.Values["controller"].ToString()] = value; }
            get
            {
                var temp = TempData.Peek("Where_" + RouteData.Values["controller"].ToString());
                if (temp == null)
                {
                    return " 1=1 ";
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
                Condition = " BranchNo='" + PublicHelper.Get_BranchNo() + "'";
                if (!string.IsNullOrEmpty(sWhere))
                {
                    Condition += " And " + sWhere;
                }

                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluatorInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, PageIndex, this.PageSize, Condition);
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

        // 
        public override ActionResult Add()
        {
            try
            {
                EvaluatorInfo info = new EvaluatorInfo();
                info.sEvalorNo = CommonHelper.Get_New12ByteGuid();
                info.dRegistDate = DateTime.Now;
                info.sEvaIpAddr = "192.168.1.1";
                info.sEvaLcPort = "9812"; 

                ViewBag.StackHolder = info;

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
                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluatorInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost] 
        public override ActionResult Save()
        {
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };
            try
            {
                string sSuNo = ((LoginerInfo)this.HttpContext.Session["loginUser"]).UserNo;

                string EvalorNo = Request.Form["EvalorNo"].ToString();
                string EvaVCode = Request.Form["EvaVCode"].ToString();
                DateTime RegistDate =DateTime.Parse( Request.Form["RegistDate"].ToString());
                string EvaIpAddr = Request.Form["EvaIpAddr"].ToString();
                string EvaLcPort = Request.Form["EvaLcPort"].ToString();
                string Comments = Request.Form["Comments"].ToString();


                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluatorInfo info = infoBLL.GetRecordByNo(EvalorNo);

                //新增操作
                if (info == null)
                {
                    info = new EvaluatorInfo();

                    info.sEvalorNo = EvalorNo;
                    info.sEvaVCode = EvaVCode;
                    info.dRegistDate = RegistDate;
                    info.sEvaIpAddr = EvaIpAddr;
                    info.sEvaLcPort = EvaLcPort;
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
                    info.sEvaVCode = EvaVCode;
                    info.dRegistDate = RegistDate;
                    info.sEvaIpAddr = EvaIpAddr;
                    info.sEvaLcPort = EvaLcPort;
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
        public override ActionResult Delete(string ids)
        {
            JsonxHelper json = new JsonxHelper() { Message = "操作成功", Status = "Success" };
            try
            {
                string[] sNos = ids.Split(';');
                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
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
        public override ActionResult Detail(string id)
        {
            try
            {
                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                EvaluatorInfo info = infoBLL.GetRecordByNo(id);

                ViewBag.StackHolder = info;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}