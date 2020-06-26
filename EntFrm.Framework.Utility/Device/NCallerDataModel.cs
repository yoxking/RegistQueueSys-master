using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public class NCallerDataModel
    {
        private int address;
        private int funcode;
        private string optdata;
        private string optcode;

        public NCallerDataModel()
        {

        }

        public NCallerDataModel(int address, int funcode, string optdata,string optcode)
        {
            this.address = address;
            this.funcode = funcode;
            this.optdata = optdata;
            this.optcode = optcode;
        }

        public int Address {
            set { this.address = value; }
            get { return this.address; }
        }
        public int Funcode
        {
            set { this.funcode = value; }
            get { return this.funcode; }
        }
        public string Optdata
        {
            set { this.optdata = value; }
            get { return this.optdata; }
        }
        public string Optcode { 
            set { this.optcode = value; }
            get { return this.optcode; }
        }
    }
}
