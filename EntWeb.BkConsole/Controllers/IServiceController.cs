using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntWeb.BkConsole.Entities;
using EntWeb.BkConsole.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole.Controllers
{
    public class IServiceController : Controller
    {
        #region 信息发布接口
        public string getServTime()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return JsonConvert.SerializeObject(codeData);
        }
        public string getServTime2()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = DateTime.Now.ToString("yyyyMMdd.HHmmss");

            return JsonConvert.SerializeObject(codeData);
        }

        public string getWrokState(string id)
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
                    if (info.iIsAutoLogon == 1 || info.iLogonState == 1)
                    {
                        codeData.data = "true";
                    }
                    else
                    {
                        codeData.data = "false";
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
        public string getPrograms(string devcode)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                List<ProgramData> modelList = new List<ProgramData>();
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 10, "");

                if (infoColl != null && infoColl.Count > 0)
                {
                    ProgramData model = null;
                    foreach (DsProgramInfo info in infoColl)
                    {
                        model = new ProgramData();
                        model.programNo = info.sProgmName;
                        model.programName = info.sPClassNo;

                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
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

        public string getProgram(string programId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsProgramInfo info = infoBLL.GetRecordByNo(programId);

                if (info != null)
                {
                    ProgramData model = new ProgramData();
                    model.programNo = info.sProgmName;
                    model.programName = info.sPClassNo;

                    codeData.data = JsonConvert.SerializeObject(model);
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
        #endregion

        #region 基本排队接口
        public string getAllBranchs()
        {
            return QueueServImpl.CreateInstance().getAllBranchs();
        }

        public string getAllServices(string branchNo)
        {
            return QueueServImpl.CreateInstance().getAllServices(branchNo);
        }

        public string getService(string id)
        {
            return QueueServImpl.CreateInstance().getService(id);
        }
        public string getAllCounters(string branchNo)
        {
            return QueueServImpl.CreateInstance().getAllCounters(branchNo);
        }
        public string getCounter(string id)
        {
            return QueueServImpl.CreateInstance().getCounter(id);
        }
        public string getAllStaffers(string branchNo)
        {
            return QueueServImpl.CreateInstance().getAllStaffers(branchNo);
        }
        public string getStaffer(string id)
        {
            return QueueServImpl.CreateInstance().getStaffer(id);
        }
        public string getTicket(string branchNo,string ticketNo)
        {
            return QueueServImpl.CreateInstance().getTicket(branchNo, ticketNo);
        }
        public string getWaitingNumByServiceId(string branchNo,string serviceId)
        {
            return QueueServImpl.CreateInstance().getWaitingNumByServiceId(branchNo, serviceId);
        }
        public string getWaitingNumByCounterId(string branchNo, string counterId)
        {
            return QueueServImpl.CreateInstance().getWaitingNumByCounterId(branchNo, counterId);
        }
        public string getWaitingNumByStafferId(string branchNo, string stafferId)
        {
            return QueueServImpl.CreateInstance().getWaitingNumByStafferId(branchNo, stafferId);
        } 

        public string getTicketByServiceId(string serviceid, string uname, string sex, string age, string phone, string cardno, string workdate)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            { 
                codeData.data = doAddNewTicket(serviceid, uname, phone, cardno, sex, age, workdate);
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getRegistFlowByPatientId(string patId,int inTime)
        {
            return QueueServImpl.CreateInstance().getRegistFlowsByPatientId(patId, inTime);
        }

        #endregion

        #region 预约信息
        public string getRegistNumByStafferId(string stafferId, string startDate, string endDate)
        {
            return QueueServImpl.CreateInstance().getRegistNumByStafferId( stafferId, startDate, endDate);
        }
        public string getRegistNumByServiceId(string serviceId, string startDate, string endDate)
        {
            return QueueServImpl.CreateInstance().getRegistNumByServiceId(serviceId, startDate, endDate);
        }
        #endregion

        #region  插入票号
        //插入一新票号
        private string doAddNewTicket(string sServiceNo, string sName, string sPhone, string sCardNo, string sSex, string sAge, string sWorkDate)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string doInsertTicketFlow(string sTicketNo, string sParams, string sName, string sPhone, string sCardNo, string sStrResult)
        {
            try
            {
                

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string doInsertProcessFlow(string sTFlowNo, string sServiceNo, int iStartNum, string sWorkFlowsNo, int iWFlowsIndex)
        {
            try
            {
                

                return "";
            }
            catch (Exception ex)
            {
                //throw new Exception("插入取票流水时出错！" + ex.Message);
                return "";
            }
        }
        private string getNextWFlowServiceNo(string sWorkFlowNo, int WFlowIndex)
        {
            try
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                ServiceInfo info = infoBoss.GetRecordByNo(sWorkFlowNo);

                string sResult = "";

                if (info != null)
                {
                    string[] serviceList = info.sWorkflowValue.Split(';');
                    if (serviceList.Length > WFlowIndex)
                    {
                        sResult = serviceList[WFlowIndex];
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string getCounterGroupByServiceNo(string sServiceNo)
        {
            StringBuilder sb = new StringBuilder();
            if (sServiceNo != null && sServiceNo.Length > 0)
            {
                int count = 0;

                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1000, "ServiceGroupValue Like '%" + sServiceNo + ":%'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sb.Append(info.sCounterNo + ";");
                    }
                }
            }
            return sb.ToString();
        }

        #endregion
         


    }
}