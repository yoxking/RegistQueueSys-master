using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Entities
{
    public class TicketStatsData
    {
        public string sItemName { set; get; }
        public int iTotalNum { set; get; }
        public int iFinishedNum { set; get; }
        public int iAbsentNum { set; get; }
        public int iTransfNum { set; get; }
        public DateTime dProcessedDate { set; get; }
        public string sComments { set; get; }
    }
}