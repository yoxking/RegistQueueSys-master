using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.System.Controllers
{
    public class AuthController : Controller
    {

        //
        // GET: /System/Auth/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            string appName = PublicHelper.GetConfigValue("AppName");
            string copyRight = PublicHelper.GetConfigValue("CopyRight");

            Dictionary<string, object> stackHolder = new Dictionary<string, object>();
            stackHolder.Add("AppName", appName);
            stackHolder.Add("CopyRight", copyRight);
            ViewBag.StackHolder = stackHolder;

            return View();
        }  

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        public void AjaxLogin()
        {
            string username = Request["username"];
            string password = Request["password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Response.Write("ERROR");
            }
            else
            {
                AuthService infoBLL = new AuthService();

                var loginInfo = infoBLL.Login(username, password);

                if (loginInfo != null)
                {
                    Session["loginUser"] = loginInfo;

                    Response.Write("SUCCESS");

                }
                else
                {
                    Response.Write("ERROR");
                }
            }
        }
    }
}