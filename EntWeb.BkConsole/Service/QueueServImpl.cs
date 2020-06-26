using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntWeb.BkConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EntWeb.BkConsole.Service
{
    public class QueueServImpl
    {
        private volatile static QueueServImpl _instance = null;
        private static readonly object lockHelper = new object(); 

        public static QueueServImpl CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new QueueServImpl();
                }
            }
            return _instance;
        }

        private QueueServImpl() { }

        public string getServTime()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return JsonConvert.SerializeObject(codeData);
        } 
        public string getAllBranchs()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                List<BranchData> modelList = new List<BranchData>();
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                BranchInfoCollections infoColl = infoBLL.GetAllRecords();

                if (infoColl != null && infoColl.Count > 0)
                {
                    BranchData model = null;
                    foreach (BranchInfo info in infoColl)
                    {
                        model = new BranchData();
                        model.BranchNo = info.sBranchNo;
                        model.BranchName = info.sBranchName;

                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getAllServices(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<ServiceData> modelList = new List<ServiceData>();
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    ServiceData model = null;
                    foreach (ServiceInfo info in infoColl)
                    {
                        if (info.iHaveChild == 0)
                        {
                            model = new ServiceData();
                            model.ServiceNo = info.sServiceNo;
                            model.ServiceName = info.sServiceName;
                            model.ServiceAlias = info.sServiceAlias;
                            model.ServiceType = info.sServiceType;
                            model.AmLimit = info.iAMLimit == 1 ? "true" : "false";
                            model.AmStartTime = info.dAMStartTime.ToString("HH:mm:ss");
                            model.AmEndTime = info.dAMEndTime.ToString("HH:mm:ss");
                            model.PmLimit = info.iPMLimit == 1 ? "true" : "false";
                            model.PmStartTime = info.dPMStartTime.ToString("HH:mm:ss");
                            model.PmEndTime = info.dPMEndTime.ToString("HH:mm:ss");
                            model.WeekLimit = info.iWeekLimit == 1 ? "true" : "false";
                            model.WeekDays = info.sWeekDays;

                            modelList.Add(model);
                        }
                    }
                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getService(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    ServiceData model = new ServiceData();
                    model.ServiceNo = info.sServiceNo;
                    model.ServiceName = info.sServiceName;
                    model.ServiceAlias = info.sServiceAlias;
                    model.ServiceType = info.sServiceType;
                    model.AmLimit = info.iAMLimit == 1 ? "true" : "false";
                    model.AmStartTime = info.dAMStartTime.ToString("HH:mm:ss");
                    model.AmEndTime = info.dAMEndTime.ToString("HH:mm:ss");
                    model.PmLimit = info.iPMLimit == 1 ? "true" : "false";
                    model.PmStartTime = info.dPMStartTime.ToString("HH:mm:ss");
                    model.PmEndTime = info.dPMEndTime.ToString("HH:mm:ss");
                    model.WeekLimit = info.iWeekLimit == 1 ? "true" : "false";
                    model.WeekDays = info.sWeekDays;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getAllCounters(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<CounterData> modelList = new List<CounterData>();
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    CounterData model = null;
                    foreach (CounterInfo info in infoColl)
                    {
                        model = new CounterData();
                        model.CounterNo = info.sCounterNo;
                        model.CounterName = info.sCounterName;
                        model.CounterAlias = info.sCounterAlias;
                        model.LogonState = info.iLogonState;
                        model.PauseState = info.iPauseState;
                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getCounter(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                CounterInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    CounterData model = new CounterData();
                    model.CounterNo = info.sCounterNo;
                    model.CounterName = info.sCounterName;
                    model.CounterAlias = info.sCounterAlias;
                    model.LogonState = info.iLogonState;
                    model.PauseState = info.iPauseState;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getAllStaffers(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<StafferData> modelList = new List<StafferData>();
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    StafferData model = null;
                    foreach (StafferInfo info in infoColl)
                    {
                        model = new StafferData();
                        model.StafferNo = info.sStafferNo;
                        model.LoginId = info.sLoginId;
                        model.StafferName = info.sStafferName;
                        model.OrganizName = info.sOrganizName;
                        model.StarLevel = info.sStarLevel;
                        model.HeadPhoto = info.sHeadPhoto;
                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getStaffer(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                StafferInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    StafferData model = new StafferData();
                    model.StafferNo = info.sStafferNo;
                    model.LoginId = info.sLoginId;
                    model.StafferName = info.sStafferName;
                    model.OrganizName = info.sOrganizName;
                    model.StarLevel = info.sStarLevel;
                    model.HeadPhoto = info.sHeadPhoto;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getTicket(string branchNo, string ticketNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

                string where = " BranchNo='" + branchNo + "' And TicketNo='" + ticketNo + "'  And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                ViewTicketFlowsCollections vTicketFlowColl = ticketBoss.GetRecordsByPaging(ref count, 1, 1, where);

                if (vTicketFlowColl != null && vTicketFlowColl.Count > 0)
                {
                    ViewTicketFlows info = vTicketFlowColl.GetFirstOne();
                    if (info != null)
                    {
                        TicketViewData model = new TicketViewData();
                        model.PFlowNo = info.sPFlowNo;
                        model.TicketNo = info.sTicketNo;

                        codeData.data = JsonConvert.SerializeObject(model);
                    }
                }
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByServiceId(string branchId, string serviceId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = PublicHelper.getProcessingCountByServiceNo(branchId, serviceId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByCounterId(string branchId, string counterId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = PublicHelper.getProcessingCountByCounterNo(branchId, counterId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByStafferId(string branchId, string stafferId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = PublicHelper.getProcessingCountByStafferNo(branchId, stafferId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByPatientId(string branchId, string patientId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = PublicHelper.getProcessingCountByStafferNo(branchId, patientId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getRegistFlowsByPatientId(string patId, int inTime)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "1";
            codeData.data = "";
            try
            {
                int count = 0;
                string strWhere = "RegistState=0 And  RUserNo='"+ patId + "' "; 
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, inTime, 100, strWhere);
                List<RegistData> registList = new List<RegistData>();
                RegistData registData = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewRegistFlows info in infoColl)
                    {

                        registData = new RegistData();

                        registData.id = info.iID.ToString();
                        registData.dpId = info.sBranchNo;
                        registData.dpName = PageHelper.getBranchInfoNameByNo(info.sBranchNo);
                        registData.dpLocation = "门诊主大楼";
                        registData.sortDate = info.dRegistDate.ToString("yyyy-MM-dd");
                        registData.sortTime = info.dRegistDate.ToString("HH:mm:ss");
                        registData.itemName = PageHelper.getServiceInfoNameByNo(info.sServiceNo);
                        registData.itemTip = PageHelper.getServiceInfoByNo(info.sServiceNo).sComments;
                        registData.sortNo = "0";
                        registData.waitingNo = infoBoss.GetCountByCondition(strWhere + " And ServiceNo='" + info.sServiceNo + "' And RegistDate<'" + info.dRegistDate.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
                        registData.status = "预约";

                        registList.Add(registData);
                    }
                    codeData.data = JsonConvert.SerializeObject(registList);
                }

            }
            catch (Exception ex)
            {
                codeData.msg = "error,"+ex.Message;
                codeData.code = "0";
            }
                return JsonConvert.SerializeObject(codeData);
        }
         
        public string getRegistFlowsByPatientId(string branchId,string patId, int inTime)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "1";
            codeData.data = "";
            try
            {
                int count = 0;
                string strWhere = " BranchNo='"+branchId+"' And RegistState=0 And  RUserNo='" + patId + "' ";
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, inTime, 100, strWhere);
                List<RegistData> registList = new List<RegistData>();
                RegistData registData = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewRegistFlows info in infoColl)
                    {

                        registData = new RegistData();

                        registData.id = info.iID.ToString();
                        registData.dpId = info.sBranchNo;
                        registData.dpName = PageHelper.getBranchInfoNameByNo(info.sBranchNo);
                        registData.dpLocation = "门诊主大楼";
                        registData.sortDate = info.dRegistDate.ToString("yyyy-MM-dd");
                        registData.sortTime = info.dRegistDate.ToString("HH:mm:ss");
                        registData.itemName = PageHelper.getServiceInfoNameByNo(info.sServiceNo);
                        registData.itemTip = PageHelper.getServiceInfoByNo(info.sServiceNo).sComments;
                        registData.sortNo = "0";
                        registData.waitingNo = infoBoss.GetCountByCondition(strWhere + " And ServiceNo='" + info.sServiceNo + "' And RegistDate<'" + info.dRegistDate.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
                        registData.status = "预约";

                        registList.Add(registData);
                    }
                    codeData.data = JsonConvert.SerializeObject(registList);
                }

            }
            catch (Exception ex)
            {
                codeData.msg = "error," + ex.Message;
                codeData.code = "0";
            }
            return JsonConvert.SerializeObject(codeData);
        }

        public string getRegistFlowsByPatientId2(string branchIds, string patId, int inTime)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "1";
            codeData.data = "";
            try
            {
                int count = 0;
                branchIds = branchIds.Replace(";", "','");
                string strWhere = " BranchNo In ('" + branchIds + "') And RegistState=0 And  RUserNo='" + patId + "' ";
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, inTime, 100, strWhere);
                List<RegistData> registList = new List<RegistData>();
                RegistData registData = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewRegistFlows info in infoColl)
                    {

                        registData = new RegistData();

                        registData.id = info.iID.ToString();
                        registData.dpId = info.sBranchNo;
                        registData.dpName = PageHelper.getBranchInfoNameByNo(info.sBranchNo);
                        registData.dpLocation = "门诊主大楼";
                        registData.sortDate = info.dRegistDate.ToString("yyyy-MM-dd");
                        registData.sortTime = info.dRegistDate.ToString("HH:mm:ss");
                        registData.itemName = PageHelper.getServiceInfoNameByNo(info.sServiceNo);
                        registData.itemTip = PageHelper.getServiceInfoByNo(info.sServiceNo).sComments;
                        registData.sortNo = "0";
                        registData.waitingNo = infoBoss.GetCountByCondition(strWhere + " And ServiceNo='" + info.sServiceNo + "' And RegistDate<'" + info.dRegistDate.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
                        registData.status = "预约";

                        registList.Add(registData);
                    }
                    codeData.data = JsonConvert.SerializeObject(registList);
                }

            }
            catch (Exception ex)
            {
                codeData.msg = "error," + ex.Message;
                codeData.code = "0";
            }
            return JsonConvert.SerializeObject(codeData);
        }
        public string getRegistFlowsByPatientIdNo(string branchIds, string patIdNo, int inTime)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "1";
            codeData.data = "";
            try
            {
                int count = 0;
                branchIds = branchIds.Replace(";", "','");
                string strWhere = " BranchNo In ('" + branchIds + "') And RegistState=0 And  IdCardNo='" + patIdNo + "' ";
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, inTime, 100, strWhere);
                List<RegistData> registList = new List<RegistData>();
                RegistData registData = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewRegistFlows info in infoColl)
                    {

                        registData = new RegistData();

                        registData.id = info.iID.ToString();
                        registData.dpId = info.sBranchNo;
                        registData.dpName = PageHelper.getBranchInfoNameByNo(info.sBranchNo);
                        registData.dpLocation = "门诊主大楼";
                        registData.sortDate = info.dRegistDate.ToString("yyyy-MM-dd");
                        registData.sortTime = info.dRegistDate.ToString("HH:mm:ss");
                        registData.itemName = PageHelper.getServiceInfoNameByNo(info.sServiceNo);
                        registData.itemTip = PageHelper.getServiceInfoByNo(info.sServiceNo).sComments;
                        registData.sortNo = "0";
                        registData.waitingNo = infoBoss.GetCountByCondition(strWhere + " And ServiceNo='" + info.sServiceNo + "' And RegistDate<'" + info.dRegistDate.ToString("yyyy-MM-dd HH:mm:ss") + "'").ToString();
                        registData.status = "预约";

                        registList.Add(registData);
                    }
                    codeData.data = JsonConvert.SerializeObject(registList);
                }

            }
            catch (Exception ex)
            {
                codeData.msg = "error," + ex.Message;
                codeData.code = "0";
            }
            return JsonConvert.SerializeObject(codeData);
        }

        public string getRegistFlowsByBranchId(string branchNo,string pageIndex,string pageSize,string strWhere)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), strWhere);
                
                codeData.data = JsonConvert.SerializeObject(infoColl);
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        
        public string getRegistNumByStafferId(string stafferId, string startDate,string enditDate)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 

                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                int count = infoBoss.GetCountByCondition(" RegistType=2 And StafferNo='" + stafferId + "' And  RegistDate Between '" + startDate + "' And '" + enditDate + "'"); 

                codeData.data = count+"";

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getRegistNumByServiceId(string serviceId, string startDate, string enditDate)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 

                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                int count = infoBoss.GetCountByCondition(" RegistType=2 And ServiceNo='" + serviceId + "' And  RegistDate Between '"+ startDate+ "' And '"+ enditDate+ "'");

                codeData.data = count + "";

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getRegistNumByServiceName(string serviceName, string startDate, string enditDate)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 

                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                int count = infoBoss.GetCountByCondition(" RegistType=2 And ServiceNo='" + PageHelper.getServiceInfoNoByName(serviceName) + "' And  RegistDate Between '" + startDate + "' And '" + enditDate + "'");

                codeData.data = count + "";

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
    }
}