using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Text;

namespace EntFrm.MainService.Services
{
    public class IMessageService
    {
        private volatile static IMessageService _instance = null;
        private static readonly object lockHelper = new object();

        private static int topn = 3;

        public static IMessageService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IMessageService();
                }
            }
            return _instance;
        }

        private IMessageService(){
            topn = int.Parse(IUserContext.GetConfigValue("WxTopnum"));
        }

        public void SendMessage(object counterNo)
        { 
            string sWhere = ""; 
            DateTime workDate = DateTime.Now.AddMinutes(30);
            string workingMode = IUserContext.GetParamValue(IPublicConsts.DEF_WORKINGMODE, "Others");
            bool wxmessageFlag= bool.Parse(IUserContext.GetConfigValue("WxMessage"));

            if (!wxmessageFlag)
            {
                return;
            }

            ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
            ViewTicketFlowsCollections ticketFlows = null;


            if (workingMode.Equals("SERVICE"))
            {
                sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + counterNo + "%' And ProcessState Between " + IPublicConsts.PROCSTATE_DIAGNOSIS + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + " And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

            }
            else if (workingMode.Equals("STAFF"))
            {
                //获取登录窗口的医生/员工编号
                string stafferNo = IPublicHelper.GetCounterByNo(counterNo.ToString()).sLogonStafferNo;
                sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + stafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_DIAGNOSIS + " And " + IPublicConsts.PROCSTATE_WAITAREA9 + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
            }

            SqlModel s_model = new SqlModel();

            s_model.iPageNo = 1;
            s_model.iPageSize = topn;
            s_model.sFields = "*";
            s_model.sCondition = sWhere;
            s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
            s_model.sOrderType = "Asc";
            s_model.sTableName = "ViewTicketFlows";

            ticketFlows = ticketBoss.GetRecordsByPaging(s_model);

            StringBuilder sb = new StringBuilder();
             
             
            if (ticketFlows != null && ticketFlows.Count > 0)
            { 
                sb.Append("<msg>");
                int i = 1;
                foreach (ViewTicketFlows ticketFlow in ticketFlows)
                {
                    sb.Append("<row action='new'>");
                    sb.Append("<PATIENT_NO>" + ticketFlow.sRUserNo + "</PATIENT_NO>");
                    sb.Append("<DEPT_NAME>" + IPublicHelper.GetBranchNameByNo(ticketFlow.sBranchNo) + "</DEPT_NAME>");
                    sb.Append("<DOC_NAME>" + IPublicHelper.GetStafferNameById(ticketFlow.sStafferNo) + "</DOC_NAME>");
                    sb.Append("<SEE_NO>" + ticketFlow.sTicketNo + "</SEE_NO>");
                    sb.Append("<AHEAD_NO>" + i + "</AHEAD_NO>");
                    sb.Append("<CLINIC_NAME>" + IPublicHelper.GetCounterNameByNo(counterNo.ToString()) + "</CLINIC_NAME>");
                    sb.Append("<POSITION>N/A</POSITION> ");
                    sb.Append("<REG_NO>"+ ticketFlow.sPFlowNo+ "</REG_NO>");
                    sb.Append("</row>");

                    i++;
                }
                sb.Append("</msg>"); 

                try
                { 
                    MessagePushService.MqWsSoapClient c = new MessagePushService.MqWsSoapClient();
                    //推送
                    c.SendMQ("10.177.124.23", "APP_SVRCONN", "QLOCAL.IN.ROOTQ", "IN_QM", 1616, "SYS106", "VES324", "0003", "000000", "000000", "000000", "02", "000000", "000000", sb.ToString());
                     
                }
                catch(Exception ex)
                {
                    MainFrame.PrintMessage("推送消息出错提示：" + ex.Message);
                }
            }
        }
    }
}
