using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class MsgModel
    {
        public string MsgNo { set; get; }

        public string MsgType { set; get; }
        public string MTitle { set; get; }
        public string MContent { set; get; }
        public string Sender { set; get; }

        public DateTime SendTime { set; get; }

        public String ReadState { set; get; }
    }
}