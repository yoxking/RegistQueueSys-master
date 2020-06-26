using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using EntWeb.MedicConsole.Common;
using EntWeb.MedicConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class RecipeController : frmMainController
    { 
        // GET: Recipe
        override
        public ActionResult Index()
        {
            return View();
        } 

        // GET: Recipe
        override
        public ActionResult Detail(string id)
        {
            ViewBag.RFlow = id;
            return View();
        }

        // GET: Recipe 
        public ActionResult Check(string id)
        {
            string sSUserNo = ((LoginerInfo)Session["loginUser"]).UserNo;
            string sCounterNo = Session["CounterNo"].ToString();
            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(id);

            if (info != null && (info.sRecipeOpter.Equals(sSUserNo) || info.iRecipeState < 2))
            {
                { 
                    info.sCounterNo = sCounterNo;
                    info.iRecipeState = PublicConsts.RECIPESTATE2;//配药中
                    info.sRecipeOpter = sSUserNo;
                    info.dRecipeDate = DateTime.Now;
                    info.dModDate = DateTime.Now;

                    infoBLL.UpdateRecord(info);
                }

                ViewBag.RFlow = id;
                ViewBag.CounterNo = sCounterNo;
                ViewBag.TotalAmount = getTotalAmount(id);
                ViewBag.CounterList = getCounterList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "MError");
            }
        }
        private CounterInfoCollections getCounterList()
        {

            int count = 0;
            string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' ";
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, strWhere);

            return infoColl;
        }

        private string getTotalAmount(string recipeId)
        {
            double amount = 0;
            RecipeDetailsBLL infoBLL = new RecipeDetailsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeDetailsCollections infoColl = infoBLL.GetRecordsByClassNo(recipeId);

            if (infoColl != null && infoColl.Count > 0)
            {
                foreach(RecipeDetails info in infoColl)
                {
                    amount += info.dAmount;
                }
            }

            return amount.ToString("0.00");
        }

        public void CheckRecipe()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sRFlowNo = Request["sRFlowNo"];
            string sCounterNo = Request["sCounterNo"]; 

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(sRFlowNo);

            if (info != null)
            {
                info.sTicketNo = doGenerateTicketNo(sCounterNo, PublicHelper.Get_BranchNo());
                info.sCounterNo = sCounterNo;
                info.iRecipeState = PublicConsts.RECIPESTATE3;//已完成
                info.iProcessState = PublicConsts.RROCESSSTATE1;//等候中
                info.sRecipeOpter = sSUserNo;
                info.dRecipeDate = DateTime.Now;
                info.dEnqueueTime = DateTime.Now;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public void CallRecipe()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sRFlowNo = Request["sRFlowNo"];
            string sCounterNo = Session["CounterNo"].ToString();

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(sRFlowNo);

            if (info != null)
            {
                info.iProcessState = PublicConsts.RROCESSSTATE2;//呼叫中 
                info.sPrcsCounterNo = sCounterNo;
                info.sModOptor = sSUserNo;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                { 
                    Task.Factory.StartNew(() =>
                    {
                        IUserContext.OnExecuteCommand_Xp("doCallNextRecipe", new string[] { sRFlowNo });
                        ITicketService.CreateInstance().FindBoxInfo(info.sRUserNo,info.sCounterNo);
                    });

                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }
        public void PassRecipe()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sRFlowNo = Request["sRFlowNo"];
            string sCounterNo = Session["CounterNo"].ToString();

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(sRFlowNo);

            if (info != null)
            {
                info.iProcessState = PublicConsts.RROCESSSTATE3;//已过号 
                info.sPrcsCounterNo = sCounterNo;
                info.sModOptor = sSUserNo;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {  
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public void RequeueRecipe()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sRFlowNo = Request["sRFlowNo"];
            string sCounterNo = Session["CounterNo"].ToString();

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(sRFlowNo);

            if (info != null)
            {
                info.iProcessState = PublicConsts.RROCESSSTATE1;//呼叫中 
                info.sPrcsCounterNo = sCounterNo;
                info.dEnqueueTime = DateTime.Now;
                info.sModOptor = sSUserNo;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public void FinishRecipe()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sRFlowNo = Request["sRFlowNo"];
            string sCounterNo = Session["CounterNo"].ToString();

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = infoBLL.GetRecordByNo(sRFlowNo);

            if (info != null)
            {
                info.iProcessState = PublicConsts.RROCESSSTATE4;//完成呼叫 
                info.sPrcsCounterNo = sCounterNo;
                info.sModOptor = sSUserNo;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Task.Factory.StartNew(() =>
                    { 
                        ITicketService.CreateInstance().FinishRecipe(info.sRUserNo, info.sCounterNo);
                    });
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                Response.Write("ERROR");
            }
        }
        public void FinishRecipes()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string[] sRFlowNos = Request["sRFlowNos"].Split(';');
            string sCounterNo = Session["CounterNo"].ToString();

            RecipeFlowsBLL infoBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RecipeFlows info = null;

            foreach (string sRFlowNo in sRFlowNos)
            {
                info = infoBLL.GetRecordByNo(sRFlowNo);

                if (info != null)
                {
                    info.iProcessState = PublicConsts.RROCESSSTATE4;//完成呼叫 
                    info.sPrcsCounterNo = sCounterNo;
                    info.sModOptor = sSUserNo;
                    info.dModDate = DateTime.Now;

                    if (infoBLL.UpdateRecord(info))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            ITicketService.CreateInstance().FinishRecipe(info.sRUserNo, info.sCounterNo);
                        });
                    } 
                }
            }

            Response.Write("SUCCESS");
        }

        private string doGenerateTicketNo(string sCounterNo, string sBranchNo)
        { 
            string sTicketNo = "0001"; 
            DateTime workDate = DateTime.Now;
            string sWhere = "";

            try
            {
                //默认生成票号
                ViewRecipeFlowsBLL vticketBLL = new ViewRecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo='" + sBranchNo + "' And CounterNo='" + sCounterNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                }
                else
                {
                    sWhere = " BranchNo='" + sBranchNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                }
                int countNum = vticketBLL.GetCountByCondition(sWhere) + 1;
                sTicketNo = String.Format("{0:D3}", countNum);
            }
            catch (Exception ex)
            {
            }
            return sTicketNo;
        }


        public ActionResult getDataList_Recipe(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                string recipeId = condition; 
                TableData tdata = new TableData();

                string strWhere = " RFlowNo='"+ recipeId + "' "; 

                int count = 0;
                RecipeDetailsBLL infoBLL = new RecipeDetailsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RecipeDetailsCollections infoColl = infoBLL.GetRecordsByClassNo(recipeId);
                count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertRecipeData(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}