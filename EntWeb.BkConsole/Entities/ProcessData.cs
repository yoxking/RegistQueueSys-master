using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole
{
    public class ProcessData
    {
        public string processCode { set; get; }
        public string callingData { set; get; }
        public string waitingData { set; get; }
        public string queuingData { set; get; }
    }
}