using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.DataAdapter.OracleDAL;
using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    /// <summary>
    /// 重庆璧山妇幼保健院
    /// </summary>
    public class BsfuyouAdapter: IAdapterBusiness
    {


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

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetOracleStr());
                List<HisBranchInfo> hisbranchList = hisbranchDAL.GetRecordsByPaging(1, 1000, "");
                //List<HisBranchInfo> hisbranchList = hisbranchDAL.GetAllRecords();

                if (hisbranchList != null && hisbranchList.Count > 0)
                {
                    foreach (HisBranchInfo hisbranchInfo in hisbranchList)
                    {
                        branchInfo = branchBoss.GetRecordByNo(hisbranchInfo.BranchId);
                        if (branchInfo != null)
                        {
                            //branchInfo.sBranchName = hisbranchInfo.BranchName;
                            //branchInfo.iBranchType = IPublicConsts.BRANCHTYPE1;
                            //branchInfo.sBranchCode = hisbranchInfo.ParentNo;
                            //branchInfo.sSummary = hisbranchInfo.ParentName;
                            //branchInfo.dModDate = DateTime.Now;

                            //branchBoss.UpdateRecord(branchInfo);
                        }
                        else
                        {

                            branchInfo = new BranchInfo();
                            branchInfo.sBranchNo = hisbranchInfo.BranchId;
                            branchInfo.sBranchName = hisbranchInfo.BranchName;
                            branchInfo.iBranchType = IPublicConsts.BRANCHTYPE1;
                            branchInfo.sBranchCode = hisbranchInfo.ParentId;
                            branchInfo.sContacts = "";
                            branchInfo.sTelphone = "";
                            branchInfo.sSummary = hisbranchInfo.ParentName;
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

                return false;
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
                string branchNo = "428";
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfo staffInfo = null;

                HisDoctorDAL hisdoctorDAL = new HisDoctorDAL(IUserContext.GetOracleStr());
                List<HisDoctorInfo> hisdoctorList = hisdoctorDAL.GetAllRecords();

                if (hisdoctorList != null && hisdoctorList.Count > 0)
                {
                    foreach (HisDoctorInfo hisdoctInfo in hisdoctorList)
                    {
                        branchNo= getBranchNoByBranchId(hisdoctInfo.BranchId);
                        staffInfo = staffBoss.GetRecordByNo(hisdoctInfo.DocId);
                        if (staffInfo != null)
                        {
                            //staffInfo.sStafferName = hisdoctInfo.DocName;
                            ////staffInfo.sLoginId = hisdoctInfo.DocId;
                            ////staffInfo.sOrganizNo = hisdoctInfo.BranchId;
                            //staffInfo.sRanks = hisdoctInfo.DocResume;
                            //staffInfo.sBranchNo = branchNo;
                            //staffInfo.dModDate = DateTime.Now;

                            //staffBoss.UpdateRecord(staffInfo);
                        }
                        else
                        {
                            staffInfo = new StafferInfo();
                            staffInfo.sStafferNo = hisdoctInfo.DocId;
                            staffInfo.sStafferName = hisdoctInfo.DocName;
                            staffInfo.sLoginId = hisdoctInfo.DocPhoto;
                            staffInfo.sPassword = hisdoctInfo.DocPhoto;
                            staffInfo.sCounterNo = "";
                            staffInfo.sOrganizNo = hisdoctInfo.BranchId;
                            staffInfo.sOrganizName = hisdoctInfo.BranchId;
                            staffInfo.sStarLevel = "五星";
                            staffInfo.sHeadPhoto = "";
                            staffInfo.sSummary = hisdoctInfo.DocResume;
                            staffInfo.sBranchNo = branchNo;
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
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updatePhexamList()
        { 
            return true;
        }

        public bool updateInspectList()
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
                string branchNo = "231";
                string StafferNo = "";
                string serviceNo = ""; 

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                string condition = " update_time> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')  ";
                //string condition = " 登记时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisPatientDAL hispatientDAL = new HisPatientDAL(IUserContext.GetOracleStr());
                List<HisPatientInfo> hispatientList = hispatientDAL.GetRecordsByPaging(1, 1000, condition);

                if (hispatientList != null && hispatientList.Count > 0)
                {
                    foreach (HisPatientInfo hispatient in hispatientList)
                    {
                        //branchNo = hispatient.BranchId;
                        branchNo = getBranchNoByBranchId(hispatient.BranchId);
                        StafferNo = IBusinessHelper.getDoctorNoByDoctorName(hispatient.DoctorName);
                        serviceNo = IBusinessHelper.getServiceNoByCounterNo(branchNo);
                        IBusinessHelper.addRUserInfo(hispatient.PatId, hispatient.PatName, hispatient.PatAge, hispatient.PatSex, hispatient.PatIdNo, hispatient.PatPhone);
                         
                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, hispatient.PatId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegistId;

                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = hispatient.TicketId;
                            registFlow.sRUserNo = hispatient.PatId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            registFlow.sDataFrom = "挂号";
                            registFlow.dRegistDate = DateTime.Parse(hispatient.RegistTime);
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = StafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = hispatient.TicketId;
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = hispatient.RegistId;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow); 
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }

        public bool updateRegisteList()
        {
            try
            {
                string branchNo = "231";
                string StafferNo = "";
                string serviceNo = "";

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                string condition = " register_time> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')  ";
                //string condition = " 登记时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisRegisteDAL hispatientDAL = new HisRegisteDAL(IUserContext.GetOracleStr());
                List<HisPatientInfo> hispatientList = hispatientDAL.GetRecordsByPaging(1, 1000, condition);

                if (hispatientList != null && hispatientList.Count > 0)
                {
                    foreach (HisPatientInfo hispatient in hispatientList)
                    {
                        //branchNo = hispatient.BranchId;
                        branchNo = getBranchNoByBranchId(hispatient.BranchId);
                        StafferNo = IBusinessHelper.getDoctorNoByDoctorName(hispatient.DoctorName);
                        serviceNo = IBusinessHelper.getServiceNoByCounterNo(branchNo);
                        IBusinessHelper.addRUserInfo(hispatient.PatId, hispatient.PatName, hispatient.PatAge, hispatient.PatSex, hispatient.PatIdNo, hispatient.PatPhone);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, StafferNo, hispatient.PatId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegistId;

                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = hispatient.TicketId;
                            registFlow.sRUserNo = hispatient.PatId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE2; //预约
                            registFlow.sDataFrom = "在线预约"; 
                            registFlow.dRegistDate = DateTime.Parse(hispatient.RegistTime);
                            registFlow.sServiceNo = serviceNo;
                            registFlow.sStafferNo = StafferNo;
                            registFlow.iWorkTime = IPublicConsts.WORKTIMETYPE2;//全天
                            registFlow.dStartDate = DateTime.Now;
                            registFlow.dEnditDate = DateTime.Now;
                            registFlow.iRegistState = 0;
                            registFlow.sBranchNo = branchNo;
                            registFlow.sAddOptor = hispatient.TicketId;
                            registFlow.dAddDate = DateTime.Now;
                            registFlow.sModOptor = "";
                            registFlow.dModDate = DateTime.Now;
                            registFlow.iValidityState = 1;
                            //registFlow.sComments = hispatient.RegNo + "," + hispatient.RegId + "," + hispatient.PatId + "," + hispatient.PatType;
                            registFlow.sComments = hispatient.RegistId;
                            registFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            registBoss.AddNewRecord(registFlow);
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }

        //永川人民医院 科室id转换方法
        private string getBranchNoByBranchId(string branchId)
        {
            string result = branchId;

            switch (branchId)
            {
                case "2225": 
                case "2705":
                    result = "2225";
                    break;
                case "2224":
                    result = "2224";
                    break;
                case "231":
                case "824":
                    result = "231";
                    break;
                default: break;
            }

            return result;
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
                string branchNo = "428";
                StafferRotaBLL srotaBoss = new StafferRotaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferRota staffRota = null;

                HisDocRotaDAL hisdocrotaDAL = new HisDocRotaDAL(IUserContext.GetOracleStr());

                List<HisDoctorRota> hisdoctRotas = hisdocrotaDAL.GetRecordsByPaging(1, 1000, "");

                if (hisdoctRotas != null && hisdoctRotas.Count > 0)
                {
                    foreach (HisDoctorRota hisrota in hisdoctRotas)
                    {
                        staffRota = IBusinessHelper.getStafferRotaByStafferNo(hisrota.DoctorId);
                        if (staffRota != null)
                        {
                            staffRota.sStafferNo = hisrota.DoctorId;
                            staffRota.iWeekDay1 = IPublicHelper.getWorkTimeType(hisrota.WeekDay1);
                            staffRota.iWeekDay2 = IPublicHelper.getWorkTimeType(hisrota.WeekDay2);
                            staffRota.iWeekDay3 = IPublicHelper.getWorkTimeType(hisrota.WeekDay3);
                            staffRota.iWeekDay4 = IPublicHelper.getWorkTimeType(hisrota.WeekDay4);
                            staffRota.iWeekDay5 = IPublicHelper.getWorkTimeType(hisrota.WeekDay5);
                            staffRota.iWeekDay6 = IPublicHelper.getWorkTimeType(hisrota.WeekDay6);
                            staffRota.iWeekDay7 = IPublicHelper.getWorkTimeType(hisrota.WeekDay7);
                            staffRota.sBranchNo = "";
                            staffRota.dModDate = DateTime.Now;

                            srotaBoss.UpdateRecord(staffRota);
                        }
                        else
                        {
                            staffRota = new StafferRota();
                            staffRota.sRotaNo = CommonHelper.Get_New12ByteGuid();
                            staffRota.sStafferNo = hisrota.DoctorId;
                            staffRota.iRotaType = IPublicConsts.ROTATYPE1;//正常排班
                            staffRota.dStartDate = DateTime.Now;
                            staffRota.dEnditDate = DateTime.Now;
                            staffRota.iWeekDay1 = IPublicHelper.getWorkTimeType(hisrota.WeekDay1);
                            staffRota.iWeekDay2 = IPublicHelper.getWorkTimeType(hisrota.WeekDay2);
                            staffRota.iWeekDay3 = IPublicHelper.getWorkTimeType(hisrota.WeekDay3);
                            staffRota.iWeekDay4 = IPublicHelper.getWorkTimeType(hisrota.WeekDay4);
                            staffRota.iWeekDay5 = IPublicHelper.getWorkTimeType(hisrota.WeekDay5);
                            staffRota.iWeekDay6 = IPublicHelper.getWorkTimeType(hisrota.WeekDay6);
                            staffRota.iWeekDay7 = IPublicHelper.getWorkTimeType(hisrota.WeekDay7);
                            staffRota.sRotaFormat = "";
                            staffRota.sBranchNo = branchNo;
                            staffRota.sAddOptor = "";
                            staffRota.dAddDate = DateTime.Now;
                            staffRota.sModOptor = "";
                            staffRota.dModDate = DateTime.Now;
                            staffRota.iValidityState = 1;
                            staffRota.sComments = "";
                            staffRota.sAppCode = IUserContext.GetAppCode();

                            srotaBoss.AddNewRecord(staffRota);
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


        /// <summary>
        /// 从HIS更新服务排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateServiceRota()
        {
            try
            {
                string branchNo = "428";
                ServiceRotaBLL srotaBoss = new ServiceRotaBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceRota serviceRota = null;

                HisDocRotaDAL hisdocrotaDAL = new HisDocRotaDAL(IUserContext.GetOracleStr());

                List<HisDoctorRota> hisdoctRotas = hisdocrotaDAL.GetRecordsByPaging(1, 1000, "");

                if (hisdoctRotas != null && hisdoctRotas.Count > 0)
                {
                    foreach (HisDoctorRota hisrota in hisdoctRotas)
                    {
                        serviceRota = IBusinessHelper.getServiceRotaByServiceNo(hisrota.DoctorId);
                        if (serviceRota != null)
                        {
                            serviceRota.sServiceNo = hisrota.CounterId;
                            serviceRota.iWeekDay1 = IPublicHelper.getWorkTimeType(hisrota.WeekDay1);
                            serviceRota.iWeekDay2 = IPublicHelper.getWorkTimeType(hisrota.WeekDay2);
                            serviceRota.iWeekDay3 = IPublicHelper.getWorkTimeType(hisrota.WeekDay3);
                            serviceRota.iWeekDay4 = IPublicHelper.getWorkTimeType(hisrota.WeekDay4);
                            serviceRota.iWeekDay5 = IPublicHelper.getWorkTimeType(hisrota.WeekDay5);
                            serviceRota.iWeekDay6 = IPublicHelper.getWorkTimeType(hisrota.WeekDay6);
                            serviceRota.iWeekDay7 = IPublicHelper.getWorkTimeType(hisrota.WeekDay7);
                            serviceRota.sBranchNo = "";
                            serviceRota.dModDate = DateTime.Now;

                            srotaBoss.UpdateRecord(serviceRota);
                        }
                        else
                        {
                            serviceRota = new ServiceRota();
                            serviceRota.sRotaNo = CommonHelper.Get_New12ByteGuid();
                            serviceRota.sServiceNo = hisrota.CounterId;
                            serviceRota.iRotaType = IPublicConsts.ROTATYPE1;//正常排班
                            serviceRota.dStartDate = DateTime.Now;
                            serviceRota.dEnditDate = DateTime.Now;
                            serviceRota.iWeekDay1 = IPublicHelper.getWorkTimeType(hisrota.WeekDay1);
                            serviceRota.iWeekDay2 = IPublicHelper.getWorkTimeType(hisrota.WeekDay2);
                            serviceRota.iWeekDay3 = IPublicHelper.getWorkTimeType(hisrota.WeekDay3);
                            serviceRota.iWeekDay4 = IPublicHelper.getWorkTimeType(hisrota.WeekDay4);
                            serviceRota.iWeekDay5 = IPublicHelper.getWorkTimeType(hisrota.WeekDay5);
                            serviceRota.iWeekDay6 = IPublicHelper.getWorkTimeType(hisrota.WeekDay6);
                            serviceRota.iWeekDay7 = IPublicHelper.getWorkTimeType(hisrota.WeekDay7);
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

                        return true;
                    }
                }
                return false;
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
                string branchNo = "428";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counterInfo = null;

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetOracleStr());
                List<HisBranchInfo> hisbranchList = hisbranchDAL.GetRecordsByPaging(1, 1000, "");

                if (hisbranchList != null && hisbranchList.Count > 0)
                {
                    foreach (HisBranchInfo hisbranchInfo in hisbranchList)
                    {
                        counterInfo = counterBoss.GetRecordByNo(hisbranchInfo.BranchId);
                        if (counterInfo != null)
                        {

                            counterInfo.sCounterName = hisbranchInfo.BranchName; 
                            counterInfo.dModDate = DateTime.Now;

                            counterBoss.UpdateRecord(counterInfo);
                        }
                        else
                        {

                            counterInfo = new CounterInfo();
                            counterInfo.sCounterNo = hisbranchInfo.BranchId;
                            counterInfo.sCounterName = hisbranchInfo.BranchName;
                            counterInfo.sCounterAlias = hisbranchInfo.BranchName;
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

                return false;
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
                string branchNo = "428";
                ServiceInfoBLL serviceBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfo serviceInfo = null;

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetOracleStr());
                List<HisBranchInfo> hisbranchList = hisbranchDAL.GetRecordsByPaging(1, 1000, "");

                if (hisbranchList != null && hisbranchList.Count > 0)
                {
                    foreach (HisBranchInfo hisbranchInfo in hisbranchList)
                    {
                        serviceInfo = serviceBoss.GetRecordByNo(hisbranchInfo.BranchId);
                        if (serviceInfo != null)
                        {
                            serviceInfo.sServiceName = hisbranchInfo.BranchName;
                            serviceInfo.dModDate = DateTime.Now;

                            serviceBoss.UpdateRecord(serviceInfo);
                        }
                        else
                        {
                            serviceInfo = new ServiceInfo();
                            serviceInfo.sServiceNo = hisbranchInfo.BranchId; ;
                            serviceInfo.sServiceName = hisbranchInfo.BranchName;
                            serviceInfo.sServiceAlias = hisbranchInfo.BranchName;
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
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateOperateList()
        {
            return true;
        }

        /// <summary>
        /// 从HIS更新取药业务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRecipeList()
        {
            try
            {
                string branchNo = "429"; 
                string registeMode = "MANUAL";
                int state = 0;

                RecipeFlowsBLL recipeBoss = new RecipeFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RecipeFlows recipeFlow = null;

                string condition = " update_time> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
                //string condition = " 发送时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisRecipeInfoDAL hisrecipeDAL = new HisRecipeInfoDAL(IUserContext.GetOracleStr());
                List<HisRecipeInfo> hisrecipeList = hisrecipeDAL.GetRecordsByPaging(1, 1000, condition);

                if (hisrecipeList != null && hisrecipeList.Count > 0)
                {
                    foreach (HisRecipeInfo hisrecipe in hisrecipeList)
                    { 
                        IBusinessHelper.addRUserInfo(hisrecipe.PatId, hisrecipe.PatName, hisrecipe.PatAge, hisrecipe.PatSex, hisrecipe.PatIdNo, hisrecipe.PatPhone);

                        if (!IBusinessHelper.hasRecipeFlowsByRegistNo(hisrecipe.Guid, hisrecipe.PatId))
                        {
                            state = 0;
                            registeMode = IUserContext.GetParamValue(branchNo, IPublicConsts.DEF_REGISTEMODE, "Others");
                            //自动报到模式
                            if (registeMode.Equals("AUTO"))
                            {
                                state = 1;
                            }

                            recipeFlow = new RecipeFlows();
                            recipeFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();

                            recipeFlow.iDataFlag = 0;
                            //recipeFlow.sRegistNo = hisrecipe.Guid;
                            recipeFlow.sRegistNo = hisrecipe.RegistId;
                            recipeFlow.sTicketNo = "";
                            recipeFlow.sRUserNo = hisrecipe.PatId;
                            recipeFlow.sCounterNo = hisrecipe.CounterId;
                            recipeFlow.dEnqueueTime = DateTime.Now;
                            recipeFlow.dBeginTime = DateTime.Now;
                            recipeFlow.dFinishTime = DateTime.Now;
                            recipeFlow.iRecipeState = state;
                            recipeFlow.sRecipeOpter = "";
                            recipeFlow.dRecipeDate = DateTime.Now;
                            recipeFlow.iProcessState = state;
                            recipeFlow.dProcessedTime = DateTime.Now;
                            recipeFlow.sPrcsCounterNo = "";
                            recipeFlow.sDataFrom = hisrecipe.BranchName;
                            recipeFlow.sBranchNo =branchNo;

                            recipeFlow.sAddOptor = "";
                            recipeFlow.dAddDate = DateTime.Now;
                            recipeFlow.sModOptor = "";
                            recipeFlow.dModDate = DateTime.Now;
                            recipeFlow.iValidityState = 1;
                            //registFlow.sComments = hisphexam.PatFrom + "," + hisphexam.BranchName + "," + hisphexam.PexamId + "," + hisphexam.PatId + "," + hisphexam.ImgType + "," + hisphexam.UserName;
                            recipeFlow.sComments = hisrecipe.Guid;
                            //recipeFlow.sComments = hisrecipe.HospitId;
                            recipeFlow.sAppCode = IUserContext.GetAppCode() + ";";

                            if (recipeBoss.AddNewRecord(recipeFlow))
                            {
                                insertRecipeDetail(recipeFlow.sRFlowNo,hisrecipe.Guid, hisrecipe.PatId);
                            }

                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }

        private bool insertRecipeDetail(string rflowNo,string guid,string patId)
        {
            try
            {
                RecipeDetailsBLL recipeBoss = new RecipeDetailsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RecipeDetails recipeDetail = null;

                 string condition = " no='"+ guid + "' And  patient_id='" + patId + "' ";

                HisRecipeDetailDAL hisrecipeDAL = new HisRecipeDetailDAL(IUserContext.GetOracleStr());
                List<HisRecipeDetail> hisrecipeList = hisrecipeDAL.GetRecordsByPaging(1, 100, condition);

                if (hisrecipeList != null && hisrecipeList.Count > 0)
                {
                    foreach (HisRecipeDetail hisrecipe in hisrecipeList)
                    {
                        if (!IBusinessHelper.hasRecipeDetailByRecipeName(hisrecipe.PatId, hisrecipe.RecipeName))
                        {

                            recipeDetail = new RecipeDetails();
                            recipeDetail.sRecipeNo = CommonHelper.Get_New12ByteGuid();

                            recipeDetail.sRFlowNo = rflowNo;
                            recipeDetail.sRUserNo = hisrecipe.PatId;
                            recipeDetail.sRecipeName = hisrecipe.RecipeName;
                            recipeDetail.sRecipeSpec = hisrecipe.RecipeSpec;
                            recipeDetail.sSeriNumber = hisrecipe.SeriNumber;
                            recipeDetail.sStandard = hisrecipe.Standard;
                            recipeDetail.dPrice = hisrecipe.Price;
                            recipeDetail.dAmount = hisrecipe.Amount;
                            recipeDetail.sSQuantity = hisrecipe.SQuantity;
                            recipeDetail.sTQuantity = hisrecipe.TQuantity;
                            recipeDetail.sDirection = hisrecipe.Direction;
                            recipeDetail.dExpiryDate = DateTime.Now;
                            recipeDetail.sFrequency = hisrecipe.Frequency;

                            recipeDetail.sAddOptor = "";
                            recipeDetail.dAddDate = DateTime.Now;
                            recipeDetail.sModOptor = "";
                            recipeDetail.dModDate = DateTime.Now;
                            recipeDetail.iValidityState = 1;
                            recipeDetail.sComments = "";
                            recipeDetail.sAppCode = IUserContext.GetAppCode() + ";";

                            recipeBoss.AddNewRecord(recipeDetail);

                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }
         

        /// <summary>
        /// 从HIS更新取药业务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRecipeDetail()
        {
            //try
            //{
            //    RecipeDetailsBLL recipeBoss = new RecipeDetailsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            //    RecipeDetails recipeDetail = null;

            //    string condition = "";
            //    //string condition = " 日期> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            //    //string condition = " 发送时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

            //    HisRecipeDetailDAL hisrecipeDAL = new HisRecipeDetailDAL(IUserContext.GetOracleStr());
            //    List<HisRecipeDetail> hisrecipeList = hisrecipeDAL.GetRecordsByPaging(1, 10, condition);

            //    if (hisrecipeList != null && hisrecipeList.Count > 0)
            //    {
            //        foreach (HisRecipeDetail hisrecipe in hisrecipeList)
            //        {
            //            if (!IBusinessHelper.hasRecipeDetailByRecipeName(hisrecipe.RegistId, hisrecipe.RecipeName))
            //            {

            //                recipeDetail = new RecipeDetails();
            //                recipeDetail.sRecipeNo = CommonHelper.Get_New12ByteGuid();

            //                recipeDetail.sRFlowNo = hisrecipe.HospitId;
            //                recipeDetail.sRecipeName = hisrecipe.RecipeName;
            //                recipeDetail.sRecipeSpec = hisrecipe.RecipeSpec;
            //                recipeDetail.sSeriNumber = hisrecipe.SeriNumber;
            //                recipeDetail.sStandard = hisrecipe.Standard;
            //                recipeDetail.dPrice = Math.Round(hisrecipe.Price, 2);
            //                recipeDetail.dAmount = Math.Round(hisrecipe.Amount, 2);
            //                recipeDetail.sSQuantity = hisrecipe.SQuantity;
            //                recipeDetail.sTQuantity = hisrecipe.TQuantity;
            //                recipeDetail.sDirection = hisrecipe.Direction;
            //                recipeDetail.dExpiryDate = DateTime.Now;
            //                recipeDetail.sFrequency = hisrecipe.Frequency;

            //                recipeDetail.sAddOptor = "";
            //                recipeDetail.dAddDate = DateTime.Now;
            //                recipeDetail.sModOptor = "";
            //                recipeDetail.dModDate = DateTime.Now;
            //                recipeDetail.iValidityState = 1;
            //                recipeDetail.sComments = "";
            //                recipeDetail.sAppCode = IUserContext.GetAppCode() + ";";

            //                recipeBoss.AddNewRecord(recipeDetail);

            //            }
            //        }
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //MainFrame.PrintMessage("错误:" + ex.Message);
            //    return false;
            //} 
            return true;
        }
    }
}