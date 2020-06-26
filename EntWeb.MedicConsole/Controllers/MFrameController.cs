using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntWeb.MedicConsole.Common;
using EntWeb.MedicConsole.Models;
using System;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class MFrameController : frmMainController
    {
        // GET: MFrame
        override
       public  ActionResult Index()
        {
            string workMode = Session["workMode"].ToString();
            if (workMode.Equals(PublicConsts.WORKMODE_MAKERECIPE.ToString()))
            {
                return RedirectToAction("MakeRecipe");
            }
            else
            {
                return RedirectToAction("TakeRecipe");
            } 
        }

        public ActionResult MakeRecipe()
        { 
            string counterNo = Session["CounterNo"].ToString();

            ViewBag.WebName = PublicHelper.Get_AppName();
            ViewBag.BranchName = PublicHelper.getBranchNameById(PublicHelper.Get_BranchNo());
            ViewBag.CounterName = PublicHelper.getCounterNameById(counterNo);
            ViewBag.WorkMode = "配药";
            ViewBag.LoginId = ((LoginerInfo)Session["loginUser"]).LoginId;
            return View();
        }

        public ActionResult getDataList_MakeRecipe(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                DateTime workDate = DateTime.Now;
                TableData tdata = new TableData();
                string sSUserNo = ((LoginerInfo)Session["loginUser"]).UserNo;

                string strWhere = " BranchNo='"+PublicHelper.Get_BranchNo()+ "' And (RecipeOpter='"+ sSUserNo + "' Or RecipeOpter='') And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition))
                { 
                    strWhere += " And " + condition;
                }
                else
                {
                    strWhere += " And RecipeState Between 1 And 2 ";
                }
                 
                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = "EnqueueTime";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewRecipeFlows";
                ViewRecipeFlowsBLL infoBLL = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections infoColl = infoBLL.GetRecordsByPaging(s_model);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertTicketFlows(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TakeRecipe()
        {
            string counterNo = Session["CounterNo"].ToString();

            ViewBag.WebName = PublicHelper.Get_AppName();
            ViewBag.BranchName = PublicHelper.getBranchNameById(PublicHelper.Get_BranchNo());
            ViewBag.CounterName = PublicHelper.getCounterNameById(counterNo);
            ViewBag.WorkMode = "发药";
            ViewBag.LoginId = ((LoginerInfo)Session["loginUser"]).LoginId;
            return View();
        }

        public ActionResult getDataList_TakeRecipe(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                DateTime workDate = DateTime.Now;
                TableData tdata = new TableData();
                string sSUserNo = ((LoginerInfo)Session["loginUser"]).UserNo;
                string counterNo = Session["CounterNo"].ToString();

                string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And CounterNo='"+ counterNo + "' And RecipeState=3 And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                if (!string.IsNullOrEmpty(condition))
                {
                    strWhere += " And " + condition;
                }
                else
                {
                    strWhere += " And ProcessState <3 ";
                }

                SqlModel s_model = new SqlModel();
                s_model.iPageNo = 1;
                s_model.iPageSize = 100;
                s_model.sFields = "*";
                s_model.sCondition = strWhere;
                s_model.sOrderField = "EnqueueTime";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewRecipeFlows";
                ViewRecipeFlowsBLL infoBLL = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRecipeFlowsCollections infoColl = infoBLL.GetRecordsByPaging(s_model);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertTicketFlows(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SelectCounter()
        {
            int count = 0;
            string strWhere = " BranchNo='"+PublicHelper.Get_BranchNo()+"' ";
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, strWhere);

            ViewBag.CounterNo =Session["CounterNo"];
            ViewBag.CounterList = infoColl;
            return View();
        }

        public void UpdateCounter()
        {
            try
            {
                string sCounterNo = Request["sCounterNo"];

                Session["CounterNo"] = sCounterNo;

                Response.Write("SUCCESS");

            }
            catch (Exception ex)
            {
                Response.Write("ERROR");
            }
        }
    }
}