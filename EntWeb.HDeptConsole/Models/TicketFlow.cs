using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class TicketFlow
    {
        public string PFlowNo { set; get; }
        public string TicketNo { set; get; }
        public string RUserNo { set; get; }
        public string RUserName { set; get; }
        public string StafferName { set; get; }
        public string CounterName { set; get; }
        public string ServiceName { set; get; }
        public string ProcessState { set; get; }
        public DateTime RegistTime { set; get; }
        public string RegistType { set; get; }
        public string RegWorkTime { set; get; }
        public DateTime EnqueueTime { set; get; }
        public DateTime BeginTime { set; get; }
        public DateTime FinishTime { set; get; }
        public string PriorityType { set; get; }
        public double DelayTime { set; get; }
        public string WaitAreaName { set; get; }

    }
}