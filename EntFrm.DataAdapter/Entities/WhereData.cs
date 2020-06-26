using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Entities
{
    public class WhereData
    {
        public string field { set; get; }
        public string operate { set; get; }
        public string relation { set; get; }
        public string value { set; get; }
    }
}
