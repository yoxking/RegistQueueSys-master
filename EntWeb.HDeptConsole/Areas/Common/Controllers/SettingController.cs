using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class SettingController : frmMainController
    {
        public SettingController()
        {
            ViewBag.ViewExpress = PublicHelper.Get_ViewExpress();
        }

        // GET: Common/Setting
        override
        public ActionResult Index()
        {
            string sBranchNo = PublicHelper.Get_BranchNo();
            string sServerIp = PublicHelper.GetConfigValue("ServerIp");
            string sWTcpPort = PublicHelper.GetConfigValue("WTcpPort");
            string sSTcpPort = PublicHelper.GetConfigValue("STcpPort");
            string sWHttpPort = PublicHelper.GetConfigValue("WHttpPort"); 

            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");
            string sRegisteMode = PublicHelper.GetParamValue(PublicConsts.DEF_REGISTEMODE, "Others");
            string sRediagInterval = PublicHelper.GetParamValue(PublicConsts.DEF_REDIAGNOSISINTERVAL, "Others");

            ViewBag.ServerIp = sServerIp;
            ViewBag.WTcpPort = sWTcpPort;
            ViewBag.STcpPort = sSTcpPort; 
            ViewBag.WHttpPort = sWHttpPort;
            ViewBag.BranchNo = sBranchNo;
            ViewBag.BranchList = getBranchList();
            ViewBag.WorkingMode = sWorkingMode;
            ViewBag.RegisteMode = sRegisteMode;
            ViewBag.RediagInterval = sRediagInterval;

            return View();
        }

        private List<ItemData> getBranchList()
        { 
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            BranchInfoCollections infoColl = infoBLL.GetAllRecords();

            if(infoColl!=null&& infoColl.Count > 0)
            {
                List<ItemData> itemList = new List<ItemData>(); 

                foreach(BranchInfo info in infoColl)
                {
                    itemList.Add(new ItemData(info.sBranchNo, info.sBranchName));
                }

                return itemList;
            }

            return null;
        }

        public void Saveit(string ServerIp, string WTcpPort, string STcpPort,string WHttpPort,  string BranchNo, string RediagInterval,string WorkingMode, string RegisteMode)
        {
            try
            {
                PublicHelper.SetConfigValue("ServerIp", ServerIp);
                PublicHelper.SetConfigValue("WTcpPort", WTcpPort);
                PublicHelper.SetConfigValue("STcpPort", STcpPort); 
                PublicHelper.SetConfigValue("WHttpPort", WHttpPort);
                //PublicHelper.SetConfigValue("BranchNo", BranchNo);

                PublicHelper.SetParamValue("RediagnosisInterval", RediagInterval, "Others");
                PublicHelper.SetParamValue("WorkingMode", WorkingMode, "Others");
                PublicHelper.SetParamValue("RegisteMode", RegisteMode, "Others");


                Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                Response.Write("ERROR");
            }
        }
    }
}