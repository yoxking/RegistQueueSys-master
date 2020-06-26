using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class ResultData
    {
        private string code;
        private string message;

        public string Code { set { this.code = value; } get { return this.code; } }
        public string Message { set { this.message = value; } get { return this.message; } }

        public ResultData(string code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}