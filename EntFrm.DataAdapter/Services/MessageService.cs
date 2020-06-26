using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Services
{
    public class MessageService
    {
        public void test()
        {
            //返回类型整型：【1代表推送成功，0等其他均为连接指定队列失败】
            MssgQueueService.MqWsSoapClient d = new MssgQueueService.MqWsSoapClient();
            int i=d.SendMQ("10.177.124.23", "APP_SVRCONN", "QLOCAL.IN.ROOTQ", "IN_QM", 1616, "0003", "VES324", "000000", "000000", "000000", "SYS106","02", "000000", "000000","");

            //返回类型字符型：【返回字符串内容为成功； 0代表连接队列失败 、2代表队列为空】
            string s=d.AnilysisMQ("10.177.124.22", "APP_SVRCONN", "QLOCAL.OUT.SYS001", "OUT_QM", 1818);
             
        }
    }
}
