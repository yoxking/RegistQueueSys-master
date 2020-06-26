using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntWeb.MedicConsole.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class IAdapterController : Controller
    {
        // GET: IAdapter
        public ActionResult Index()
        {
            return View();
        }
        
        //刷卡报到-配药
        public string EnqueueRCard_MakeRecipe(string ricardno)
        {
            int count = 0;
            DateTime workDate = DateTime.Now;
            RUsersInfo ruser = PageService.GetRUserInfoByRiCardNo(ricardno);

            if (ruser != null)
            {
                string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + ruser.sRUserNo + "' And RecipeState=0 And ProcessState=0  And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                RecipeFlowsBLL rflowBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RecipeFlowsCollections recColl = rflowBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                RecipeFlows recFlow = null;

                if (recColl != null && recColl.Count > 0)
                {
                    recFlow = recColl.GetFirstOne();

                    recFlow.iRecipeState = 1;
                    recFlow.iProcessState = 0;
                    recFlow.dRecipeDate = DateTime.Now;
                    recFlow.dModDate = DateTime.Now;
                    ;
                    if (rflowBLL.UpdateRecord(recFlow))
                    {
                        return ("Success");
                    }
                }
            }
            return "Error";
        }

        //刷卡报到-发药
        public string EnqueueRCard_TakeRecipe(string ricardno)
        {
            int count = 0;
            DateTime workDate = DateTime.Now;
            RUsersInfo ruser = PageService.GetRUserInfoByRiCardNo(ricardno);

            if (ruser != null)
            {
                string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + ruser.sRUserNo + "' And RecipeState=0 And ProcessState=0 And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                RecipeFlowsBLL rflowBLL = new RecipeFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RecipeFlowsCollections recColl = rflowBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                RecipeFlows recFlow = null;

                if (recColl != null && recColl.Count > 0)
                {
                    recFlow = recColl.GetFirstOne();

                    recFlow.iRecipeState = 3;
                    recFlow.iProcessState = 1;
                    recFlow.dRecipeDate = DateTime.Now;
                    recFlow.dModDate = DateTime.Now;
                    ;
                    if (rflowBLL.UpdateRecord(recFlow))
                    {
                        return ("Success");
                    }
                }
            }
            return "Error";
        }
    }
}