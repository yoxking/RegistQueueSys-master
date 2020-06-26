using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class TableData
    {
        private int Code;
        private string Msg;
        private int Count;
        private object Data;

        public int code { get { return this.Code; }set { this.Code = value; } }
        public string msg { get { return this.Msg; } set { this.Msg = value; } }
        public int count { get { return this.Count; } set { this.Count = value; } }
        public object data { get { return this.Data; } set { this.Data = value; } }
    }
}