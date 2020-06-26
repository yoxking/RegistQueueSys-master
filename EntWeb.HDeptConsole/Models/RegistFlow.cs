using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class RegistFlow
    {
        public string RegistNo { set; get; }
        public string RegistType { set; get; }
        public string StafferName { set; get; }
        public string ServiceName { set; get; }
        public string RUserNo { set; get; }
        public string RUserName { set; get; }
        public string RUserTel { set; get; }
        public DateTime RegistTime { set; get; }

        public string RegWorkTime { set; get; }

        public string RegistState { set; get; }
        public string DataFrom { set; get; }
    }
}