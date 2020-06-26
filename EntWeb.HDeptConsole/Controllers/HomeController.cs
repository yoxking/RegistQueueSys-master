using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Auth", new { Area = "System" });
        }

        public ActionResult About()
        {
            string copyRight = PublicHelper.GetConfigValue("CopyRight");

            Dictionary<string, object> stackHolder = new Dictionary<string, object>(); 
            stackHolder.Add("CopyRight", copyRight);
            ViewBag.StackHolder = stackHolder;
            return View();
        } 
    }
}