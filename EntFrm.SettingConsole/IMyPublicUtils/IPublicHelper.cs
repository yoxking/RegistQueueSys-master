using EntFrm.Business.BLL;
using EntFrm.Business.Model;

namespace EntFrm.SettingConsole
{
    public class IPublicHelper
    { 
        public static ServiceInfo GetServiceByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetServiceNameByNo(string sNo)
        {

            ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            return infoBoss.GetRecordNameByNo(sNo);

        } 

        public static CounterInfo GetCounterByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetCounterNameByNo(string No)
        {
            CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            return infoBoss.GetRecordNameByNo(No);
        }

        public static TicketFlows GetTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                TicketFlowsBLL infoBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static ViewTicketFlows GetVTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }
    }
}

