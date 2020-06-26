using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Entities
{
    public class TicketFlowData
    {
        public string sTicketNo { set; get; }
        public string sServiceName { set; get; }
        public DateTime dQueueTime { set; get; }
        public string sProcessStatus { set; get; }
        public DateTime dStartTime { set; get; }
        public DateTime dFinishedTime { set; get; }
        public string sProcessedCounterName { set; get; }
        public string sProcessedStafferName { set; get; }
        public string sComments { set; get; }
    }
}