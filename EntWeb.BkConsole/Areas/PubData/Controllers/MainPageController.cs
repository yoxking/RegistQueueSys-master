using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections; 
using EntFrm.Framework.Web;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Areas.PubData.Controllers
{
    public class MainPageController : frmMainController
    {
        //
        // GET: /System/Role/
        public override ActionResult Index()
        {
            LoginerInfo loginer = ((LoginerInfo)this.HttpContext.Session["loginUser"]);

            ViewBag.AppName = PublicHelper.GetConfigValue("AppName");
            ViewBag.LoginId = loginer.LoginId;
            ViewBag.TopMenu = getTopMenu(loginer.UserNo);
            ViewBag.SubMenu = getSubMenu(loginer.UserNo);
            ViewBag.BranchList = getBranchList();
            ViewBag.BranchNo = PublicHelper.Get_BranchNo();
            return View();
        } 

        private BranchInfoCollections getBranchList()
        {
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetAllRecords();

        }

        private string getTopMenu(string sSuNo)
        {
            try
            {
                StringBuilder sb = new StringBuilder(); 

                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); 

                PermitInfoCollections lv0Permits = infoBLL.GetRecordsByUserNo(sSuNo, "00000000");

                int i = 0;
                if (lv0Permits != null && lv0Permits.Count > 0)
                {
                    foreach (PermitInfo info0 in lv0Permits)
                    {
                         if(i==0)
                        {
                            sb.Append("<li class=\"nav-item  dl-selected\"><a href = \"javascript: void(0)\" ><i class=\"\"></i><span>"+info0.sPermitName+"</span></a></li>");
                        }
                         else
                        {
                            sb.Append("<li class=\"nav-item\"><a href = \"javascript: void(0)\" ><i class=\"\"></i><span>" + info0.sPermitName + "</span></a></li>");
                        }
                        i++;
                    }
                }
                return sb.ToString();
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        private string getSubMenu(string sSuNo)
        {
            try
            {
                //var config = [{ "id": "StatisReport", "homePage": "TStatis", "menu": [{ "text": "业务管理", "icon": "", "collapsed": true, "items": [{ "id": "TStatis", "text": "分诊台管理", "href": "/PubData/Test/Index", "icon": "" }, { "id": "TFlows", "text": "医生管理", "href": "/PubData/Test/Index", "icon": "" }, { "id": "EStatis", "text": "挂号管理", "href": "/PubData/Test/Index", "icon": "" }, { "id": "EFlows", "text": "收费管理", "href": "/PubData/Test/Index", "icon": "" }] }] }, { "id": "SystemManage", "homePage": "SUsers", "menu": [{ "text": "系统管理", "icon": "", "items": [{ "id": "SUsers", "text": "管理员列表", "href": "/System/SUsers/List", "icon": "" }, { "id": "Setting", "text": "系统设置", "href": "/System/Setting/Index", "icon": "" }] }] }];

                StringBuilder sb = new StringBuilder();

                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                PermitInfoCollections lv0Permits = infoBLL.GetRecordsByUserNo(sSuNo, "00000000");
                PermitInfoCollections lv1Permits = null;
                PermitInfoCollections lv2Permits = null;

                if (lv0Permits!=null&&lv0Permits.Count>0)
                {
                    sb.Append("[");
                    foreach (PermitInfo lv0Permit in lv0Permits)
                    {
                        sb.Append("{\"id\":\"" + lv0Permit.sPermitCode + "\",\"homePage\":\"" + lv0Permit.sPFunction + "\",\"menu\":[");
                        lv1Permits = infoBLL.GetRecordsByUserNo(sSuNo, lv0Permit.sPermitCode);
                        if(lv1Permits!=null&&lv1Permits.Count>0)
                        {
                            foreach(PermitInfo lv1Permit in lv1Permits)
                            {
                                sb.Append("{ \"text\": \""+ lv1Permit.sPermitName+ "\", \"icon\": \""+ lv1Permit.sPPicture+ "\", \"collapsed\": true, \"items\":[");
                                lv2Permits = infoBLL.GetRecordsByUserNo(sSuNo, lv1Permit.sPermitCode);
                                if(lv2Permits!=null&&lv2Permits.Count>0)
                                {
                                    foreach(PermitInfo lv2Permit in lv2Permits)
                                    {
                                        sb.Append("{ \"id\": \"" + lv2Permit.sPermitCode + "\", \"text\": \"" + lv2Permit.sPermitName + "\", \"href\": \"" + lv2Permit.sPFunction + "\", \"icon\": \"" + lv2Permit.sPPicture + "\" },");
                                    }
                                    sb.Length -= 1;
                                }
                                sb.Append("]},");
                            }
                            sb.Length -= 1;
                        }
                        sb.Append("]},");
                    }
                    sb.Length -= 1; 
                    sb.Append("]");
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public ActionResult UpdateBranch(string branchNo)
        { 
            JsonxHelper json = new JsonxHelper() { Message = "保存失败", Status = "Failure" };

            if (!string.IsNullOrEmpty(branchNo))
            {
                PublicHelper.SetConfigValue("BranchNo", branchNo); 

                json.Message = "保存成功";
                json.Status = "Success";
            }
            else
            {
                json.Message = "保存信息时发生内部错误！" ;
                json.Status = "Failure";
            }
            return Json(json);
        }
    }
}