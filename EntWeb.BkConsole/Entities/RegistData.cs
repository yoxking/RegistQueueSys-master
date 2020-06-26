using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Entities
{
    public class RegistData
    {
        public string id { set; get; }
        public string dpId { set; get; }
        public string dpName { set; get; }
        public string dpLocation { set; get; }
        public string sortDate { set; get; }
        public string sortTime { set; get; }
        public string itemName { set; get; }
        public string itemTip { set; get; }
        public string sortNo { set; get; }
        public string waitingNo{set;get;}
        public string status { set; get; }
    }
}