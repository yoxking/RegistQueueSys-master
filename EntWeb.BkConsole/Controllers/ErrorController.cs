using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error_400()
        {
            return View();
        }
        public ActionResult Error_401()
        {
            return View();
        }
        public ActionResult Error_403()
        {
            return View();
        }
        public ActionResult Error_404()
        {
            return View();
        }
        public ActionResult Error_405()
        {
            return View();
        }
        public ActionResult Error_500()
        {
            return View();
        }
        public ActionResult Error_503()
        {
            return View();
        }
        /// <summary>
        /// 禁止登录跳转页
        /// </summary>
        public ActionResult Error_888()
        {
            string backurl = Request.QueryString["backurl"];
            ViewData["backurl"] = !string.IsNullOrEmpty(backurl) ? Server.UrlDecode(backurl) : "";
            return View();
        }
    }
}