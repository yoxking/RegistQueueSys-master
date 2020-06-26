using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.HisData;
using EntFrm.DataAdapter.MsSqlDAL;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    /// <summary>
    /// 贵州平塘县人民医院
    /// </summary>
    public class PingtangAdapter : IAdapterBusiness
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

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetConnStr2());
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
                MainFrame.PrintMessage("错误:" + ex.Message);
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
                string branchNo = "";
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfo staffInfo = null;

                HisDoctorDAL hisdoctorDAL = new HisDoctorDAL(IUserContext.GetConnStr2());
                List<HisDoctorInfo> hisdoctorList = hisdoctorDAL.GetAllRecords();

                if (hisdoctorList != null && hisdoctorList.Count > 0)
                {
                    foreach (HisDoctorInfo hisdoctInfo in hisdoctorList)
                    {
                        branchNo = getBranchNoByBranchId(hisdoctInfo.BranchId);
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
                MainFrame.PrintMessage("错误:" + ex.Message);
                return false;
            }
        }

        public bool updatePhexamList()
        {
            return true;
        }

        //处理已就诊记录
        public bool updateInspectList()
        {
            try
            {
                string branchNo = "";
                string stafferNo = ""; 
                int count = 0;

                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ViewTicketFlowsCollections ticketColl = null;
                ProcessFlows processFlow = null;

                string condition = " status=3 And register_time> '" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' "; 

                HisPatientDAL hispatientDAL = new HisPatientDAL(IUserContext.GetConnStr2());
                List<HisPatientInfo> hispatientList = hispatientDAL.GetRecordsByPaging(1, 1000, condition);

                if (hispatientList != null && hispatientList.Count > 0)
                {
                    foreach (HisPatientInfo hispatient in hispatientList)
                    {
                        branchNo = getBranchNoByBranchId(hispatient.BranchId);
                        stafferNo = hispatient.DoctorId;

                        ticketColl = ticketBoss.GetRecordsByPaging(ref count, 1, 1, " ProcessState<30 And  BranchNo='" + branchNo+"' And StafferNo='"+ stafferNo + "' And RUserNo='"+ hispatient.PatId+ "' ");
                        if (ticketColl != null && ticketColl.Count > 0)
                        {
                            processFlow = IPublicHelper.GetProcessFlowByNo(ticketColl.GetFirstOne().sPFlowNo);

                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            processBoss.UpdateRecord(processFlow);

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

        /// <summary>
        /// 从HIS更新病人挂号信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updatePatientList()
        {
            try
            {
                string branchNo = "";
                string stafferNo = "";
                string serviceNo = "";

                RegistFlowsBLL registBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlows registFlow = null;

                string condition = " status=0 And register_time> '" + DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                //string condition = " 登记时间> to_date('" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";

                HisPatientDAL hispatientDAL = new HisPatientDAL(IUserContext.GetConnStr2());
                List<HisPatientInfo> hispatientList = hispatientDAL.GetRecordsByPaging(1, 1000, condition);

                if (hispatientList != null && hispatientList.Count > 0)
                {
                    foreach (HisPatientInfo hispatient in hispatientList)
                    {
                        if (string.IsNullOrEmpty(IPublicHelper.GetStafferNameById(hispatient.DoctorId)))
                        {
                            continue;
                        }

                        //branchNo = hispatient.BranchId;
                        branchNo = getBranchNoByBranchId(hispatient.BranchId);
                        stafferNo = hispatient.DoctorId;
                        //StafferNo = IBusinessHelper.getDoctorNoByDoctorName(hispatient.DoctorName);
                        //serviceNo = IBusinessHelper.getServiceNoByServiceName(hispatient.BranchName);
                        IBusinessHelper.addRUserInfo(hispatient.PatId, hispatient.PatName, hispatient.PatAge, hispatient.PatSex, hispatient.PatIdNo, hispatient.PatPhone);

                        //registFlow =registBoss.GetRecordByNo(hispatient.RegId);

                        if (!IBusinessHelper.hasRegistFlowsByRUserNo(serviceNo, stafferNo, hispatient.PatId, branchNo))
                        {
                            registFlow = new RegistFlows();
                            registFlow.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                            //registFlow.sRFlowNo = hispatient.RegistId;

                            registFlow.iDataFlag = 0;
                            registFlow.sTicketNo = "";
                            registFlow.sRUserNo = hispatient.PatId;
                            registFlow.iRegistType = IPublicConsts.REGISTETYPE1; //挂号
                            registFlow.sDataFrom = "门诊挂号";
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
                return false;
            }
        }


        /// <summary>
        /// 从HIS更新病人预约挂号信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateRegisteList()
        {
            return true;
        }


        //双江人民医院 科室id转换方法
        private string getBranchNoByBranchId(string branchId)
        {
            return "170858507526";
        }

        /// <summary>
        /// 从HIS更新医生排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateStafferRota()
        {
            return true;
        }


        /// <summary>
        /// 从HIS更新服务排班
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool updateServiceRota()
        {
            return true;
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
                string branchNo = "";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counterInfo = null;

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetConnStr2());
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
                MainFrame.PrintMessage("错误:" + ex.Message);
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

                HisBranchDAL hisbranchDAL = new HisBranchDAL(IUserContext.GetConnStr2());
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
                MainFrame.PrintMessage("错误:" + ex.Message);
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