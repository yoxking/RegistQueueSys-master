using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web.Controls;
using EntWeb.BkConsole.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class IRTicketController : Controller
    {
        // GET: RTicket
        public ActionResult Index()
        {
            return View();
        }

        // 一级科室列表
        public ActionResult BranchList(int pageindex = 1, int pagesize = 10)
        {
            int count = 0;

            List<ItemObject> itemList = new List<ItemObject>();

            BranchInfoBLL branchBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            BranchInfoCollections branchColl = branchBLL.GetRecordsByPaging(ref count, pageindex, pagesize, "");

            PagerHelper pager = new PagerHelper(pageindex, pagesize, count);

            if (branchColl != null && branchColl.Count > 0)
            {
                foreach (BranchInfo info in branchColl)
                {
                    ItemObject item = new ItemObject();
                    item.Name = info.sBranchName;
                    item.Value = info.sBranchNo;

                    itemList.Add(item);
                }
            }

            ViewBag.ItemList = itemList;
            ViewBag.Pager = pager;
            return View();
        }

        // 二级业务列表
        public ActionResult ServiceList(string branchno, int pageindex = 1, int pagesize = 10)
        {

            int count = 0;
            string condition = " BranchNo='" + branchno + "' ";
            List<ItemObject> itemList = new List<ItemObject>();

            ServiceInfoBLL serviceBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            ServiceInfoCollections serviceColl = serviceBLL.GetRecordsByPaging(ref count, pageindex, pagesize, condition);

            PagerHelper pager = new PagerHelper(pageindex, pagesize, count);

            if (serviceColl != null && serviceColl.Count > 0)
            {
                foreach (ServiceInfo info in serviceColl)
                {
                    ItemObject item = new ItemObject();
                    item.Name = info.sServiceName;
                    item.Value = info.sServiceNo;

                    itemList.Add(item);
                }
            }

            ViewBag.BranchNo = branchno;
            ViewBag.ItemList = itemList;
            ViewBag.Pager = pager;
            return View();
        }

        // 二级医生列表
        public ActionResult StaffList(string branchno, int pageindex = 1, int pagesize = 10)
        {

            int count = 0;
            string condition = " BranchNo='" + branchno + "' ";
            List<ItemObject> itemList = new List<ItemObject>();

            StafferInfoBLL serviceBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            StafferInfoCollections serviceColl = serviceBLL.GetRecordsByPaging(ref count, pageindex, pagesize, condition);

            PagerHelper pager = new PagerHelper(pageindex, pagesize, count);

            if (serviceColl != null && serviceColl.Count > 0)
            {
                foreach (StafferInfo info in serviceColl)
                {
                    ItemObject item = new ItemObject();
                    item.Name = info.sStafferName;
                    item.Value = info.sStafferNo;

                    itemList.Add(item);
                }
            }

            ViewBag.BranchNo = branchno;
            ViewBag.ItemList = itemList;
            ViewBag.Pager = pager;
            return View();
        }

        public string GetServiceInfo(string serviceNo)
        {
            ServiceInfo info=PageHelper.getServiceInfoByNo(serviceNo);
            if (info != null)
            {
                return PageHelper.getBranchInfoNameByNo(info.sBranchNo) + "-" + info.sServiceName;
            }
            return "Error";
        }

        public string RegisteRCard(string serviceno, string worktime, string name, string age, string sex, string idno, string ricardno, string telphone)
        {
            try
            {
                string ruserNo = AdapterUtil.AddRUserInfo(name, sex, idno, age, telphone, "", ricardno);

                if (!string.IsNullOrEmpty(ruserNo))
                {
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlows info = new RegistFlows();

                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.iDataFlag = 0;
                    info.sRUserNo = ruserNo;
                    info.iRegistType = PublicConsts.REGISTETYPE1;
                    info.sDataFrom = "虚拟挂号";
                    info.dRegistDate = DateTime.Now;
                    info.sServiceNo = serviceno;
                    info.sStafferNo = "";
                    info.iWorkTime = int.Parse(worktime);
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iRegistState = 0;

                    info.sBranchNo = AdapterUtil.GetBranchNoByServiceNo(serviceno);
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        return ("Success");
                    }
                }
            }
            catch(Exception ex) { }
            return "Error";
        }

    }
}