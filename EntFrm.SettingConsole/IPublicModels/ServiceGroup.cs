using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.SettingConsole
{
    public class ServiceGroup
    {
        private int CheckedFlag;

        public int iCheckedFlag
        {
            get { return CheckedFlag; }
            set { CheckedFlag = value; }
        } 

        private string ServiceNo;

        public string sServiceNo
        {
            get { return ServiceNo; }
            set { ServiceNo = value; }
        }
        private string ServiceName;

        public string sServiceName
        {
            get { return ServiceName; }
            set { ServiceName = value; }
        }
        private string ServiceAlias;

        public string sServiceAlias
        {
            get { return ServiceAlias; }
            set { ServiceAlias = value; }
        }
        private int Priority;

        public int iPriority
        {
            get { return Priority; }
            set { Priority = value; }
        }
    }
}
