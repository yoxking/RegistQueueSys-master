using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace EntWeb.BkConsole.Areas.System.Controllers
{
    public class SettingController : frmMainController
    {
        // GET: System/Setting
        public override ActionResult Index()
        {

            string sqlServer = PublicHelper.Get_ConnStr();
            string appUrl = PublicHelper.GetConfigValue("AppUrl");
            string appName = PublicHelper.GetConfigValue("AppName");
            string appDesc = PublicHelper.GetConfigValue("AppDesc");
            string serverIp = PublicHelper.GetConfigValue("ServerIp");
            string wtcpPort = PublicHelper.GetConfigValue("WTcpPort");
            string maskFlag = PublicHelper.GetConfigValue("MaskFlag");
            string copyRight = PublicHelper.GetConfigValue("CopyRight");

            sqlServer = EnconfigHelper.Decrypt(sqlServer);

            Dictionary<string, object> stackHolder = new Dictionary<string, object>();
            stackHolder.Add("AppUrl", appUrl);
            stackHolder.Add("AppName", appName);
            stackHolder.Add("SqlServer", sqlServer);
            stackHolder.Add("AppDesc", appDesc);
            stackHolder.Add("ServerIp", serverIp);
            stackHolder.Add("WTcpPort", wtcpPort);
            stackHolder.Add("MaskFlag", maskFlag);
            stackHolder.Add("CopyRight", copyRight);
            ViewBag.StackHolder = stackHolder;
            return View();
        } 

        // GET: System/Setting
        public void AjaxSave()
        {
            try
            {
                string sAppName = Request.Form["appName"].ToString();
                string sAppUrl = Request.Form["appUrl"].ToString();
                string sSqlServer = Request.Form["sqlServer"].ToString();
                string sAppDesc = Request.Form["appDesc"].ToString();
                string sServerIp = Request.Form["serverIp"].ToString();
                string sWtcpPort = Request.Form["wtcpPort"].ToString();
                string sMaskFlag = Request.Form["maskFlag"].ToString();
                string sCopyRight = Request.Form["copyRight"].ToString();


                string connStr = EnconfigHelper.Encrypt(sSqlServer);

                PublicHelper.SetConfigValue("AppUrl", sAppUrl);
                PublicHelper.SetConfigValue("AppName", sAppName);
                PublicHelper.SetConfigValue("SqlServer", connStr);
                PublicHelper.SetConfigValue("AppDesc", sAppDesc);
                PublicHelper.SetConfigValue("ServerIp", sServerIp);
                PublicHelper.SetConfigValue("WTcpPort", sWtcpPort);
                PublicHelper.SetConfigValue("MaskFlag", sMaskFlag);
                PublicHelper.SetConfigValue("CopyRight", sCopyRight);

                Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR");
            }
        }
    }
} 