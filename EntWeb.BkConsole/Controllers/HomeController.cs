using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Auth", new { Area = "System" });
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Encrypt()
        {
            return View();
        }

        public string EncryptText(string text)
        {
           return  EnconfigHelper.Encrypt(text); 
        }

        public string DecryptText(string text)
        {
            return EnconfigHelper.Decrypt(text);
        }
    }
}