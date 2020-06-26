﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.OracleDAL;
using EntFrm.DataAdapter.HisData;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    /// <summary>
    /// 泸州龙马潭区妇幼保健院
    /// </summary>
    public class LzfuyouAdapter : IAdapterBusiness
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
                List<HisBranchInfo> hisbranchList = hisbranchDAL.GetAllRecords();

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
                string branchNo = "170858507526";
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfo staffInfo = null;

                HisDoctorDAL hisdoctorDAL = new HisDoctorDAL(IUserContext.GetOracleStr());
                List<HisDoctorInfo> hisdoctorList = hisdoctorDAL.GetAllRecords();

                if (hisdoctorList != null && hisdoctorList.Count > 0)
                {
                    foreach (HisDoctorInfo hisdoctInfo in hisdoctorList)
                    {
                        //branchNo = getBranchNoByBranchId(hisdoctInfo.BranchId);
                        staffInfo = staffBoss.GetRecordByNo(hisdoctInfo.DocId);
                        if (staffInfo != null)
                        {
                            staffInfo.sStafferName = hisdoctInfo.DocName;
                            //staffInfo.sLoginId = hisdoctInfo.DocId;
                            //staffInfo.sOrganizNo = hisdoctInfo.BranchId;
                            staffInfo.sRanks = hisdoctInfo.DocResume;
                            staffInfo.sBranchNo = branchNo;
                            staffInfo.dModDate = DateTime.Now;

                            staffBoss.UpdateRecord(staffInfo);
                        }
                        else
                        {
                            staffInfo = new StafferInfo();
                            staffInfo.sStafferNo = hisdoctInfo.DocId;
                            staffInfo.sStafferName = hisdoctInfo.DocName;
                            staffInfo.sLoginId = hisdoctInfo.DocId;
                            staffInfo.sPassword = hisdoctInfo.DocId;
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
                MainFrame.PrintMessage(ex.Message);
                return false;
            }
        }

        public bool updatePhexamList()
        {
            try
            {
                string branchNo = "389185933554";
                string stafferNo = "";
                string serviceNo = "";

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                //string condition = " check_time> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
                string condition = " check_time> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisPhexamDAL hisphexamDAL = new HisPhexamDAL(IUserContext.GetOracleStr());
                List<HisPhexamInfo> hisphexamList = hisphexamDAL.GetRecordsByPaging(1, 1000, condition);

                if (hisphexamList != null && hisphexamList.Count > 0)
                {
                    foreach (HisPhexamInfo hisphexam in hisphexamList)
                    {
                        //branchNo = getBranchNoByBranchId(hisphexam.BranchId);
                        serviceNo = getServiceNoByServiceName(hisphexam.BranchName);
                        IBusinessHelper.addRUserInfo(hisphexam.PatId, hisphexam.PatName, hisphexam.PatAge, hisphexam.PatSex, hisphexam.PatIdNo, hisphexam.PatPhone);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, stafferNo, hisphexam.PatId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            //registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            registFlow.sRFlowNo = hisphexam.RegistId;

                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = hisphexam.PatId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            registFlow.sDataFrom = hisphexam.PatFrom;
                            registFlow.dRegistDate = DateTime.Parse(hisphexam.RegistTime);
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
                            //registFlow.sComments = hisphexam.PatFrom + "," + hisphexam.BranchName + "," + hisphexam.PexamId + "," + hisphexam.PatId + "," + hisphexam.ImgType + "," + hisphexam.UserName;
                            registFlow.sComments = hisphexam.RegistId;
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
                MyFileHelper.WriteLog(ex.Message);
                return false;
            }
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
                string branchNo = "170858507526";
                string stafferNo = "";
                string serviceNo = "";

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                string condition = " update_time> to_date('" + DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
                //string condition = " 登记时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisPatientDAL hispatientDAL = new HisPatientDAL(IUserContext.GetOracleStr());
                List<HisPatientInfo> hispatientList = hispatientDAL.GetRecordsByPaging(1, 1000, condition);

                if (hispatientList != null && hispatientList.Count > 0)
                {
                    foreach (HisPatientInfo hispatient in hispatientList)
                    {
                        //branchNo = getBranchNoByBranchId(hispatient.BranchId); 
                        //StafferNo= IBusinessHelper.getDoctorNoByDoctorName(hispatient.DoctorName);
                        stafferNo = hispatient.DoctorId;
                        //serviceNo = IBusinessHelper.getServiceNoByCounterNo(branchNo);
                        IBusinessHelper.addRUserInfo(hispatient.PatId, hispatient.PatName, hispatient.PatAge, hispatient.PatSex, hispatient.PatIdNo, hispatient.PatPhone);

                        //registFlow =registBoss.GetRecordByNo(hispatient.RegId);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, stafferNo, hispatient.PatId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegistId;

                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = hispatient.TicketId;
                            registFlow.sRUserNo = hispatient.PatId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            registFlow.sDataFrom = "现场挂号";
                            registFlow.dRegistDate = DateTime.Parse(hispatient.RegistTime);
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
                //MainFrame.PrintMessage(ex.Message);
                return false;
            }
        }

        public bool updateRegisteList()
        {
            return true;
        }

        //妇幼保健医院 科室id转换方法 
        private string getBranchNoByBranchId(string branchId)
        {
            string result = "170858507526";

            if ("1061".Contains(branchId))
            {
                result = "170858507526";//儿科
            }
            //else if ("42".Contains(branchId))
            //{
            //    result = "989634661495";//产科
            //}
            //else if ("801".Contains(branchId))
            //{
            //    result = "888586337378";//妇科
            //}

            return result;
        }


        private string getServiceNoByServiceName(string serviceName)
        {
            try
            {
                string result = "";

                int count = 0;
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "ServiceAlias = '"+ serviceName + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sServiceNo;
                }
                return result;

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
                string branchNo = "170858507526";
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
                string branchNo = "170858507526";
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
                string branchNo = "170858507526";
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
                string branchNo = "170858507526";
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

                    return true;
                }

                return false;
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
            return true;
        }

        /// <summary>
        /// 从HIS更新取药业务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRecipeDetail()
        {
            return true;
        }
    }
}