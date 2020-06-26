using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.HisData
{
    public class HisBranchInfo
    {
        public string BranchId { set; get; }
        public string BranchName { set; get; }
        public string ParentId { set; get; }
        public string ParentName { set; get; }
        public string BranchType { set; get; }
        public string Remark { set; get; }   //备注
    }
}
