using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Web;
using EntWeb.MedicConsole.Common;
using System;
using System.Web.Mvc;

namespace EntWeb.MedicConsole.Controllers
{
    public class ProfileController : frmMainController
    {
        
        // GET: Profile
        override
        public ActionResult Index()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            StafferInfo info = infoBLL.GetRecordByNo(loginInfo.UserNo);

            ViewBag.StafferName = info.sStafferName;
            ViewBag.OrganizNo = info.sOrganizName;

            return View();
        }
        public ActionResult Psword()
        { 
            return View();
        }

        public void UptPsword()
        {
            var loginInfo = (LoginerInfo)Session["loginUser"];

            string sSUserNo = loginInfo.UserNo;
            string sOldPsword = Request["sOldPsword"];
            string sNewPsword = Request["sNewPsword"];

            AuthService infoBLL = new AuthService();

            if (infoBLL.ModifyPassword(sSUserNo, sOldPsword, sNewPsword))
            {
                Response.Write("SUCCESS");
            }
            else
            {
                Response.Write("ERROR");
            }
        }

        public ActionResult Setting()
        {

            string serverIp = PublicHelper.GetConfigValue("ServerIp");
            string wtcpPort = PublicHelper.GetConfigValue("WTcpPort");
            string registeMode = PublicHelper.GetParamValue("RegisteMode");

            ViewBag.ServerIp = serverIp;
            ViewBag.WTcpPort = wtcpPort;
            ViewBag.RegisteMode = registeMode;

            return View();
        }

        public void UptSetting()
        {
            try
            {
                string sServerIp = Request["sServerIp"];
                string sWtcpPort = Request["sWtcpPort"];
                string sRegisteMode = Request["sRegisteMode"];

                PublicHelper.SetConfigValue("ServerIp", sServerIp);
                PublicHelper.SetConfigValue("WTcpPort", sWtcpPort);
                PublicHelper.SetParamValue("RegisteMode", sRegisteMode);

                Response.Write("SUCCESS");

            }
            catch (Exception ex)
            {
                Response.Write("ERROR");
            }
        }
    }
}