using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.CallerConsole
{
    public class ItemTicket
    {
        public string PFlowNo { set; get; }
        public string TicketNo { set; get; }
        public string UserName { set; get; }

        public string EnqueueDate { set; get; }
        public string ProcessDate { set; get; }

        public string ProcessState { set; get; }
    }
}
