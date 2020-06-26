using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using EntFrm.DataAdapter.Entities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Services
{
    public class NettyHostService
    { 
    private volatile static NettyHostService _instance = null;
    private static readonly object lockHelper = new object();

        private MultithreadEventLoopGroup bossGroup =null;
        private MultithreadEventLoopGroup workerGroup =null;

    public static NettyHostService CreateInstance()
    {
        if (_instance == null)
        {
            lock (lockHelper)
            {
                if (_instance == null)
                    _instance = new NettyHostService();
            }
        }
        return _instance;
    }

        private NettyHostService() { }

        /// <summary>  
        /// socket发送字节数组  
        /// </summary>   
        /// <param name="port">服务器端口</param>  
        /// <returns>接收的byte[]</returns>  
        public async void StartHostService()
        {

            int port = int.Parse(IUserContext.GetConfigValue("WTcpPort"));
            // 主工作线程组，设置为1个线程
            bossGroup = new MultithreadEventLoopGroup(1);
            // 工作线程组，默认为内核数*2的线程数
            workerGroup = new MultithreadEventLoopGroup();

            try
            {
                //声明一个服务端Bootstrap，每个Netty服务端程序，都由ServerBootstrap控制，
                //通过链式的方式组装需要的参数

                var bootstrap = new ServerBootstrap();
                 

                bootstrap.Group(bossGroup, workerGroup) // 设置主和工作线程组
                            .Channel<TcpServerSocketChannel>() // 设置通道模式为TcpSocket
                            .Option(ChannelOption.SoKeepalive, true) 
                            .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                            {
                                //工作线程连接器 是设置了一个管道，服务端主线程所有接收到的信息都会通过这个管道一层层往下传输
                                //同时所有出栈的消息 也要这个管道的所有处理器进行一步步处理

                                IChannelPipeline pipeline = channel.Pipeline;
                                pipeline.AddLast("framer", new DelimiterBasedFrameDecoder(int.MaxValue, Delimiters.LineDelimiter()));
                                pipeline.AddLast("decoder", new StringDecoder());
                                pipeline.AddLast("encoder", new StringEncoder());
                                pipeline.AddLast("handler", new NettyHostHandler());
                            }));

                // bootstrap绑定到指定端口的行为 就是服务端启动服务，同样的Serverbootstrap可以bind到多个端口
                IChannel boundChannel = await bootstrap.BindAsync(port);
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "主机监听服务启动完成...");
                //Console.ReadLine();
                //关闭服务
                //await boundChannel.CloseAsync();
            }
            catch(Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "主机监听服务启动失败,("+ex.Message+")"); 
            }
            finally
            {
                //释放工作组线程
                //await Task.WhenAll(
                //    bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                //    workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }

        public async void StopHostService()
        {
            try
            {
                if (bossGroup != null && workerGroup != null)
                {

                    //释放工作组线程
                    await Task.WhenAll(
                        bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                        workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void SendCommandData(String devCode, String cmmdData)
        {
            try
            {
                IChannel channel = NettyChannelMap.getChannel(devCode); 

                if (channel != null && channel.Active)
                {
                    NettyData ndata = new NettyData();
                    ndata.devCode=devCode;
                    ndata.type=NettyType.COMMAND;
                    ndata.data = cmmdData;

                    channel.WriteAndFlushAsync(JsonConvert.SerializeObject(ndata) + "\r\n");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

