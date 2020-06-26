using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntFrm.DataAdapter.Entities
{
    public class VTicketData
    { 

        public string PFlowNo { get; set; }
        public string TicketNo { get; set; }
        public string ServiceName { get; set; }
        public DateTime QueueTime { get; set; }
        public string CounterGroup { get; set; }
        public int ProcessStatus { get; set; }
        public string CnName { get; set; }
        public string Telphone { get; set; }
        public string CardNo { get; set; }
        public string StrResult { get; set; }
    }
}