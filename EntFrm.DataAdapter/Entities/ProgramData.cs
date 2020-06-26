using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Entities
{
    public class ProgramData
    {
        public long id { set; get; }
        public string programNo { set; get; }
        public string programName { set; get; }
        public string programType { set; get; }
        public string playMode { set; get; }
        public string pfilePath { set; get; }
        public string pwebUrl { set; get; }

        public int pversion { set; get; }
        public string duration { set; get; }
        public string resolution { set; get; }
        public string playWeeks { set; get; }
        public string startDate { set; get; }
        public string enditDate { set; get; }
        public string startTime { set; get; }
        public string enditTime { set; get; }
        public string playStatus { set; get; }
        public int playCount { set; get; }
        public string dloadStatus { set; get; }
        public int dloadCount { set; get; }
        public string dloadDate { set; get; }
        public string pComments { set; get; }
    }
}
