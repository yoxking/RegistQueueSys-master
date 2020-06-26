using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.MainService.Entities
{
    public class NettyType
    {
        //模式
        public const int HRTBEAT = 1;//心跳

        public const int REGISTER = 2;//注册

        public const int COMMAND = 3;//命令

        public const int RESULT = 4; //结果
    }
}