using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Entities;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EntFrm.DataAdapter.Business
{
    /// <summary>
    /// 青海第五人民医院
    /// </summary>
    public class Qh5thrmAdapter : IAdapterBusiness
    {
        private static bool isRunning = false;
        private static int emCount = 1;

        public bool wipeHrtbeatFlows()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    DsHrtbeatFlowsBLL infoBoss = new DsHrtbeatFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                    infoBoss.HardDeleteByCondition(" RegistDate<'" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 

        /// <summary>
        /// 从HIS更新门诊科室
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateBranchList()
        {
            try
            {
                BranchInfoBLL branchBoss = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                BranchInfo branchInfo = null;
                string branchNo = "";

                DicContentService.DicContentClient dicService = new DicContentService.DicContentClient();                 
                string result=dicService.getDicContentByPage("MDM023", ""); 

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result); 
                XmlNodeList xmlList = xmlDoc.SelectNodes("/dicInfo/declare/entity");
                 
                foreach (XmlElement item in xmlList)
                {
                    branchNo = item["CODE"].InnerText;
                    branchInfo = branchBoss.GetRecordByNo(branchNo);
                    if (branchInfo != null)
                    { 
                        branchInfo.sBranchName = item["NAME"].InnerText; 
                        branchInfo.sSummary = item["MARK"].InnerText;

                        branchBoss.UpdateRecord(branchInfo);
                    }
                    else
                    {

                        branchInfo = new BranchInfo();
                        branchInfo.sBranchNo = branchNo;
                        branchInfo.sBranchName = item["NAME"].InnerText;
                        branchInfo.iBranchType = IPublicConsts.BRANCHTYPE1;
                        branchInfo.sBranchCode = branchNo;
                        branchInfo.sContacts = "";
                        branchInfo.sTelphone = "";
                        branchInfo.sSummary = item["MARK"].InnerText;
                        branchInfo.sAddOptor = "";
                        branchInfo.dAddDate = DateTime.Now;
                        branchInfo.sModOptor = "";
                        branchInfo.dModDate = DateTime.Now;
                        branchInfo.iValidityState = 1;
                        branchInfo.sComments = "";
                        branchInfo.sAppCode = IUserContext.GetAppCode() + ";";

                        branchBoss.AddNewRecord(branchInfo);
                    }
                } 

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 从HIS更新医生
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateStafferList()
        {
            try
            {
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfo staffInfo = null;
                string staffId = null; 

                DicContentService.DicContentClient dicService = new DicContentService.DicContentClient();
                string result = dicService.getDicContentByPage("MDM084", "");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/dicInfo/declare/entity");

                foreach (XmlElement item in xmlList)
                {
                    staffId = item["EMPL_CODE"].InnerText;
                    staffInfo = staffBoss.GetRecordByNo(staffId);
                    if (staffInfo != null)
                    {
                        staffInfo.sStafferName = item["EMPL_NAME"].InnerText;
                        staffInfo.sOrganizNo = item["DEPT_CODE"].InnerText;
                        staffInfo.sOrganizName = item["DEPT_NAME"].InnerText;
                        staffInfo.sSummary = item["LEVL_CODE"].InnerText;
                        staffInfo.sBranchNo = getBranchNoByBranchId(item["DEPT_CODE"].InnerText);

                        staffBoss.UpdateRecord(staffInfo);
                    }
                    else
                    {
                        staffInfo = new StafferInfo();
                        staffInfo.sStafferNo = staffId;
                        staffInfo.sStafferName = item["EMPL_NAME"].InnerText;
                        staffInfo.sLoginId = staffId;
                        staffInfo.sPassword = staffId;
                        staffInfo.sCounterNo = "";
                        staffInfo.sOrganizNo = item["DEPT_CODE"].InnerText;
                        staffInfo.sOrganizName = item["DEPT_NAME"].InnerText;
                        staffInfo.sStarLevel = "五星";
                        staffInfo.sHeadPhoto = "";
                        staffInfo.sSummary = item["LEVL_CODE"].InnerText;
                        staffInfo.sBranchNo = getBranchNoByBranchId(item["DEPT_CODE"].InnerText);
                        //staffInfo.sBranchNo = branchNo;
                        staffInfo.sAddOptor = "";
                        staffInfo.dAddDate = DateTime.Now;
                        staffInfo.sModOptor = "";
                        staffInfo.dModDate = DateTime.Now;
                        staffInfo.iValidityState = 1;
                        staffInfo.sComments = "";
                        staffInfo.sAppCode = IUserContext.GetAppCode() + ";";

                        staffBoss.AddNewRecord(staffInfo);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 从HIS更新医生职称
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private string getStafferRank()
        {
            try
            {
                DicContentService.DicContentClient dicService = new DicContentService.DicContentClient();

                return dicService.getDicContentByPage("MDM053", ""); 
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 从HIS更新医生排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateStafferRota()
        {
            try
            {

                StafferRotaBLL srotaBoss = new StafferRotaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferRota staffRota = null;
                string staffId = null;
                string branchNo = "170858507526";


                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "SEE_DATE";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = "to_date('" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss')";

                whereList.Add(where);

                //where = new WhereData();
                //where.field = "SEE_DATE";
                //where.operate = "LTE";
                //where.relation = "AND";
                //where.value = "to_date('" + DateTime.Now.ToString("yyyy/MM/dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss')";

                //whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("DOCTOR_SCHEDULE", condition);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");

                foreach (XmlElement item in xmlList)
                {
                    staffId = item["DOCT_CODE"].InnerText;
                    staffRota = IBusinessHelper.getStafferRotaByStafferNo(staffId);
                    if (staffRota != null)
                    {
                        staffRota.sStafferNo = staffId;
                        staffRota.iWeekDay1 = IPublicHelper.getWorkTimeType(item["MONDAY"].InnerText);
                        staffRota.iWeekDay2 = IPublicHelper.getWorkTimeType(item["TUESDAY"].InnerText);
                        staffRota.iWeekDay3 = IPublicHelper.getWorkTimeType(item["WEDNESDAY"].InnerText);
                        staffRota.iWeekDay4 = IPublicHelper.getWorkTimeType(item["THURSDAY"].InnerText);
                        staffRota.iWeekDay5 = IPublicHelper.getWorkTimeType(item["FRIDAY"].InnerText);
                        staffRota.iWeekDay6 = IPublicHelper.getWorkTimeType(item["SATURDAY"].InnerText);
                        staffRota.iWeekDay7 = IPublicHelper.getWorkTimeType(item["SUNDAY"].InnerText);
                        staffRota.dModDate = DateTime.Now;

                        srotaBoss.UpdateRecord(staffRota);
                    }
                    else
                    {
                        staffRota = new StafferRota();
                        staffRota.sRotaNo = CommonHelper.Get_New12ByteGuid();
                        staffRota.sStafferNo = staffId;
                        staffRota.iRotaType = IPublicConsts.ROTATYPE1;//正常排班
                        staffRota.dStartDate = DateTime.Now;
                        staffRota.dEnditDate = DateTime.Now;
                        staffRota.iWeekDay1 = IPublicHelper.getWorkTimeType(item["MONDAY"].InnerText);
                        staffRota.iWeekDay2 = IPublicHelper.getWorkTimeType(item["TUESDAY"].InnerText);
                        staffRota.iWeekDay3 = IPublicHelper.getWorkTimeType(item["WEDNESDAY"].InnerText);
                        staffRota.iWeekDay4 = IPublicHelper.getWorkTimeType(item["THURSDAY"].InnerText);
                        staffRota.iWeekDay5 = IPublicHelper.getWorkTimeType(item["FRIDAY"].InnerText);
                        staffRota.iWeekDay6 = IPublicHelper.getWorkTimeType(item["SATURDAY"].InnerText);
                        staffRota.iWeekDay7 = IPublicHelper.getWorkTimeType(item["SUNDAY"].InnerText);
                        staffRota.sRotaFormat = "";
                        staffRota.sBranchNo = branchNo;
                        staffRota.sAddOptor = "";
                        staffRota.dAddDate = DateTime.Now;
                        staffRota.sModOptor = "";
                        staffRota.dModDate = DateTime.Now;
                        staffRota.iValidityState = 1;
                        staffRota.sComments = "";
                        staffRota.sAppCode = IUserContext.GetAppCode() + ";";

                        srotaBoss.AddNewRecord(staffRota);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 从HIS更新门诊业务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateServiceList()
        {
            try
            {
                ServiceInfoBLL serviceBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfo serviceInfo = null;
                string serviceId = null;
                string branchNo = "170858507526";

                DicContentService.DicContentClient dicService = new DicContentService.DicContentClient();
                string result = dicService.getDicContentByPage("MDM023", "");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/dicInfo/declare/entity");

                foreach (XmlElement item in xmlList)
                {
                    serviceId = item["CODE"].InnerText;
                    serviceInfo = serviceBoss.GetRecordByNo(serviceId);
                    if (serviceInfo != null)
                    {
                        serviceInfo.sServiceName = item["NAME"].InnerText;
                        serviceInfo.dModDate = DateTime.Now;

                        serviceBoss.UpdateRecord(serviceInfo);
                    }
                    else
                    {
                        serviceInfo = new ServiceInfo();
                        serviceInfo.sServiceNo = serviceId;
                        serviceInfo.sServiceName = item["NAME"].InnerText;
                        serviceInfo.sServiceAlias = item["NAME"].InnerText;
                        serviceInfo.sServiceType = "";
                        serviceInfo.iStartNum = 1;
                        serviceInfo.iEndNum = 1000;
                        serviceInfo.iAlarmNum = 1000;
                        serviceInfo.iDigitLength = 3;
                        serviceInfo.sWorkflowValue = "";
                        serviceInfo.sWorkflowText = "";
                        serviceInfo.sTicketButtonFmt = "";
                        serviceInfo.sTicketStyleNo = "";
                        serviceInfo.iAMLimit = 0;
                        serviceInfo.dAMStartTime = DateTime.Now;
                        serviceInfo.dAMEndTime = DateTime.Now;
                        serviceInfo.iAMTotal = 1000;
                        serviceInfo.iPMLimit = 1000;
                        serviceInfo.dPMStartTime = DateTime.Now;
                        serviceInfo.dPMEndTime = DateTime.Now;
                        serviceInfo.iPMTotal = 1000;
                        serviceInfo.iWeekLimit = 0;
                        serviceInfo.sWeekDays = "1234567";
                        serviceInfo.iPrintPause = 0;
                        serviceInfo.sParentNo = "00000000";
                        serviceInfo.iHaveChild = 0;
                        serviceInfo.iIsShowDialog = 0;
                        serviceInfo.sShowDialogName = "";
                        serviceInfo.sBranchNo = branchNo;

                        serviceInfo.sAddOptor = "";
                        serviceInfo.dAddDate = DateTime.Now;
                        serviceInfo.sModOptor = "";
                        serviceInfo.dModDate = DateTime.Now;
                        serviceInfo.iValidityState = 1;
                        serviceInfo.sComments = "";
                        serviceInfo.sAppCode = IUserContext.GetAppCode() + ";";

                        serviceBoss.AddNewRecord(serviceInfo);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 从HIS更新服务排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateServiceRota()
        {

            try
            {

                ServiceRotaBLL srotaBoss = new ServiceRotaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceRota serviceRota = null;
                string serviceId = null;
                string branchNo = "170858507526";


                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "SEE_DATE";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = "to_date('" + DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss')";

                whereList.Add(where);

                //where = new WhereData();
                //where.field = "SEE_DATE";
                //where.operate = "LTE";
                //where.relation = "AND";
                //where.value = "to_date('" + DateTime.Now.ToString("yyyy/MM/dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss')";

                //whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("DOCTOR_SCHEDULE", condition);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");

                foreach (XmlElement item in xmlList)
                {
                    serviceId = item["DOCT_CODE"].InnerText;
                    serviceRota = IBusinessHelper.getServiceRotaByServiceNo(serviceId);
                    if (serviceRota != null)
                    {
                        serviceRota.sServiceNo = serviceId;
                        serviceRota.iWeekDay1 = IPublicHelper.getWorkTimeType(item["MONDAY"].InnerText);
                        serviceRota.iWeekDay2 = IPublicHelper.getWorkTimeType(item["TUESDAY"].InnerText);
                        serviceRota.iWeekDay3 = IPublicHelper.getWorkTimeType(item["WEDNESDAY"].InnerText);
                        serviceRota.iWeekDay4 = IPublicHelper.getWorkTimeType(item["THURSDAY"].InnerText);
                        serviceRota.iWeekDay5 = IPublicHelper.getWorkTimeType(item["FRIDAY"].InnerText);
                        serviceRota.iWeekDay6 = IPublicHelper.getWorkTimeType(item["SATURDAY"].InnerText);
                        serviceRota.iWeekDay7 = IPublicHelper.getWorkTimeType(item["SUNDAY"].InnerText);
                        serviceRota.dModDate = DateTime.Now;

                        srotaBoss.UpdateRecord(serviceRota);
                    }
                    else
                    {
                        serviceRota = new ServiceRota();
                        serviceRota.sRotaNo = CommonHelper.Get_New12ByteGuid();
                        serviceRota.sServiceNo = serviceId;
                        serviceRota.iRotaType = IPublicConsts.ROTATYPE1;//正常排班
                        serviceRota.dStartDate = DateTime.Now;
                        serviceRota.dEnditDate = DateTime.Now;
                        serviceRota.iWeekDay1 = IPublicHelper.getWorkTimeType(item["MONDAY"].InnerText);
                        serviceRota.iWeekDay2 = IPublicHelper.getWorkTimeType(item["TUESDAY"].InnerText);
                        serviceRota.iWeekDay3 = IPublicHelper.getWorkTimeType(item["WEDNESDAY"].InnerText);
                        serviceRota.iWeekDay4 = IPublicHelper.getWorkTimeType(item["THURSDAY"].InnerText);
                        serviceRota.iWeekDay5 = IPublicHelper.getWorkTimeType(item["FRIDAY"].InnerText);
                        serviceRota.iWeekDay6 = IPublicHelper.getWorkTimeType(item["SATURDAY"].InnerText);
                        serviceRota.iWeekDay7 = IPublicHelper.getWorkTimeType(item["SUNDAY"].InnerText);
                        serviceRota.sRotaFormat = "";
                        serviceRota.sBranchNo = branchNo;
                        serviceRota.sAddOptor = "";
                        serviceRota.dAddDate = DateTime.Now;
                        serviceRota.sModOptor = "";
                        serviceRota.dModDate = DateTime.Now;
                        serviceRota.iValidityState = 1;
                        serviceRota.sComments = "";
                        serviceRota.sAppCode = IUserContext.GetAppCode() + ";";

                        srotaBoss.AddNewRecord(serviceRota);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 从HIS更新医技科室
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateCounterList()
        {
            try
            {
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counterInfo = null;
                string counterId = null;
                string branchNo = "170858507526";

                DicContentService.DicContentClient dicService = new DicContentService.DicContentClient();
                string result = dicService.getDicContentByPage("MDM023", "");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/dicInfo/declare/entity");

                foreach (XmlElement item in xmlList)
                {
                    counterId = item["CODE"].InnerText;
                    counterInfo = counterBoss.GetRecordByNo(counterId);
                    if (counterInfo != null)
                    {
                        counterInfo.sCounterName = item["NAME"].InnerText;
                        counterInfo.dModDate = DateTime.Now;

                        counterBoss.UpdateRecord(counterInfo);
                    }
                    else
                    {

                        counterInfo = new CounterInfo();
                        counterInfo.sCounterNo = counterId;
                        counterInfo.sCounterName = item["NAME"].InnerText;
                        counterInfo.sCounterAlias = item["NAME"].InnerText;
                        counterInfo.sServiceGroupValue = "";
                        counterInfo.sServiceGroupText = "";
                        counterInfo.sVoiceStyleNos = "";
                        counterInfo.sLedDisplayNo = "";
                        counterInfo.iLedAddress = 0;
                        counterInfo.sCallerNo = "";
                        counterInfo.iCallerAddress = 0;
                        counterInfo.iIsAutoLogon = 0;
                        counterInfo.iLogonState = 0;
                        counterInfo.sLogonStafferNo = "";
                        counterInfo.iPauseState = 0;
                        counterInfo.iCalledNum = 0;
                        counterInfo.sBranchNo = branchNo;

                        counterInfo.sAddOptor = "";
                        counterInfo.dAddDate = DateTime.Now;
                        counterInfo.sModOptor = "";
                        counterInfo.dModDate = DateTime.Now;
                        counterInfo.iValidityState = 1;
                        counterInfo.sComments = "";
                        counterInfo.sAppCode = IUserContext.GetAppCode() + ";";

                        counterBoss.AddNewRecord(counterInfo);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 从HIS更新手术信息(6楼)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateOperateList()
        {
            try
            {
                //string registNo = "";//门急诊/住院就诊号
                string branchNo = ""; 
                string patientId = "";
                string dataFrom = "手术室";
                string roomNo = "";
                DateTime workTime = DateTime.Now;
                string operatTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int operatState = 1;
                int count = 0;

                OperateFlowsBLL operateBoss = new OperateFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                OperateFlowsCollections operateFlowColl=null;
                OperateFlows operateFlow = null;

                //List<WhereData> whereList = new List<WhereData>();
                //WhereData where = new WhereData();
                //where.field = "START_DATE_TIME";
                //where.operate = "GTE";
                //where.relation = "AND";
                //where.value = "to_date('" + DateTime.Now.ToString("yyyy/MM/dd 00:00:00") + "','yyyy/mm/dd hh24:mi:ss')";

                //whereList.Add(where);

                //string condition = JsonConvert.SerializeObject(whereList);
                //condition = condition.Replace("operate", "operator");
                string condition = "[{\"field\":\"START_DATE_TIME\",\"operator\":\"GTE\",\"relation\":\"AND\", \"value\":\"to_date(\'"+ DateTime.Now.ToString("yyyy/MM/dd 00:00:00")+"\',\'YYYY/MM/DD HH24:MI:SS\')\"}]";

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("OPER_INFO", condition);
                 
                if (result.IndexOf("无相关数据") >= 0)
                {
                    return true;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");

                foreach (XmlElement item in xmlList)
                {
                    //registNo = item["DEPT_STAYED"].InnerText;  //处方号 
                    patientId = item["PATIENT_ID"].InnerText;
                    roomNo = item["OPERATING_ROOM_NO"].InnerText;
                    operatTime= item["START_DATE_TIME"].InnerText;
                    operatState = int.Parse(item["OPER_STATUS"].InnerText);
                    branchNo = getBranchNoByBranchId(item["DEPT_STAYED"].InnerText);
                    dataFrom = getBranchNameByBranchId(item["DEPT_STAYED"].InnerText);

                    insertPatientInfo(patientId);

                    operateFlowColl = operateBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo='"+ branchNo + "' And RUserNo='"+ patientId + "' And RegistDate Between '"+ workTime.ToString("yyyy-MM-dd 00:00:00")+ "' And '"+ workTime.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ");

                    if (operateFlowColl != null && operateFlowColl.Count > 0)
                    {
                        operateFlow = operateFlowColl.GetFirstOne();
                        operateFlow.sDataFrom = dataFrom;
                        operateFlow.sRoomNo = roomNo;
                        operateFlow.sRoomName = roomNo;
                        operateFlow.iOperatState = operatState;
                        operateFlow.sBranchNo = branchNo;

                        operateFlow.dModDate = DateTime.Now;

                        operateBoss.UpdateRecord(operateFlow);
                    }
                    else
                    { 
                        operateFlow = new OperateFlows();
                        operateFlow.sOFlowNo = CommonHelper.Get_New12ByteGuid();
                        operateFlow.iDataFlag = 0;
                        operateFlow.iOperatType = 1;
                        operateFlow.sDataFrom = dataFrom;
                        operateFlow.sRUserNo = patientId;
                        operateFlow.sStafferNo ="";
                        operateFlow.dRegistDate = DateTime.Now;
                        operateFlow.dOperatTime = DateTime.Parse(operatTime);
                        operateFlow.sRoomNo = roomNo;
                        operateFlow.sRoomName = roomNo;
                        operateFlow.iOptIndex = 0;
                        operateFlow.sOptName = "";
                        operateFlow.sOptDesc ="";
                        operateFlow.sOpter1No = "";
                        operateFlow.sOpter1Name = "";
                        operateFlow.sOpter2No = "";
                        operateFlow.sOpter2Name = "";
                        operateFlow.sOpter3No ="";
                        operateFlow.sOpter3Name = "";
                        operateFlow.sOpter4No = "";
                        operateFlow.sOpter4Name = "";
                        operateFlow.sOpter5No ="";
                        operateFlow.sOpter5Name = "";
                        operateFlow.iOperatState = operatState;

                        operateFlow.sBranchNo = branchNo;

                        operateFlow.sAddOptor = "";
                        operateFlow.dAddDate = DateTime.Now;
                        operateFlow.sModOptor = "";
                        operateFlow.dModDate = DateTime.Now;
                        operateFlow.iValidityState = 1;
                        operateFlow.sComments = "";
                        operateFlow.sAppCode = IUserContext.GetAppCode() + ";";

                        operateBoss.AddNewRecord(operateFlow);
                    } 
                }

                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        /// <summary>
        /// 从HIS更新取药信息(门诊西药房)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRecipeList()
        {
            try
            {
                string registNo = "";//门急诊/住院就诊号
                string branchNo = IPublicHelper.GetBranchNoByName("门诊药房");//门诊药房
                string counterNo = "";
                string patientId = "";
                //
                string dataFrom = "";
                int state = 1;

                RecipeFlowsBLL recipeBoss = new RecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RecipeFlows recipeFlow = null;


                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("YFPD", "");

                if (result.IndexOf("无相关数据") >= 0)
                {
                    return true;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");

                foreach (XmlElement item in xmlList)
                {
                    registNo = item["PRESC_NO"].InnerText;  //处方号
                    //branchNo = item["DRUG_DEPT_CODE"].InnerText;
                    counterNo = getCounterNoByWindowId(item["WIN_NO"].InnerText, branchNo);
                    patientId = item["PATIENT_ID"].InnerText;
                    dataFrom = "药房";

                    insertPatientInfo(patientId);

                    if (!hasRecipeFlowsByRegistNo(counterNo, patientId, branchNo))
                    {
                        recipeFlow = new RecipeFlows();
                        recipeFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();

                        recipeFlow.iDataFlag = 0;
                        recipeFlow.sRegistNo = registNo;
                        recipeFlow.sTicketNo = doGenerate_RecipeTicketNo(counterNo, branchNo);
                        recipeFlow.sRUserNo = patientId;
                        recipeFlow.sCounterNo = counterNo;
                        recipeFlow.dEnqueueTime = DateTime.Now;
                        recipeFlow.dBeginTime = DateTime.Now;
                        recipeFlow.dFinishTime = DateTime.Now;
                        recipeFlow.iRecipeState = 3;
                        recipeFlow.sRecipeOpter = "";
                        recipeFlow.dRecipeDate = DateTime.Now;
                        recipeFlow.iProcessState = state;
                        recipeFlow.dProcessedTime = DateTime.Now;
                        recipeFlow.sPrcsCounterNo = "";
                        recipeFlow.sDataFrom = dataFrom;
                        recipeFlow.sBranchNo = branchNo;

                        recipeFlow.sAddOptor = "";
                        recipeFlow.dAddDate = DateTime.Now;
                        recipeFlow.sModOptor = "";
                        recipeFlow.dModDate = DateTime.Now;
                        recipeFlow.iValidityState = 1;
                        //registFlow.sComments = hisphexam.PatFrom + "," + hisphexam.BranchName + "," + hisphexam.PexamId + "," + hisphexam.PatId + "," + hisphexam.ImgType + "," + hisphexam.UserName;
                        recipeFlow.sComments = registNo;
                        recipeFlow.sAppCode = IUserContext.GetAppCode() + ";";

                        recipeBoss.AddNewRecord(recipeFlow);

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 从HIS更新处方详细
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRecipeDetail()
        {
            return true;
        }

        /// <summary>
        /// 从HIS更新病人挂号信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updatePatientList()
        {
            try
            {
                Thread vThread = new Thread(() =>
                {
                    if (!isRunning)
                    {
                        isRunning = true;

                        MssgQueueService.MqWsSoapClient mqueueService = new MssgQueueService.MqWsSoapClient();
                        string result = "";

                        while (true)
                        {
                            result = mqueueService.AnilysisMQ("10.177.124.22", "APP_SVRCONN", "QLOCAL.OUT.SYS106", "OUT_QM", 1818);
                              
                            if (result.Equals("2"))
                            {
                                emCount++;
                                break;
                            }
                            else
                            {
                                emCount = 0;

                                result = result.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");

                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(result);
                                XmlElement ele = (XmlElement)xmlDoc.SelectSingleNode("/msh/msh-head/msh2");
                                string mssgType = ele.InnerText;
                                ele = (XmlElement)xmlDoc.SelectSingleNode("/msh/msh-body");
                                string mssgBody = ele.InnerXml;

                                switch (mssgType)
                                {
                                    //患者基本信息服务
                                    case "VES301":
                                        break;
                                    //一体机挂号信息服务
                                    case "VES203":
                                    //患者挂号信息服务
                                    case "VES302": 
                                        insertPatientList2(mssgBody);
                                        break;
                                    //检查申请服务
                                    case "VES303":
                                        insertPhexamList(mssgBody);
                                        break;
                                    //医生排班
                                    case "VES305":
                                        break;
                                    //所有药房取药服务
                                    case "VES307":
                                        //insertRecipeList(mssgBody);
                                        break;
                                    //检验申请服务
                                    case "VES310":
                                        insertInspectList(mssgBody);
                                        break;
                                    //门诊西药房取药服务
                                    case "VES311":
                                        //insertRecipeList(mssgBody);
                                        break;
                                        //义幻预约挂号
                                    case "VES317": 
                                        //大象就医预约
                                    case "VES318": 
                                        insertRegisteList(mssgBody);
                                        break;
                                    default: break;
                                }
                            }
                        }

                        //等候2~n秒
                        if (emCount > 8)
                        {
                            emCount = 8;
                        }
                        Thread.Sleep(1000 * (int)Math.Pow(2, emCount));

                        isRunning = false;
                    }
                });
                vThread.IsBackground = true;
                vThread.Start();                
            }
            catch (Exception ex)
            {
                isRunning = false; 
            }

            return true;
            
        }

        public bool updateRegisteList()
        {
            return true;
        }

        /// <summary>
        /// 从HIS更新检查挂号信息
        /// </summary>
        /// <returns></returns>
        public bool updatePhexamList()
        {
            return true; 
        }
        
        /// <summary>
        /// 从HIS更新检验挂号信息
        /// </summary>
        /// <returns></returns>
        public bool updateInspectList()
        {
            return true;
        }

        private bool updatePatientList2()
        {
            try
            {
                string registNo = "";//门急诊/住院就诊号
                string branchNo = "170858507526";
                string StafferNo = "";
                string serviceNo = "";
                string patientId = "";//病人基本信息id
                

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "REGISTRATION_DATE";
                where.operate = "GTE";
                where.relation = "AND";
                where.value = "to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy/MM/dd HH:mm:ss") + "','yyyy-MM-dd hh24:mi:ss')";

                whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("PATIENT_REGISTER", condition);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");

                foreach (XmlElement item in xmlList)
                {
                    registNo = item["VISIT_ID"].InnerText;
                    //branchNo = item["REGISTRATION_DEPT_CODE"].InnerText;
                    //StafferNo = item["REGISTRATION_DOCTOR_CODE"].InnerText;
                    serviceNo = item["REGISTRATION_DEPT_CODE"].InnerText;
                    patientId = item["PATIENT_INFO_ID"].InnerText;

                    insertPatientInfo(patientId);

                    //branchNo = getBranchNoByBranchId(hispatient.BranchId);
                    //StafferNo = getDoctorNoByDoctorName(hispatient.DoctorName);
                    //serviceNo = getServiceNoByCounterNo(branchNo);
                    //addRUserInfo(hispatient.PatId, hispatient.PatName, hispatient.PatAge, hispatient.PatSex, hispatient.PatIdNo, hispatient.PatPhone);

                    //registFlow =registBoss.GetRecordByNo(hispatient.RegId);

                    if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, patientId, branchNo))
                    {
                        registFlow = new RegistFlows();
                        registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                        //registFlow.sRFlowNo = hispatient.RegId;

                        registFlow.iDataFlag = 0;
                        registFlow.sRUserNo = patientId;
                        registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                        registFlow.sDataFrom = item["REGISTRATION_WAY_NAME"].InnerText;
                        registFlow.dRegistDate = DateTime.Parse(item["REGISTRATION_DATE"].InnerText);
                        registFlow.sServiceNo = serviceNo;
                        registFlow.sStafferNo = StafferNo;
                        registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                        registFlow.dStartDate = DateTime.Now;
                        registFlow.dEnditDate = DateTime.Now;
                        registFlow.iRegistState = 0;
                        registFlow.sBranchNo = branchNo;
                        registFlow.sAddOptor = "";
                        registFlow.dAddDate = DateTime.Now;
                        registFlow.sModOptor = "";
                        registFlow.dModDate = DateTime.Now;
                        registFlow.iValidityState = 1;
                        //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                        registFlow.sComments = registNo;
                        registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                        registBoss.AddNewRecord(registFlow); 

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }

        //医院 科室id转换方法 
        private static string getBranchNoByBranchId(string branchId)
        {
            string result = branchId;

            switch (branchId)
            {   
                case "1037":
                case "1007":
                case "1009":
                case "1017":
                case "1018":
                case "1021":
                case "1022":
                case "1036":
                case "2015":
                case "2016":
                case "2006":
                case "2020":
                case "2021":
                case "2004":
                    result = "170858507526";//内科诊区
                    break;
                case "1013":  //内分泌科
                case "2011":  //内分泌科
                    result = "794692459145";
                    break;
                case "2010":
                case "1012": 
                    result = "2010";//口腔科
                    break;
                case "1040":
                case "1041":
                case "1038":
                case "1004":
                case "1005":
                case "1020":
                case "1023":
                case "1027":
                case "2029":
                case "2022":
                case "2031":
                case "2001":
                case "2030":
                case "2002":
                case "2019": 
                    result = "987599611513";  //肿瘤诊区
                    break;
                case "1016":
                case "1032":
                case "1033":
                case "1019":
                case "2014":
                case "2018": 
                    result = "577616678884";//乳腺、头颈外科、儿科诊区
                    break;
                case "1025":
                case "2024":
                    result = "1025";//眼科
                    break;
                case "1006":
                case "1043":
                case "2003":
                    result = "181822232919";//妇产科诊区
                    break;
                case "1015":
                case "1008":
                case "1034":
                case "2013":
                case "2005":
                    result = "788114929783";//外科诊区
                    break;
                case "1014":
                case "1026":
                case "2025":
                case "2012":
                    result = "648155688981";//中医、皮肤科诊区
                    break;
                case "3006":
                case "3001":
                case "3002":
                    result = "362966793176";//医学影像科
                    break;
                case "3007":
                    result = "268451868252";//内镜中心
                    break;
                case "1024":
                    result = "189466453963";//血液净化诊区
                    break;
                //case "1020":
                //case "2219":
                //case "2319":
                //case "2119": 
                //    result = "3005";//功能检查科
                //    break;
                default: break;
            }

            return result;
        }

        private static string getBranchNameByBranchId(string branchId)
        {
            try
            {
                string branchName = "";

                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "DEPT_CODE";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = branchId;

                whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("FZ_DEPT", condition);


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");
                XmlElement item = (XmlElement)xmlList[0];

                if (item != null)
                {
                    branchName = item["DEPT_NAME"].InnerText; 
                }


                return branchName;
            }
            catch (Exception ex)
            { 
                return "";
            }
        }

        private static string doGenerate_RecipeTicketNo(string sCounterNo, string sBranchNo)
        {
            string sTicketNo = "0001";
            DateTime workDate = DateTime.Now;
            string sWhere = "";

            try
            {
                //默认生成票号
                ViewRecipeFlowsBLL vticketBLL = new ViewRecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    sWhere = " BranchNo='" + sBranchNo + "' And CounterNo='" + sCounterNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                }
                else
                {
                    sWhere = " BranchNo='" + sBranchNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                }
                int countNum = vticketBLL.GetCountByCondition(sWhere) + 1;
                sTicketNo = String.Format("{0:D4}", countNum);
            }
            catch (Exception ex)
            {
            }
            return sTicketNo;
        }

        private static string getCounterNoByWindowId(string windowNo, string branchNo)
        {
            int count = 0;
            CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " CounterAlias='" + windowNo + "' And BranchNo='" + branchNo + "' ");
            if (infoColl != null && infoColl.Count > 0)
            {
                return infoColl[0].sCounterNo;
            }
            return "";
        }
        
        #region 消息队列插入数据 

        /// <summary>
        /// 从HIS更新病人信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// 

        private bool insertPatientInfo(string patientId)
        {
            try
            {
                if (IBusinessHelper.findRUserInfo(patientId))
                {
                    return true;
                }

                List<WhereData> whereList = new List<WhereData>();
                WhereData where = new WhereData();
                where.field = "PATIENT_INFO_ID";
                where.operate = "EQ";
                where.relation = "AND";
                where.value = patientId;

                whereList.Add(where);

                string condition = JsonConvert.SerializeObject(whereList);
                condition = condition.Replace("operate", "operator");

                MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
                string result = messageService.getMessage("PATIENT_INFO", condition);


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");
                XmlElement item = (XmlElement)xmlList[0];

                if (item != null)
                {
                    string patId = item["PATIENT_INFO_ID"].InnerText;
                    string patName = item["NAME"].InnerText;
                    string patAge = item["AGE"].InnerText;
                    string patSex = item["SEX_NAME"].InnerText;
                    string patIdNo = item["ID_NO"].InnerText;
                    string patPhone = item["TEL_NO"].InnerText;
                    string patRiNo = item["CARD_NO"].InnerText;

                    IBusinessHelper.addRUserInfo(patId, patName, patAge, patSex, patIdNo, patPhone, patRiNo);
                }


                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        //普通线下挂号信息
        private bool insertPatientList(string xmlSource)
        {
            try
            {

                if (!string.IsNullOrEmpty(xmlSource))
                {
                    string registNo = "";//门急诊/住院就诊号
                    string branchNo = "170858507526";
                    string StafferNo = "";
                    string serviceNo = "";
                    string patientId = "";//病人基本信息id
                    string registDate = "";

                    RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    RegistFlows registFlow = null;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlSource);
                    XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/row");

                    foreach (XmlElement item in xmlList)
                    {
                        registNo = item["VISIT_ID"].InnerText;
                        branchNo = getBranchNoByBranchId(item["REGISTRATION_DEPT_CODE"].InnerText);
                        //StafferNo = item["REGISTRATION_DOCTOR_CODE"].InnerText;
                        serviceNo = IBusinessHelper.getServiceNoByServiceName(getServiceNameByDeptId(item["REGISTRATION_DEPT_CODE"].InnerText));
                        patientId = item["PATIENT_INFO_ID"].InnerText;
                        registDate = item["REGISTRATION_DATE"].InnerText;

                        if (string.IsNullOrEmpty(serviceNo))
                        {
                            continue;
                        }

                        insertPatientInfo(patientId);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, patientId, branchNo))                            
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo =registNo;

                            registFlow.sTicketNo = "";
                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = patientId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            registFlow.sDataFrom = item["REGISTRATION_WAY_NAME"].InnerText; 
                            registFlow.dRegistDate = DateTime.Parse(registDate);
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = StafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = "";
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = registNo;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow); 
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
         
        //普通预约挂号信息
        private bool insertPatientList2(string xmlSource)
        {
            try
            { 
                if (!string.IsNullOrEmpty(xmlSource))
                {
                    string registNo = "";//门急诊/住院就诊号
                    string branchNo = "170858507526";
                    string stafferNo = "";
                    string serviceNo = "";
                    string patientId = "";//病人基本信息id
                    string registDate = "";
                    string dataFrom = "";

                    RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    RegistFlows registFlow = null;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlSource);
                    XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/row");

                    foreach (XmlElement item in xmlList)
                    {
                        registNo = item["VISIT_ID"].InnerText;
                        branchNo = getBranchNoByBranchId(item["REGISTRATION_DEPT_CODE"].InnerText);
                        stafferNo = item["REGISTRATION_DOCTOR_CODE"].InnerText; 
                        patientId = item["PATIENT_INFO_ID"].InnerText;
                        registDate = item["REGISTRATION_DATE"].InnerText;
                        dataFrom= item["REGISTRATION_WAY_NAME"].InnerText;  

                        insertPatientInfo(patientId);

                        if (!hasRegistFlowsByRegistDate(serviceNo, stafferNo, patientId, branchNo, registDate))
                        {  
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();

                            registFlow.sTicketNo = "";
                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = patientId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE2; //预约
                            registFlow.sDataFrom = dataFrom;
                            registFlow.dRegistDate = DateTime.Parse(registDate);
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = stafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = "";
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = registNo;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow);
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        //预约挂号数据
        private bool insertRegisteList(string xmlSource)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlSource))
                {
                    string registNo = "";//门急诊/住院就诊号
                    string branchNo = "170858507526";
                    string stafferNo = "";
                    string serviceNo = "";
                    string patientId = "";//病人基本信息id  
                    string registDate = "";

                    RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    RegistFlows registFlow = null;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlSource);
                    XmlNodeList xmlList = null;
                    if (xmlSource.IndexOf("<body>") > 0)
                    {
                        xmlList=xmlDoc.SelectNodes("/msg/body/row");
                    }
                    else
                    {
                        xmlList = xmlDoc.SelectNodes("/msg/row");
                    }

                    foreach (XmlElement item in xmlList)
                    {
                        registNo = item["visitid"].InnerText;
                        branchNo = getBranchNoByBranchId(item["dept_code"].InnerText);
                        stafferNo = item["doc_code"].InnerText;
                        //serviceNo = IBusinessHelper.getServiceNoByServiceName(getServiceNameByDeptId(item["dept_code"].InnerText));
                        patientId = item["patientid"].InnerText;
                        registDate = item["reg_time"].InnerText; 

                        insertPatientInfo(patientId);

                        if (!hasRegistFlowsByRegistDate(serviceNo, stafferNo, patientId, branchNo, registDate))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = registNo;

                            registFlow.sTicketNo = "";
                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = patientId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE2; //预约
                            registFlow.sDataFrom = "预约挂号2";
                            registFlow.dRegistDate = DateTime.Parse(registDate);
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = stafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = "";
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = registNo;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow); 

                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
         
        private bool insertPhexamList(string xmlSource)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlSource))
                {
                    string registNo = "";//门急诊/住院就诊号
                    string branchNo = "170858507526";
                    string StafferNo = "";
                    string serviceNo = "";
                    string patientId = "";//病人基本信息id 
                    string visitId = "";
                    DateTime inputTime = DateTime.Now;
                    int emergencyFlag = 0;

                    RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    RegistFlows registFlow = null;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlSource);
                    XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/row");

                    foreach (XmlElement item in xmlList)
                    {
                        registNo = item["APPLICATION_NO"].InnerText;
                        visitId = item["VISIT_ID"].InnerText;
                        branchNo = getBranchNoByBranchId(item["EXECUTE_DEPT_CODE"].InnerText);
                        //StafferNo = item["INPUT_DOCTOR_CODE"].InnerText;//开单医生
                        serviceNo = IBusinessHelper.getServiceNoByServiceName(getServiceNameByDeptId(item["EXECUTE_DEPT_CODE"].InnerText));
                        patientId = item["PATIENT_INFO_ID"].InnerText;
                        inputTime = DateTime.Parse(item["INPUT_TIME"].InnerText);
                        emergencyFlag = int.Parse(item["EMERGENCY_FLAG"].InnerText);

                        if (string.IsNullOrEmpty(serviceNo))
                        {
                            continue;
                        }
                        
                        insertPatientInfo(patientId); 

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, patientId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegId;

                            registFlow.sTicketNo = "";
                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = patientId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            //registFlow.sDataFrom = "检查申请";
                            registFlow.sDataFrom = "检查申请-医生:" + item["INPUT_DOCTOR_NAME"].InnerText;
                            registFlow.dRegistDate = DateTime.Parse(item["INPUT_TIME"].InnerText);

                            //if (visitId.StartsWith("ZY") && emergencyFlag == 0)
                            //{
                            //    registFlow.dRegistDate = inputTime.AddDays(1);
                            //    registFlow.sDataFrom = "住院部预约";
                            //}

                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = StafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = "";
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = registNo;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow); 
                        }
                    }

                    return true;
                }

                return false;
            }
            catch(Exception ex)
            { 
                return false;
            }
        }

        private bool insertInspectList(string xmlSource)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlSource))
                {
                    string registNo = "";//门急诊/住院就诊号
                    string branchNo = "170858507526";
                    string StafferNo = "";
                    string serviceNo = "";
                    string patientId = "";//病人基本信息id 

                    RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    RegistFlows registFlow = null;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlSource);
                    XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/row");

                    foreach (XmlElement item in xmlList)
                    {
                        registNo = item["APPLICATION_NO"].InnerText;
                        branchNo = getBranchNoByBranchId(item["EXECUTE_DEPT_CODE"].InnerText);
                        //StafferNo = item["INPUT_DOCTOR_CODE"].InnerText;//开单医生
                        serviceNo = IBusinessHelper.getServiceNoByServiceName(getServiceNameByDeptId(item["EXECUTE_DEPT_CODE"].InnerText));
                        patientId = item["PATIENTID"].InnerText;

                        if (string.IsNullOrEmpty(serviceNo))
                        {
                            continue;
                        }

                        insertPatientInfo(patientId);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, patientId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegId;

                            registFlow.sTicketNo = "";
                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = patientId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            //registFlow.sDataFrom = "检验申请";
                            registFlow.sDataFrom = "检验申请-医生:" + item["INPUT_DOCTOR_NAME"].InnerText;
                            registFlow.dRegistDate = DateTime.Parse(item["INPUT_TIME"].InnerText);    
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = StafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = "";
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = registNo;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow); 
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }

        private bool insertRecipeList(string xmlSource)
        {
            return true; 
        }
         
        private string getServiceNameByDeptId(string deptId)
        {
            List<WhereData> whereList = new List<WhereData>();
            WhereData where = new WhereData();
            where.field = "DEPT_CODE";
            where.operate = "EQ";
            where.relation = "AND";
            where.value = deptId;

            whereList.Add(where);

            string condition = JsonConvert.SerializeObject(whereList);
            condition = condition.Replace("operate", "operator");

            MessagePackService.MessagePackClient messageService = new MessagePackService.MessagePackClient();
            string result = messageService.getMessage("FZ_DEPT", condition);

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                XmlNodeList xmlList = xmlDoc.SelectNodes("/msg/data");
                XmlElement item = (XmlElement)xmlList[0];

                if (item != null)
                {
                    result = item["DEPT_NAME"].InnerText;
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }
         
        private bool hasRecipeFlowsByRegistNo(string counterNo,string patientId, string branchNo)
        { 
            try
            {
                RecipeFlowsBLL infoBoss = new RecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                int count = infoBoss.GetCountByCondition(" ProcessState<3 And  RUserNo='" + patientId + "'  And BranchNo='" + branchNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ");

                return (count > 0 ? true : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        private bool hasRegistFlowsByRegistDate(string serviceNo, string stafferNo, string ruserNo, string branchNo,string registDate)
        {
            try
            {
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                int count = infoBoss.GetCountByCondition(" RegistType=2 And  ServiceNo='" + serviceNo + "' And  StafferNo='" + stafferNo + "' And RUserNo='" + ruserNo + "' And BranchNo='" + branchNo + "' And RegistDate = '" + registDate + "' ");

                return (count > 0 ? true : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}