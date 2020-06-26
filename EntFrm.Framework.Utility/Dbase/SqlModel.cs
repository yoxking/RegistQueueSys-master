using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.Framework.Utility
{
    public class SqlModel
    {
        public SqlModel()
        {
        }

        private int ipageNo;
        public int iPageNo
        {
            set { ipageNo = value; }
            get { return ipageNo; }
        }

        private string scondition;
        public string sCondition
        {
            set { scondition = value; }
            get { return scondition; }
        }

        private int ipageSize;
        public int iPageSize
        {
            set { ipageSize = value; }
            get { return ipageSize; }
        }

        private string sfields;
        public string sFields
        {
            set { sfields = value; }
            get { return sfields; }
        }

        private string stableName;
        public string sTableName
        {
            set { stableName = value; }
            get { return stableName; }
        }

        private string sorderField;
        public string sOrderField
        {
            set { sorderField = value; }
            get { return sorderField; }
        }

        private string sorderType;
        public string sOrderType
        {
            set { sorderType = value; }
            get { return sorderType; }
        }
    }
}
