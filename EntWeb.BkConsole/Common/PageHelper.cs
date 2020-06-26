using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EntWeb.BkConsole
{
    public class PageHelper
    { 
        public static string getBranchInfoNameByNo(string sNo)
        {
            try
            {
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static BranchInfo getBranchInfoByNo(string sNo)
        {
            try
            {
                BranchInfoBLL infoBLL = new BranchInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getCallerInfoNameByNo(string sNo)
        {
            try
            {
                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CallerInfo getCallerInfoByNo(string sNo)
        {
            try
            {
                CallerInfoBLL infoBLL = new CallerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getContentClassNameByNo(string sNo)
        {
            try
            {
                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static ContentClass getContentClassByNo(string sNo)
        {
            try
            {
                ContentClassBLL infoBLL = new ContentClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getContentInfoNameByNo(string sNo)
        {
            try
            {
                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static ContentInfo getContentInfoByNo(string sNo)
        {
            try
            {
                ContentInfoBLL infoBLL = new ContentInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getCounterInfoNameByNo(string sNo)
        {
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CounterInfo getCounterInfoByNo(string sNo)
        {
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getCounterNamesByNos(string sNos)
        {
            try
            {
                string[] arrNos = sNos.Split(';');
                string result = "";

                foreach (string No in arrNos)
                {
                    result += getCounterInfoNameByNo(No) + ";";
                }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string getServiceNamesByNos(string sNos)
        {
            try
            {
                string[] arrNos = sNos.Split(';');
                string result = "";

                foreach (string No in arrNos)
                {
                    result += getServiceInfoNameByNo(No) + ";";
                }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public static string getMaterialClassNameByNo(string sNo)
        {
            try
            {
                DsMaterialClassBLL infoBLL = new DsMaterialClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsMaterialClass getMaterialClassByNo(string sNo)
        {
            try
            {
                DsMaterialClassBLL infoBLL = new DsMaterialClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getMaterialInfoNameByNo(string sNo)
        {
            try
            {
                DsMaterialInfoBLL infoBLL = new DsMaterialInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsMaterialInfo getMaterialInfoByNo(string sNo)
        {
            try
            {
                DsMaterialInfoBLL infoBLL = new DsMaterialInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getPlayerClassNameByNo(string sNo)
        {
            try
            {
                DsPlayerClassBLL infoBLL = new DsPlayerClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsPlayerClass getPlayerClassByNo(string sNo)
        {
            try
            {
                DsPlayerClassBLL infoBLL = new DsPlayerClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getPlayerInfoNameByNo(string sNo)
        {
            try
            {
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getPlayerInfoNameByCode(string sCode)
        {
            try
            {
                int count = 0;
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                DsPlayerInfoCollections infoColl= infoBLL.GetRecordsByPaging(ref count, 1, 1, " PlayerCode='"+sCode+"'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sPlayerName;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getPlayerInfoNameByNos(string sNos)
        {
            try
            {
                string result = "";
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                string[] noArr = sNos.Split(',');

                foreach (string sNo in noArr)
                {
                    result+= infoBLL.GetRecordNameByNo(sNo)+",";
                }

                return result.Trim(',');
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsPlayerInfo getPlayerInfoByNo(string sNo)
        {
            try
            {
                DsPlayerInfoBLL infoBLL = new DsPlayerInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getProgramClassNameByNo(string sNo)
        {
            try
            {
                DsProgramClassBLL infoBLL = new DsProgramClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsProgramClass getProgramClassByNo(string sNo)
        {
            try
            {
                DsProgramClassBLL infoBLL = new DsProgramClassBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getProgramInfoNameByNo(string sNo)
        {
            try
            {
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DsProgramInfo getProgramInfoByNo(string sNo)
        {
            try
            {
                DsProgramInfoBLL infoBLL = new DsProgramInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         

        public static string getEvaluatorInfoNameByNo(string sNo)
        {
            try
            {
                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static EvaluatorInfo getEvaluatorInfoByNo(string sNo)
        {
            try
            {
                EvaluatorInfoBLL infoBLL = new EvaluatorInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        } 

        public static string getLEDDisplayNameByNo(string sNo)
        {
            try
            {
                LEDDisplayBLL infoBLL = new LEDDisplayBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static LEDDisplay getLEDDisplayByNo(string sNo)
        {
            try
            {
                LEDDisplayBLL infoBLL = new LEDDisplayBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getLEDMatrixNameByNo(string sNo)
        {
            try
            {
                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static LEDMatrix getLEDMatrixByNo(string sNo)
        {
            try
            {
                LEDMatrixBLL infoBLL = new LEDMatrixBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getLogsInfoNameByNo(string sNo)
        {
            try
            {
                LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static LogsInfo getLogsInfoByNo(string sNo)
        {
            try
            {
                LogsInfoBLL infoBLL = new LogsInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getMessageInfoNameByNo(string sNo)
        {
            try
            {
                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static MessageInfo getMessageInfoByNo(string sNo)
        {
            try
            {
                MessageInfoBLL infoBLL = new MessageInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getOrganizInfoNameByNo(string sNo)
        {
            try
            {
                OrganizInfoBLL infoBLL = new OrganizInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static OrganizInfo getOrganizInfoByNo(string sNo)
        {
            try
            {
                OrganizInfoBLL infoBLL = new OrganizInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
          

        public static string getRoleInfoNameByNo(string sNo)
        {
            try
            {
                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static RoleInfo getRoleInfoByNo(string sNo)
        {
            try
            {
                RoleInfoBLL infoBLL = new RoleInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         
        public static string getRUsersInfoNameByNo(string sNo)
        {
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static RUsersInfo getRUsersInfoByNo(string sNo)
        {
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getServiceInfoNameByNo(string sNo)
        {
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static ServiceInfo getServiceInfoByNo(string sNo)
        {
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getServiceInfoNoByName(string sName)
        {
            try
            {
                int count = 0;
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ServiceInfoCollections infoColl=infoBLL.GetRecordsByPaging(ref count,1,1," ServiceAlias='"+ sName + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sServiceNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public static string getStafferInfoNameByNo(string sNo)
        {
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        } 

        public static StafferInfo getStafferInfoByNo(string sNo)
        {
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         
        public static string getSUsersInfoNameByNo(string sNo)
        {
            try
            {
                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static SUsersInfo getSUsersInfoByNo(string sNo)
        {
            try
            {
                SUsersInfoBLL infoBLL = new SUsersInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         

        public static string getTicketStyleNameByNo(string sNo)
        {
            try
            {
                TicketStyleBLL infoBLL = new TicketStyleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static TicketStyle getTicketStyleByNo(string sNo)
        {
            try
            {
                TicketStyleBLL infoBLL = new TicketStyleBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         

        public static string getVoiceInfoNameByNo(string sNo)
        {
            try
            {
                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static VoiceInfo getVoiceInfoByNo(string sNo)
        {
            try
            {
                VoiceInfoBLL infoBLL = new VoiceInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getWaitAreaNameByNo(string sNo)
        {
            try
            {
                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static WaitArea getWaitAreaByNo(string sNo)
        {
            try
            {
                WaitAreaBLL infoBLL = new WaitAreaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordByNo(sNo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ViewStafferRota getStafferRotaByStafferNo(string sNo)
        {
            try
            {
                int count = 0;
                ViewStafferRotaBLL infoBLL = new ViewStafferRotaBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                ViewStafferRotaCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " StafferNo='"+ sNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool getOnlineState(string sPlayerNo)
        {
            DsHrtbeatFlowsBLL infoBLL = new DsHrtbeatFlowsBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());

            int count = infoBLL.GetCountByCondition("PlayerNo='" + sPlayerNo + "' And RegistDate>'"+DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss")+"' ");

            return (count > 0) ? true : false;
        }

        public static string getPermitNameByNo(string sNo)
        {
            try
            {
                PermitInfoBLL infoBLL = new PermitInfoBLL(PublicHelper.Get_ConnStr(), PublicHelper.Get_AppCode());
                return infoBLL.GetRecordNameByNo(sNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getWeeksName(string sWeeks)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string[] weeks = sWeeks.Split(',');
                foreach(string w in weeks)
                {
                    sb.Append(CalendarHelper.GetCnNameOfWeek(int.Parse(w) )+ ",");
                }

                return sb.ToString().Trim(',');
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public static string getPlayModeName(string playMode)
        {
            string result = "本地播放";
            switch (playMode)
            {
                case "Local": result = "本地播放"; break;
                case "Online": result = "在线播放"; break;
                default:break;
            }

            return result;
        }

        public static string getProgramTypeName(string programType)
        {
            string result = "默认节目";
            switch (programType)
            {
                case "Default": result = "默认节目"; break;
                case "Looplay": result = "轮播节目"; break;
                case "Intercut": result = "插播节目"; break;
                default: break;
            }

            return result;
        }

    }
}