using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EntWeb.BkConsole
{
    public class PublicHelper
    {
        public static string Get_AppCode()
        {
            return GetConfigValue("AppCode");
        }

        public static string Get_ConnStr()
        {
            string connStr = ConfigurationManager.AppSettings["SqlServer"].ToString();

            return EnconfigHelper.Decrypt(connStr);
            //return connStr;
        }

        public static string Get_BranchNo()
        {
            return GetConfigValue("BranchNo");
        }

        public static string GetConfigValue(string Name)
        {
            try
            {
                return ConfigurationManager.AppSettings[Name].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void SetConfigValue(string Name, string Value)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                var appSetting = (AppSettingsSection)config.GetSection("appSettings");
                if (appSetting.Settings[Name] == null) //如果不存在此节点，则添加   
                {
                    appSetting.Settings.Add(Name, Value);
                }
                else //如果存在此节点，则修改   
                {
                    appSetting.Settings[Name].Value = Value;
                }
                config.Save(ConfigurationSaveMode.Modified);
                config = null;
            }
            catch (Exception ex) { }
        }


        //定义业务、窗口、员工的状态，有更新:11111,无更新:00000
        public static bool SetStateValue1(string sBranchNo, string sName, string sValue, int index = -1)
        {
            if (index < 0)
            {
                return SetParamValue(sBranchNo, sName, sValue, "State");
            }
            else
            {
                sValue = sValue.Substring(0, 1);
                string temp = GetParamValue(sBranchNo, sName, "State");
                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Remove(index, 1);
                    temp = temp.Insert(index, sValue);

                    return SetParamValue(sBranchNo, sName, temp, "State");
                }
                else
                {
                    if (sValue.Equals("0"))
                    {
                        return SetParamValue(sBranchNo, sName, "0000000000", "State");
                    }
                    else
                    {
                        return SetParamValue(sBranchNo, sName, "1111111111", "State");
                    }
                }
            }
        }

        public static string GetStateValue1(string sBranchNo, string sName, int index = -1)
        {
            string result = "";
            if (index < 0)
            {
                result = GetParamValue(sBranchNo,sName, "State");
            }
            else
            {
                string temp = GetParamValue(sBranchNo,sName, "State");
                if (!string.IsNullOrEmpty(temp) && index < temp.Length)
                {
                    result = temp[index].ToString();
                }
            }

            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        public static bool SetParamValue(string sBranchNo, string sName, string sValue, string sType = "2")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 0;
                    SysParams info = null;
                    SysParamsBLL infoBLL = new SysParamsBLL(Get_ConnStr(), Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sKeyValue = sValue;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;

                        return infoBLL.UpdateRecord(info);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sName;
                        info.sKeyValue = sValue;
                        info.sKeyType = sType;
                        info.sBranchNo = sBranchNo;

                        info.sAddOptor = sSuNo;
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = sSuNo;
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";

                        info.sAppCode = Get_AppCode() + ";";

                        return infoBLL.AddNewRecord(info);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public static string GetParamValue(string sBranchNo, string sName, string sType = "2")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;
                    SysParamsBLL infoBLL = new SysParamsBLL(Get_ConnStr(), Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ");
                    if (infoColl != null && infoColl.Count > 0)
                    {
                        return infoColl.GetFirstOne().sKeyValue;
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return "";
        }
        public static string GetRegisteType(int registeType)
        {
            string sResult = "";
            switch (registeType)
            {
                case PublicConsts.REGISTETYPE1:
                    sResult = "现场挂号";
                    break;
                case PublicConsts.REGISTETYPE2:
                    sResult = "预约挂号";
                    break;
                default:
                    break;
            }

            return sResult;
        }
        public static string GetWorkTimeType(int workTime)
        {
            string sResult = "";
            switch (workTime)
            {
                case PublicConsts.WORKTIMETYPE1:
                    sResult = "休息";
                    break;
                case PublicConsts.WORKTIMETYPE2:
                    sResult = "全天";
                    break;
                case PublicConsts.WORKTIMETYPE3:
                    sResult = "上午";
                    break;
                case PublicConsts.WORKTIMETYPE4:
                    sResult = "下午";
                    break;
                default: break;
            }
            return sResult;
        }

        public static int GetWorkTimeType(string cnType)
        {
            int iresult = 0;
            switch (cnType)
            {
                case "休息": iresult = PublicConsts.WORKTIMETYPE1; break;
                case "全日": iresult = PublicConsts.WORKTIMETYPE2; break;
                case "上午": iresult = PublicConsts.WORKTIMETYPE3; break;
                case "下午": iresult = PublicConsts.WORKTIMETYPE4; break;
                default: break;
            }
            return iresult;
        }
        public static string GetRotaType(int rotaType)
        {
            string sResult = "";
            switch (rotaType)
            {
                case PublicConsts.ROTATYPE1:
                    sResult = "正常排班";
                    break;
                case PublicConsts.ROTATYPE2:
                    sResult = "临时排班";
                    break;
                default:
                    break;
            }

            return sResult;
        }
         
        public static string GetBranchType(int branchType)
        {
            string sResult = "";
            switch (branchType)
            {
                case PublicConsts.BRANCHTYPE1:
                    sResult = "门诊";
                    break;
                case PublicConsts.BRANCHTYPE2:
                    sResult = "检验";
                    break;
                case PublicConsts.BRANCHTYPE3:
                    sResult = "检查";
                    break;
                case PublicConsts.BRANCHTYPE4:
                    sResult = "药房";
                    break;
                default:
                    break;
            }

            return sResult;
        } 

        public static string GetPriorityType(int priorityType)
        {
            string sResult = "普通";
            switch (priorityType)
            {
                case PublicConsts.PRIORITY_TYPE1:
                    sResult = "预";
                    break;
                case PublicConsts.PRIORITY_TYPE2:
                    sResult = "过号";
                    break;
                case PublicConsts.PRIORITY_TYPE3:
                    sResult = "军人";
                    break;
                case PublicConsts.PRIORITY_TYPE4:
                    sResult = "离休";
                    break;
                case PublicConsts.PRIORITY_TYPE5:
                    sResult = "幼儿";
                    break;
                case PublicConsts.PRIORITY_TYPE6:
                    sResult = "老人";
                    break;
                case PublicConsts.PRIORITY_TYPE7:
                    sResult = "急诊";
                    break;
                default:
                    break;
            }

            return sResult;
        }


        public static string GetOperateState(int state)
        {
            string sResult = "手术中";
            switch (state)
            {
                case PublicConsts.OPERATE_STATE1:
                    sResult = "准备手术";
                    break;
                case PublicConsts.OPERATE_STATE2:
                    sResult = "入诱导室";
                    break;
                case PublicConsts.OPERATE_STATE3:
                    sResult = "出诱导室";
                    break;
                case PublicConsts.OPERATE_STATE4:
                    sResult = "入手术室";
                    break;
                case PublicConsts.OPERATE_STATE5:
                    sResult = "麻醉开始";
                    break;
                case PublicConsts.OPERATE_STATE6:
                    sResult = "手术开始";
                    break;
                case PublicConsts.OPERATE_STATE7:
                    sResult = "手术结束";
                    break;
                case PublicConsts.OPERATE_STATE8:
                    sResult = "麻醉结束";
                    break;
                case PublicConsts.OPERATE_STATE9:
                    sResult = "出手术室";
                    break;
                case PublicConsts.OPERATE_STATE10:
                    sResult = "准备复苏";
                    break;
                case PublicConsts.OPERATE_STATE11:
                    sResult = "入复苏室";
                    break;
                case PublicConsts.OPERATE_STATE12:
                    sResult = "出复苏室";
                    break;
                case PublicConsts.OPERATE_STATE13:
                    sResult = "转入病房";
                    break;
                case PublicConsts.OPERATE_STATE14:
                    sResult = "病案归档";
                    break;
                case PublicConsts.OPERATE_STATE15:
                    sResult = "取消手术";
                    break;
                default:
                    break;
            }

            return sResult;
        }

        public static string GetProcessState(int processState)
        {
            string sResult = "";
            switch (processState)
            {
                case PublicConsts.PROCSTATE_OUTQUEUE:
                    sResult = "未入队";
                    break;
                case PublicConsts.PROCSTATE_DIAGNOSIS:
                    sResult = "初诊";
                    break;
                case PublicConsts.PROCSTATE_TRIAGE:
                    sResult = "分诊";
                    break;
                case PublicConsts.PROCSTATE_EXCHANGE:
                    sResult = "转诊";
                    break;
                case PublicConsts.PROCSTATE_REDIAGNOSIS:
                    sResult = "复诊";
                    break;
                case PublicConsts.PROCSTATE_PASSTICKET:
                    sResult = "过号初诊";
                    break;
                case PublicConsts.PROCSTATE_DELAY:
                    sResult = "延迟";
                    break;
                case PublicConsts.PROCSTATE_WAITING:
                    sResult = "等候";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA1:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA2:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_WAITAREA3:
                    sResult = "等候中";
                    break;
                case PublicConsts.PROCSTATE_CALLING:
                    sResult = "叫号中";
                    break;
                case PublicConsts.PROCSTATE_PROCESSING:
                    sResult = "就诊中";
                    break;
                case PublicConsts.PROCSTATE_FINISHED:
                    sResult = "已就诊";
                    break;
                case PublicConsts.PROCSTATE_NONARRIVAL:
                    sResult = "过号";
                    break;
                case PublicConsts.PROCSTATE_HANGUP:
                    sResult = "挂起";
                    break;
                case PublicConsts.PROCSTATE_GREENCHANNEL:
                    sResult = "绿色通道";
                    break;
                case PublicConsts.PROCSTATE_ARCHIVE:
                    sResult = "归档";
                    break;
                default:
                    break;
            }

            return sResult;
        }
         

        private static string GetStafferIdByCounterId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例
                CounterInfo info = infoBoss.GetRecordByNo(sNo);

                if (info != null)
                {
                    return info.sLogonStafferNo;
                }
            }
            return "";
        }

        public static int getProcessingCountByServiceNo(string branchNo,string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;  
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                  
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int getProcessingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            { 
                string StafferNo = GetStafferIdByCounterId(counterNo);
                return getProcessingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int getProcessingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now; 
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int getProcessingCountByPatientId(string branchNo, string PatientId)
        {
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And RUserNo='" + PatientId + "'   And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);
                ViewTicketFlows info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    sWhere = " BranchNo = '" + branchNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_CALLING + " And EnqueueTime<'"+info.dEnqueueTime.ToString("yyyy-MM-dd HH:mm:ss") +"'";
                    count = infoBoss.GetCountByCondition(sWhere);
                }

                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByServiceNo(string branchNo, string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now; 
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            { 
                string StafferNo = GetStafferIdByCounterId(counterNo);
                return getWaitingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now; 
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_WAITING + " And " + PublicConsts.PROCSTATE_WAITAREA9 + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByServiceNo(string branchNo, string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now; 
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            { 
                string StafferNo = GetStafferIdByCounterId(counterNo);
                return getQueuingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now; 
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + PublicConsts.PROCSTATE_DIAGNOSIS + " And " + PublicConsts.PROCSTATE_WAITING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getAllCountByStafferNo(string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " StafferNo='" + StafferNo + "' And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}