using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Areas.Common.Controllers
{
    public class RegistDataController : frmMainController
    {
        public RegistDataController()
        {
            viewExpress = PublicHelper.Get_ViewExpress();
            viewExpress.ViewTag = "RegisteView";
            ViewBag.ViewExpress = viewExpress;
        }

        // GET: Common/RegistData
        override
        public ActionResult Index()
        {
            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");
            if (sWorkingMode.Equals("SERVICE"))
            {
                ViewBag.ItemList = PageService.GetServiceList(true); 
            }
            else
            {
                ViewBag.ItemList = PageService.GetStaffList(true);
            }

            ViewBag.WorkingMode = sWorkingMode;
            return View();
        } 
        public ActionResult getDataList_Regist(int pageIndex = 1, int pageSize = 20, string condition = "")
        {
            try
            {
                TableData tdata = new TableData();
                string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RegistState = 0  ";
                //string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RegistState = 0  And RegistType=2 ";

                if (!string.IsNullOrEmpty(condition))
                {
                    strWhere += " And "+condition;
                }

                //if(strWhere.IndexOf("RegistDate") <0)
                //{
                //    strWhere += " And RegistDate >= '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"' ";
                //} 

                ViewRegistFlowsBLL infoBLL = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewRegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref PageCount, pageIndex, pageSize, strWhere);
                int count = infoBLL.GetCountByCondition(strWhere);

                tdata.code = 0;
                tdata.msg = "";
                tdata.count = count;
                tdata.data = PublicHelper.ConvertRegistList(infoColl);

                return Json(tdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddRegist()
        {
            ViewRegistFlows info = new ViewRegistFlows();
            info.sRFlowNo= CommonHelper.Get_New12ByteGuid();
            info.dRegistDate = DateTime.Now.AddDays(1);
            info.sServiceNo = "";
            info.sStafferNo = "";
            info.iWorkTime = 1;
            info.sCnName = "";
            info.iAge = 20;
            info.iSex = 1;
            info.sIdCardNo = "";
            info.sTelphone = "";

            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");
            if (sWorkingMode.Equals("SERVICE"))
            {
                ViewBag.ItemList = PageService.GetServiceList();
            }
            else
            {
                ViewBag.ItemList = PageService.GetStaffList();
            }
            ViewBag.WTimeList = PageService.GetWorkTimeList();
            ViewBag.WorkingMode = sWorkingMode;
            ViewBag.StackHolder = info;

            return View("EditRegist");
        }

        public ActionResult EditRegist(string registNo)
        {
            ViewRegistFlowsBLL infoBLL = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            ViewRegistFlows info = infoBLL.GetRecordByNo(registNo);

            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");
            if (sWorkingMode.Equals("SERVICE"))
            {
                ViewBag.ItemList = PageService.GetServiceList();
            }
            else
            {
                ViewBag.ItemList = PageService.GetStaffList();
            }
            ViewBag.WTimeList = PageService.GetWorkTimeList();
            ViewBag.WorkingMode = sWorkingMode;
            ViewBag.StackHolder = info;

            return View();
        }

        public void SaveRegist(string registNo, string itemNo, string registDate, string workTime, string ruserName, string ruserSex, string idNo, string age, string telphone)
        {
            string SuNo = ((LoginerInfo)Session["loginUser"]).UserNo;
            string sWorkingMode = PublicHelper.GetParamValue(PublicConsts.DEF_WORKINGMODE, "Others");

            RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RegistFlows info = infoBLL.GetRecordByNo(registNo);

            if (info != null)
            {               
                info.sRUserNo = BusinessHelper.AddRUserInfo("aa",ruserName, ruserSex, idNo, age, telphone, SuNo); 
                info.iRegistType = PublicConsts.REGISTETYPE2;
                //info.sDataFrom = "预约挂号";
                info.dRegistDate = DateTime.Parse(registDate);
                info.sServiceNo = sWorkingMode.Equals("SERVICE") ? itemNo : "";
                info.sStafferNo = sWorkingMode.Equals("STAFF") ? itemNo : "";
                info.iWorkTime = int.Parse(workTime);
                info.dStartDate = DateTime.Parse(registDate);
                info.dEnditDate = DateTime.Parse(registDate); 

                info.sModOptor = SuNo;
                info.dModDate = DateTime.Now;

                if (infoBLL.UpdateRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            else
            {
                info = new RegistFlows();
                info.sTicketNo = "";
                info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                info.iDataFlag = 0;
                info.sRUserNo = BusinessHelper.AddRUserInfo("aa", ruserName, ruserSex, idNo, age, telphone, SuNo);
                info.iRegistType = PublicConsts.REGISTETYPE2;
                info.sDataFrom = "预约挂号";
                info.dRegistDate = DateTime.Parse(registDate);
                info.sServiceNo = sWorkingMode.Equals("SERVICE") ? itemNo : "";
                info.sStafferNo = sWorkingMode.Equals("STAFF") ? itemNo : "";
                info.iWorkTime = int.Parse(workTime);
                info.dStartDate = DateTime.Parse(registDate);
                info.dEnditDate = DateTime.Parse(registDate);
                info.iRegistState = 0;

                info.sBranchNo = PublicHelper.Get_BranchNo();
                info.sAddOptor = SuNo;
                info.dAddDate = DateTime.Now;
                info.sModOptor = SuNo;
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sComments = "";
                info.sAppCode = PublicHelper.Get_AppCode() + ";";

                if (infoBLL.AddNewRecord(info))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ERROR");
                }
            }
        }

        public void DelRegist(string ids)
        {
            try
            {
                string[] idList = ids.Split(';');

                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                infoBLL.SoftDeleteRecord(idList);

                Response.Write("SUCCESS");
            }
            catch(Exception ex)
            {
                Response.Write("ERROR");
            }
        }

        public string GetRegisteIdByRUserId(string ruserId)
        {
            int count = 0;
            string strWhere = "";
            ViewRegistFlowsBLL infoBLL = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            ViewRegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, strWhere);

            if (infoColl != null && infoColl.Count > 0)
            {
                return infoColl.GetFirstOne().sRFlowNo;
            }
            return "";
        }
    }
}