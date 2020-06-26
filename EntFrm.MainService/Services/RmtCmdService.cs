using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using EntFrm.MainService.Entities;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EntFrm.MainService.Services
{

    public class RmtCmdService
    {
        private volatile static RmtCmdService _instance = null;
        private static readonly object lockHelper = new object();

        private MultithreadEventLoopGroup group = new MultithreadEventLoopGroup(16);

        public static RmtCmdService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new RmtCmdService();
                }
            }
            return _instance;
        }

        /// <summary>  
        /// NETTY发送信息  
        /// </summary>     
        /// <param name="message">要发送命令</param>  
        public async void doRemoteCommand(string devCode, string commandStr)
        { 
            try
            {
                string ipAddress = IUserContext.GetConfigValue("MAdapterIp");
                int wtcpPort = int.Parse(IUserContext.GetConfigValue("MAdapterPort"));

                NettyData nettyData = new NettyData();
                nettyData.devCode = devCode;
                nettyData.type = NettyType.COMMAND;
                nettyData.data = commandStr;

                string message = JsonConvert.SerializeObject(nettyData);

                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast("framer", new DelimiterBasedFrameDecoder(int.MaxValue, Delimiters.LineDelimiter()));
                        pipeline.AddLast("decoder", new StringDecoder());
                        pipeline.AddLast("encoder", new StringEncoder());
                        pipeline.AddLast("handler", new RmtCmdHandler());
                    }));

                IChannel clientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(ipAddress), wtcpPort));

                await clientChannel.WriteAndFlushAsync(message + "\r\n");//发送消息 
            }
            catch (Exception ex) { }
            finally
            {
            }
        }
    }
}