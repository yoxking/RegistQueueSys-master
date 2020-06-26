using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.Framework.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace EntWeb.HDeptConsole
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
            catch(Exception ex) { }
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

        private static string getBranchName()
        { 
            BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetRecordNameByNo(Get_BranchNo());
        } 

        private static int getWaitingNum()
        {
            DateTime workDate = DateTime.Now;
            string strWhere = " BranchNo='" + Get_BranchNo() + "' And ProcessState Between " + PublicConsts.PROCSTATE_OUTQUEUE + " And " + PublicConsts.PROCSTATE_CALLING + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

            ViewTicketFlowsBLL infoBLL = new ViewTicketFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetCountByCondition(strWhere);
        } 

        private static int getRegisteNum()
        {
            DateTime workDate = DateTime.Now;
            string strWhere = " BranchNo='" + PublicHelper.Get_BranchNo() + "'  And RegistDate Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";

            ViewRegistFlowsBLL infoBLL = new ViewRegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            return infoBLL.GetCountByCondition(strWhere);
        }

        private static string getUserEMail(string userNo)
        {
            SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            SUsersInfo info = infoBLL.GetRecordByNo(userNo);

            if (info != null)
            {
                return info.sEMail;
            }
            return "";
        }

        //定义业务、窗口、员工的状态，有更新:11111,无更新:00000
        public static bool SetStateValue(string sName, string sValue, int index = -1)
        {
            if (index < 0)
            {
                return SetParamValue(sName, sValue, "State");
            }
            else
            {
                sValue = sValue.Substring(0, 1);
                string temp = GetParamValue(sName, "State");
                if (!string.IsNullOrEmpty(temp))
                {
                    temp = temp.Remove(index, 1);
                    temp = temp.Insert(index, sValue);

                    return SetParamValue(sName, temp, "State");
                }
                else
                {
                    if (sValue.Equals("0"))
                    {
                        return SetParamValue(sName, "0000000000", "State");
                    }
                    else
                    {
                        return SetParamValue(sName, "1111111111", "State");
                    }
                }
            }
        }

        public static string GetStateValue(string sName, int index = -1)
        {
            string result = "";
            if (index < 0)
            {
                result = GetParamValue(sName, "State");
            }
            else
            {
                string temp = GetParamValue(sName, "State");
                if (!string.IsNullOrEmpty(temp) && index < temp.Length)
                {
                    result = temp[index].ToString();
                }
            }

            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        public static bool SetParamValue(string sName, string sValue, string sType = "2")
        {
            if (sName.Length > 0 && sValue.Length > 0)
            {
                try
                {
                    string sSuNo = "00000000";

                    int count = 100;
                    SysParams info = null;

                    SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + PublicHelper.Get_BranchNo() + "'  And  KeyName='" + sName + "' And KeyType='" + sType + "' ");

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
                        info.sBranchNo = Get_BranchNo();

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

        public static string GetParamValue(string sName, string sType = "Others")
        {
            if (sName.Length > 0)
            {
                try
                {
                    int count = 0;


                    SysParamsBLL infoBLL = new SysParamsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                    string sWhere = " BranchNo='" + Get_BranchNo() + "' And  KeyName='" + sName + "' And KeyType='" + sType + "' ";
                    SysParamsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, sWhere);
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

        public static ViewExpress Get_ViewExpress()
        {
            //校验用户是否已经登录	 	
            if (HttpContext.Current.Session["loginUser"] == null)
            {
                //跳转到登陆页				 		
                HttpContext.Current.Response.Redirect("~/Home/Index");
            }

            LoginerInfo loginInfo = (LoginerInfo)HttpContext.Current.Session["loginUser"];

            ViewExpress viewExpress = new ViewExpress();
            viewExpress.HomeUrl = GetConfigValue("HomeUrl");
            viewExpress.ViewTag = "MainFrame";
            viewExpress.ViewTitle = getBranchName();
            viewExpress.VSubtitle = GetConfigValue("Subtitle");
            viewExpress.ViewDesc = GetConfigValue("WebDesc");
            viewExpress.UserName = loginInfo.LoginId;
            viewExpress.UserEmail = getUserEMail(loginInfo.UserNo);
            viewExpress.WaitingNum = getWaitingNum();
            viewExpress.RegisteNum = getRegisteNum();

            return viewExpress;
        }

        public static List<TicketFlow> ConvertTicketFlows(ViewTicketFlowsCollections infoColl)
        {

            if (infoColl != null && infoColl.Count > 0)
            {
                List<TicketFlow> tflowList = new List<TicketFlow>();
                TicketFlow tflow = null;

                foreach (ViewTicketFlows info in infoColl)
                {
                    tflow = new TicketFlow();
                    tflow.PFlowNo = info.sPFlowNo;
                    tflow.TicketNo = info.sTicketNo;
                    tflow.RUserNo = info.sRUserNo;
                    tflow.RUserName = info.sCnName;
                    tflow.StafferName = PageService.GetStafferName(info.sStafferNo);
                    tflow.CounterName = info.sCounterNos;
                    tflow.ServiceName = PageService.GetServiceName(info.sServiceNo);
                    tflow.ProcessState = PageService.GetProcessState(info.iProcessState);
                    tflow.RegistTime = DateTime.Now;
                    tflow.RegistType = PageService.GetRUserName(info.sRUserNo);
                    tflow.RegWorkTime = getRegWorkTimeType(info.sRUserNo,DateTime.Now);
                    tflow.EnqueueTime = info.dEnqueueTime;
                    tflow.BeginTime = info.dBeginTime;
                    tflow.FinishTime = info.dFinishTime;
                    tflow.PriorityType = PageService.GetPriorityType(info.iPriorityType);
                    tflow.DelayTime = info.iDelayTimeValue;

                    tflow.WaitAreaName = PageService.GetWAreaName(info.sWaitAreaNo);
                    tflowList.Add(tflow);
                }

                return tflowList;
            }
            else
            {
                return null;
            }
        }

        private static string getRegWorkTimeType(string ruserNo,DateTime dtTime)
        {
            int count = 0;
            RegistFlowsBLL infoBLL = new RegistFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
            RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " RUserNo='"+ruserNo+"' ");

            if (infoColl != null && infoColl.Count > 0)
            {
                return PageService.GetWorkTimeType(infoColl.GetFirstOne().iWorkTime);
            }
            return "";
        }

        public static List<MsgModel> ConvertMessageList(MessageInfoCollections infoColl)
        {

            if (infoColl != null && infoColl.Count > 0)
            {
                List<MsgModel> mssgList = new List<MsgModel>();
                MsgModel mssg = null;

                foreach (MessageInfo info in infoColl)
                {
                    mssg = new MsgModel();
                    mssg.MsgNo = info.sMessNo;
                    mssg.MsgType = info.iMType==0?"普通消息":"系统消息";
                    mssg.MTitle = info.sMTitle;
                    mssg.MContent = info.sMContent;
                    mssg.Sender = PageService.GetStafferName(info.sMSender);
                    mssg.SendTime = info.dSendDate;
                    mssg.ReadState = info.iReadState == 0 ? "未读" : "已读";
                    mssgList.Add(mssg);
                }

                return mssgList;
            }
            else
            {
                return null;
            }
        }

        public static List<RegistFlow> ConvertRegistList(ViewRegistFlowsCollections infoColl)
        {

            if (infoColl != null && infoColl.Count > 0)
            {
                List<RegistFlow> regList = new List<RegistFlow>();
                RegistFlow reg = null;

                foreach (ViewRegistFlows info in infoColl)
                {
                    reg = new RegistFlow();
                    reg.RegistNo = info.sRFlowNo;
                    reg.RegistType = PageService.GetRegisteType(info.iRegistType);
                    reg.StafferName = PageService.GetStafferName(info.sStafferNo);
                    reg.ServiceName = PageService.GetServiceName(info.sServiceNo);
                    RUsersInfo r=PageService.GetRUserInfo(info.sRUserNo);
                    if (r!=null)
                    {
                        reg.RUserNo = r.sRUserNo;
                        reg.RUserName =r.sCnName;
                        reg.RUserTel = r.sTelphone; 
                    }
                    else
                    {
                        reg.RUserNo = "";
                        reg.RUserName = "";
                        reg.RUserTel = "";
                    }
                    reg.RegistTime = info.dRegistDate;
                    reg.RegWorkTime = PageService.GetWorkTimeType(info.iWorkTime);
                    reg.RegistState = info.iRegistState == 1 ? "预约中" : "已处理";
                    reg.DataFrom = info.sDataFrom;
                    regList.Add(reg);
                }

                return regList;
            }
            else
            {
                return null;
            }
        }

        public static List<RotaModel> ConvertStaffRotas(ViewStafferRotaCollections infoColl)
        {
            try
            {
                if (infoColl != null && infoColl.Count > 0)
                {
                    List<RotaModel> rotaList = new List<RotaModel>();
                    RotaModel rota = null;

                    foreach (ViewStafferRota info in infoColl)
                    {
                        rota = new RotaModel();
                        rota.RotaNo = info.sRotaNo;
                        rota.RotaName = PageService.GetStafferName(info.sStafferNo);
                        rota.RotaType = info.iRotaType == 1 ? "正常排班" : "临时排班";
                        rota.Week1 = PageService.GetWorkTimeType(info.iWeekDay1);
                        rota.Week2 = PageService.GetWorkTimeType(info.iWeekDay2);
                        rota.Week3 = PageService.GetWorkTimeType(info.iWeekDay3);
                        rota.Week4 = PageService.GetWorkTimeType(info.iWeekDay4);
                        rota.Week5 = PageService.GetWorkTimeType(info.iWeekDay5);
                        rota.Week6 = PageService.GetWorkTimeType(info.iWeekDay6);
                        rota.Week7 = PageService.GetWorkTimeType(info.iWeekDay7);
                        rotaList.Add(rota);
                    }

                    return rotaList;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { return null; }
        }

        public static List<RotaModel> ConvertServiceRota(ViewServiceRotaCollections infoColl)
        {
            try
            {
                if (infoColl != null && infoColl.Count > 0)
                {
                    List<RotaModel> rotaList = new List<RotaModel>();
                    RotaModel rota = null;

                    foreach (ViewServiceRota info in infoColl)
                    {
                        rota = new RotaModel();
                        rota.RotaNo = info.sRotaNo;
                        rota.RotaName = PageService.GetServiceName(info.sServiceNo);
                        rota.RotaType = info.iRotaType == 1 ? "正常排班" : "临时排班";
                        rota.Week1 = PageService.GetWorkTimeType(info.iWeekDay1);
                        rota.Week2 = PageService.GetWorkTimeType(info.iWeekDay2);
                        rota.Week3 = PageService.GetWorkTimeType(info.iWeekDay3);
                        rota.Week4 = PageService.GetWorkTimeType(info.iWeekDay4);
                        rota.Week5 = PageService.GetWorkTimeType(info.iWeekDay5);
                        rota.Week6 = PageService.GetWorkTimeType(info.iWeekDay6);
                        rota.Week7 = PageService.GetWorkTimeType(info.iWeekDay7);
                        rotaList.Add(rota);
                    }

                    return rotaList;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { return null; }
        }

    }
}