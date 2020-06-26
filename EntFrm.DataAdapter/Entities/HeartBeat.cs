using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.DataAdapter.Entities
{
    public class HeartBeat
    {
        public string PlayerCode { set; get; }
        public string IpAddress { set; get; }
        public string MacAddress { set; get; }
        public string LocalPort { set; get; }
        public string Resolution { set; get; }
        public string MdiVolume { set; get; }//声音
        public string OSVersion { set; get; }//android版本
        public string ApVersion { set; get; }//app版本

        public string PlayRecord;//节目播放记录

    }
}
