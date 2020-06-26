using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.HisData
{
    /// <summary>
    /// 医生信息
    /// </summary>
    public class HisDoctorInfo
    {
        public string DocId { set; get; }
        public string DocName { set; get; }
        public string DocIdNo { set; get; }
        public string DocSex { set; get; }
        public string DocAge { set; get; } 
        public string DocPhoto { set; get; }
        public string DocResume { set; get; }  //聘任技术职务,1,'正高',2,'副高',3,'中级',4,'初级',5,'初级',9,'待聘') as 职称
        public string BranchId { set; get; }   //科室编号
        public string Remark { set; get; }   //备注

    }
}
