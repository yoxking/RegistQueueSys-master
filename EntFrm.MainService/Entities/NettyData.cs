using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.MainService.Entities
{
    public class NettyData
    {

        public int type { set; get; }
        //必须唯一，否者会出现channel调用混乱
        public string devCode { set; get; }
        public string data { set; get; }
    }
}