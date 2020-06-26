using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Entities
{
    public class CmmdData
    { 
        public string cmmdType { set; get; }
        public string cmmdName { set; get; }
        public string[] cmmdArgs { set; get; }
    }
}