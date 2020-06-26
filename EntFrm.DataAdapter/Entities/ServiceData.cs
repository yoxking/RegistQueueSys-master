using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntFrm.DataAdapter.Entities
{
    public class ServiceData
    { 

        public string ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAlias { get; set; }
        public string ServiceType { get; set; }
        public string AmLimit { get; set; }
        public string AmStartTime { get; set; }
        public string AmEndTime { get; set; }
        public string PmLimit { get; set; }
        public string PmStartTime { get; set; }
        public string PmEndTime { get; set; }
        public string WeekLimit { get; set; }
        public string WeekDays { get; set; }
    }
}