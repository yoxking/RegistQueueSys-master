using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.HisData
{
    public class HisRecipeInfo
    { 
        public string Guid { set; get; }  //no //单据号
        public string RegistId { set; get; }  //门诊号
        public string HospitId { set; get; }  //住院号
        public string TicketId { set; get; }  //票号/序号
        public string PatId { set; get; }  //病人id
        public string PatName { set; get; }
        public string PatType { set; get; } //病人类型
        public string PatSex { set; get; }
        public string PatAge { set; get; }
        public string PatPhone { set; get; }
        public string PatIdNo { set; get; }
        public string CounterId { set; get; }
        public string CounterName { set; get; }  //发药窗口
        public string BranchId { set; get; }
        public string BranchName { set; get; }  //药房
        public string RegistTime { set; get; }  //填制日期
        public string UpdateTime { set; get; }  //
        public string Status { set; get; }  //记录状态:0,'未收费',1,'已收费',2,'已退费'
        public string Remark { set; get; }   //备注
    }
}
