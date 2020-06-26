using DotNetty.Transport.Channels;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EntFrm.MainService.Services
{
    public class RmtCmdHandler : SimpleChannelInboundHandler<string>
    {
        private int idle_count = 1;

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }

        protected override void ChannelRead0(IChannelHandlerContext context, string message)
        {
            context.CloseAsync();
            //try
            //{
            //    string ipAddress = ((IPEndPoint)context.Channel.RemoteAddress).Address.ToString().Substring(7);

            //    ResultData resultData = JsonConvert.DeserializeObject<ResultData>(message);

            //    if (resultData != null)
            //    {

            //    }

            //    context.CloseAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }
    }
}