using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntFrm.DataAdapter.Entities
{
    public class CounterData
    { 

        public string CounterNo { get; set; }
        public string CounterName { get; set; }
        public string CounterAlias { get; set; }
        public int LogonState { get; set; }
        public int PauseState { get; set; }
    }
}