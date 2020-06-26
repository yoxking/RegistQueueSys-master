using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Entities
{
    public class EvalStatsData
    {
        public string sStafferNo { set; get; }
        public string sLoginId { set; get; }
        public string sStafferName { set; get; }
        public int iTotalScore { set; get; }
        public int iVGoodNum { set; get; }
        public int iGoodNum { set; get; }
        public int iNormalNum { set; get; }
        public int iBadNum { set; get; }
        public int iUnknownNum { set; get; }
        public string sComments { set; get; }
    }
}