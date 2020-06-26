using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.SettingConsole
{
    public class CounterGroup
    {
        private int CheckedFlag;

        public int iCheckedFlag
        {
            get { return CheckedFlag; }
            set { CheckedFlag = value; }
        }

        private string CounterNo;

        public string sCounterNo
        {
            get { return CounterNo; }
            set { CounterNo = value; }
        }
        private string CounterName;

        public string sCounterName
        {
            get { return CounterName; }
            set { CounterName = value; }
        }
        private string CounterAlias;

        public string sCounterAlias
        {
            get { return CounterAlias; }
            set { CounterAlias = value; }
        }
    }
}
