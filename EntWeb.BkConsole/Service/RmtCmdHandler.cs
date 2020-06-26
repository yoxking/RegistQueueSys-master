using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Framework.Utility;
using EntWeb.BkConsole.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EntWeb.BkConsole.Service
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
            try
            {
                string ipAddress = ((IPEndPoint)context.Channel.RemoteAddress).Address.ToString().Substring(7);

                ResultData resultData = JsonConvert.DeserializeObject<ResultData>(message);

                if (resultData != null)
                {
                     
                }

                context.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}