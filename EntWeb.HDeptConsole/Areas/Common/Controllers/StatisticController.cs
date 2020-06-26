using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class StatisticController : frmMainController
    {
        public StatisticController()
        {
            viewExpress = PublicHelper.Get_ViewExpress();
            viewExpress.ViewTag = "StatisticView";
            ViewBag.ViewExpress = viewExpress;
        }

        // GET: Common/Statistic
        override
        public ActionResult Index()
        {
            ViewBag.ItemList = PageService.GetStaffList(true);
            return View();
        }  
    }
}