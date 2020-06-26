using EntFrm.Business.BLL;
using EntFrm.Business.Model.Collections;
using EntWeb.MedicConsole.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult About()
        { 

            return View();
        }

        public ActionResult Login()
        {
            string appName = PublicHelper.GetConfigValue("AppName");
            string copyRight = PublicHelper.GetConfigValue("CopyRight");

            int count = 0;
            string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' ";
            CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, strWhere);
            ViewBag.CounterList = infoColl;

            Dictionary<string, object> stackHolder = new Dictionary<string, object>();
            stackHolder.Add("AppName", appName);
            stackHolder.Add("CopyRight", copyRight);
            ViewBag.StackHolder = stackHolder; 


            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public void AjaxLogin()
        {
            string username = Request["userName"];
            string password = Request["passWord"];
            string counterno = Request["counterNo"];
            string workmode = Request["workMode"];

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
                    Session["workMode"] = workmode;
                    Session["CounterNo"] = counterno;

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