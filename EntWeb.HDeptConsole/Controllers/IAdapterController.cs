using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EntWeb.HDeptConsole.Controllers
{
    public class IAdapterController : Controller
    {
        // GET: IAdapter
        public ActionResult Index()
        {
            return View();
        }

        public string GetServiceList()
        {
            int count = 0;
            List<ItemObject> itemList = new List<ItemObject>();
            ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            ServiceInfoCollections infoColl=infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='"+PublicHelper.Get_BranchNo()+"' ");

            if (infoColl != null && infoColl.Count > 0)
            {
                foreach(ServiceInfo info in infoColl)
                {
                    ItemObject item = new ItemObject();
                    item.Name = info.sServiceName;
                    item.Value = info.sServiceNo;

                    itemList.Add(item);
                }

                return JsonConvert.SerializeObject(itemList);
            }
            return "";
        }

        public string GetStafferList()
        {
            int count = 0;
            List<ItemObject> itemList = new List<ItemObject>();
            StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, " BranchNo='" + PublicHelper.Get_BranchNo() + "' ");

            if (infoColl != null && infoColl.Count > 0)
            {
                foreach (StafferInfo info in infoColl)
                {
                    ItemObject item = new ItemObject();
                    item.Name = info.sStafferName;
                    item.Value = info.sStafferNo;

                    itemList.Add(item);
                }

                return JsonConvert.SerializeObject(itemList);
            }
            return "";
        }

        //刷卡挂号
        public string RegisteReadCardByService(string serviceno, string worktime, string userno,string username, string age, string sex, string idno,string ricardno, string telphone)
        {
            try
            {
                string ruserno = BusinessHelper.AddRUserInfo(userno, username, sex, idno, age, telphone, "", ricardno);

                if (!string.IsNullOrEmpty(ruserno))
                {
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlows info = new RegistFlows();

                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.iDataFlag = 0;
                    info.sRUserNo = ruserno;
                    info.iRegistType = PublicConsts.REGISTETYPE1;
                    info.sDataFrom = "刷卡挂号";
                    info.dRegistDate = DateTime.Now;
                    info.sServiceNo = serviceno;
                    info.sStafferNo = "";
                    info.iWorkTime = int.Parse(worktime);
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iRegistState = 0;

                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        BusinessHelper.EnqueueRegistUser(info, serviceno, "");
                        return ("Success");
                    }
                }
            }
            catch(Exception ex) { }
            return "Error";
        }

        //刷卡报到
        public string EnqueueReadCardByService(string serviceno, string ricardno)
        {
            int count = 0;
            DateTime workDate = DateTime.Now; 

            if (!string.IsNullOrEmpty(ricardno))
            {
                RUsersInfo rusreInfo = PageService.GetRUserInfoByRiCardNo(ricardno);
                if (rusreInfo != null)
                {
                    string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + rusreInfo.sRUserNo + "' And RegistState=0 And  RegistDate Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                    RegistFlows info = null;

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        BusinessHelper.EnqueueRegistUser(info, serviceno, ""); 
                        return ("Success"); 
                    }
                }
            }
            return "Error";
        }

        //扫码挂号
        public string RegisteScanCodeByService(string serviceno, string worktime, string userno, string username, string age, string sex, string idno, string ricardno, string telphone)
        {
            try
            {
                string ruserno = BusinessHelper.AddRUserInfo(userno, username, sex, idno, age, telphone, "", ricardno);

                if (!string.IsNullOrEmpty(ruserno))
                {
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlows info = new RegistFlows();

                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.iDataFlag = 0;
                    info.sRUserNo = ruserno;
                    info.iRegistType = PublicConsts.REGISTETYPE1;
                    info.sDataFrom = "扫码挂号";
                    info.dRegistDate = DateTime.Now;
                    info.sServiceNo = serviceno;
                    info.sStafferNo = "";
                    info.iWorkTime = int.Parse(worktime);
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iRegistState = 0;

                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        BusinessHelper.EnqueueRegistUser(info, serviceno, "");
                        return ("Success");
                    }
                }
            }
            catch (Exception ex) { }
            return "Error";
        }

        //扫码报到
        public string EnqueueScanCardByService(string serviceno, string ruserNo)
        {
            int count = 0;
            DateTime workDate = DateTime.Now;

            if (!string.IsNullOrEmpty(ruserNo))
            {
                string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + ruserNo + "' And RegistState=0 And  RegistDate Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                RegistFlows info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    BusinessHelper.EnqueueRegistUser(info, serviceno, "");
                    return ("Success");
                }
            }
            return "Error";
        }


        //刷卡挂号
        public string RegisteReadCardByStaffer(string stafferno, string worktime, string userno, string username, string age, string sex, string idno, string ricardno, string telphone)
        {
            try
            {
                string ruserno = BusinessHelper.AddRUserInfo(userno, username, sex, idno, age, telphone, "", ricardno);

                if (!string.IsNullOrEmpty(ruserno))
                {
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlows info = new RegistFlows();

                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.iDataFlag = 0;
                    info.sRUserNo = ruserno;
                    info.iRegistType = PublicConsts.REGISTETYPE1;
                    info.sDataFrom = "刷卡挂号";
                    info.dRegistDate = DateTime.Now;
                    info.sServiceNo = "";
                    info.sStafferNo = stafferno;
                    info.iWorkTime = int.Parse(worktime);
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iRegistState = 0;

                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        BusinessHelper.EnqueueRegistUser(info, "", stafferno);
                        return ("Success");
                    }
                }
            }
            catch (Exception ex) { }
            return "Error";
        }

        //刷卡报到
        public string EnqueueReadCardByStaffer(string stafferno, string ricardno)
        {
            int count = 0;
            DateTime workDate = DateTime.Now;

            if (!string.IsNullOrEmpty(ricardno))
            {
                RUsersInfo rusreInfo = PageService.GetRUserInfoByRiCardNo(ricardno);
                if (rusreInfo != null)
                {
                    string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + rusreInfo.sRUserNo + "' And RegistState=0 And  RegistDate Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                    RegistFlows info = null;

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        BusinessHelper.EnqueueRegistUser(info, "", stafferno);
                        return ("Success");
                    }
                }
            }
            return "Error";
        }

        //扫码挂号
        public string RegisteScanCodeByStaffer(string stafferno, string worktime, string userno, string username, string age, string sex, string idno, string ricardno, string telphone)
        {
            try
            {
                string ruserno = BusinessHelper.AddRUserInfo(userno, username, sex, idno, age, telphone, "", ricardno);

                if (!string.IsNullOrEmpty(ruserno))
                {
                    RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    RegistFlows info = new RegistFlows();

                    info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                    info.iDataFlag = 0;
                    info.sRUserNo = ruserno;
                    info.iRegistType = PublicConsts.REGISTETYPE1;
                    info.sDataFrom = "扫码挂号";
                    info.dRegistDate = DateTime.Now;
                    info.sServiceNo = "";
                    info.sStafferNo = stafferno;
                    info.iWorkTime = int.Parse(worktime);
                    info.dStartDate = DateTime.Now;
                    info.dEnditDate = DateTime.Now;
                    info.iRegistState = 0;

                    info.sBranchNo = PublicHelper.Get_BranchNo();
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = PublicHelper.Get_AppCode() + ";";

                    if (infoBLL.AddNewRecord(info))
                    {
                        BusinessHelper.EnqueueRegistUser(info, "", stafferno);
                        return ("Success");
                    }
                }
            }
            catch (Exception ex) { }
            return "Error";
        }

        //扫码报到
        public string EnqueueScanCardByStaffer(string stafferno, string ruserNo)
        {
            int count = 0;
            DateTime workDate = DateTime.Now;

            if (!string.IsNullOrEmpty(ruserNo))
            {
                string sWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "' And RUserNo ='" + ruserNo + "' And RegistState=0 And  RegistDate Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
                RegistFlows info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    BusinessHelper.EnqueueRegistUser(info, "", stafferno);
                    return ("Success");
                }
            }
            return "Error";
        }

    }
}