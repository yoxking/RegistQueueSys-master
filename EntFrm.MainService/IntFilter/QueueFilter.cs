using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Utility;

namespace EntFrm.MainService
{
    public class QueueFilter : InterFilter
    {
        public InterFilter filter(string[] source)
        {
            //string rules1 = "doSignIn,doInService,doCallNextTicket,doFinishTicket,doNotcomeTicket,doAbortTicket,doTransferTicket,doSpecialTicket,doAddNewTicket1,doAddNewTicket2";
            string rules1 = "doInService,doCallNextTicket,doFinishTicket,doNotcomeTicket,doAbortTicket,doSpecialTicket,doAddNewTicket1,doAddNewTicket2";
            string rules2 = "doSignOut,doOutService,doClearQueueByCounterNo,doClearQueueByStafferNo";
            string counterNo = "";

            if (source != null && source.Length > 1 && !string.IsNullOrEmpty(source[1]))
            {
                counterNo = source[1].Split('$')[0];

                if (rules1.IndexOf(source[0]) >= 0)
                {
                    IUserContext.SetStateValue(counterNo, "1111111111");
                    AddServiceByCounterNo(counterNo, "1111111111");
                    AddStaffByCounterNo(counterNo, "1111111111");
                }

                if (rules2.IndexOf(source[0]) >= 0)
                {
                    IUserContext.SetStateValue(counterNo, "0000000000");
                } 
            }

            return this;
        } 

        private void AddServiceByCounterNo(string counterNo, string sValue)
        {
            CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            CounterInfo info = infoBoss.GetRecordByNo(counterNo);

            if (info != null)
            {
                string[] temps = info.sServiceGroupValue.Split(';');
                foreach (string temp in temps)
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        IUserContext.SetStateValue(temp.Split(':')[0], sValue);
                    }
                }
            }
        }

        private void AddStaffByCounterNo(string counterNo, string sValue)
        {
            CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            CounterInfo info = infoBoss.GetRecordByNo(counterNo);

            if (info != null)
            {
                string StafferNo = info.sLogonStafferNo;

                if (!string.IsNullOrEmpty(StafferNo))
                {
                    IUserContext.SetStateValue(StafferNo, sValue);
                }
            }
        }
    }
}
