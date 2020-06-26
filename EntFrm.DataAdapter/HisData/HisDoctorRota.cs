using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.HisData
{
    public class HisDoctorRota
    {
        public int Id { set; get; }
        public string DoctorId { set; get; }
        public string CounterId { set; get; }
        public string WeekDay1 { set; get; }
        public string WeekDay2 { set; get; }
        public string WeekDay3 { set; get; }
        public string WeekDay4 { set; get; }
        public string WeekDay5 { set; get; }
        public string WeekDay6 { set; get; }
        public string WeekDay7 { set; get; }
        public string Remark { set; get; }   //备注
    }
}
