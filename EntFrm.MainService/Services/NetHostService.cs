using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

namespace EntFrm.MainService.Services
{
    public class NetHostService
    {
        #region 与服操作有关
        ServiceHost host = null;
        private volatile static NetHostService _instance = null;
        private static readonly object lockHelper = new object();


        public static NetHostService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new NetHostService();
                }
            }
            return _instance;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="port">监听端口</param>
        public void OpenService()
        {
            string serverIp = IUserContext.GetConfigValue("ServerIp");
            string wtcpPort = IUserContext.GetConfigValue("WTcpPort");  //wcf tcp
            string whttpPort = IUserContext.GetConfigValue("WHttpPort"); //wcf http
            //string xsubPort = IUserContext.GetConfigValue("XSubPort");//zeroMQ Subscriber
            //string xpubPort = IUserContext.GetConfigValue("XPubPort"); //zeroMQ Publisher

            //host = new ServiceHost(typeof(QueueService));

            //BasicHttpBinding bsBinding = new BasicHttpBinding();
            ////bsBinding.Security.Mode = BasicHttpSecurityMode.Transport; 
            ////bsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            ////bsBinding.MaxReceivedMessageSize = 10 * 1024 * 1024; 
            //host.AddServiceEndpoint(typeof(IQueueService), bsBinding, new Uri("http://127.0.0.1:8889/HelloWorldService"));
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            ////true表示wcf服务会生成wsdl，否则无法被java调用
            //smb.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(smb);

            serverIp = "localhost";
            //step 1 
            Uri addr = new Uri("http://" + serverIp + ":" + whttpPort + "/QueueService");
            //step 2
            ServiceHost host = new ServiceHost(typeof(QueueService), addr);

            //step 3
            //必须要new BasicHttpBinding，而不是WSHttpBinding，否则java端wsimport后无法生成IAthorService和AthorService 
            host.AddServiceEndpoint(typeof(IQueueService), new BasicHttpBinding(), "QueueServiceHttp");
            //step 4
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //true表示wcf服务会生成wsdl，否则无法被java调用
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);


            XmlDictionaryReaderQuotas rQuotas = new XmlDictionaryReaderQuotas();
            rQuotas.MaxStringContentLength = int.MaxValue;
            rQuotas.MaxDepth = 32;
            rQuotas.MaxArrayLength = int.MaxValue;


            NetTcpBinding tcpBinding = new NetTcpBinding();
            tcpBinding.Security.Mode = SecurityMode.None;
            //tcpBinding.TransferMode = TransferMode.Streamed;
            tcpBinding.MaxReceivedMessageSize = int.MaxValue;
            tcpBinding.MaxBufferPoolSize = int.MaxValue;
            tcpBinding.MaxBufferSize = int.MaxValue;
            tcpBinding.ReaderQuotas = rQuotas;
            tcpBinding.MaxConnections = 1000;

            host.AddServiceEndpoint(typeof(IQueueService), tcpBinding, "net.tcp://" + serverIp + ":" + wtcpPort + "/QueueServicePoint/");

            host.Opened += host_Opened;
            host.Closed += host_Closed;
            try
            {
                if (host.State != CommunicationState.Opening)
                    host.Open();

                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "主机服务启动完成...");
            }
            catch (Exception ex)
            {
                //LoggerHelper.CreateInstance().Error(typeof(MainFrame), ex.Message, ex);
                host.Abort();
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "主服务启动失败...");
            }
        }

        void host_Closed(object sender, EventArgs e)
        {
            LoggerHelper.CreateInstance().Error(typeof(MainFrame), "服务已关闭。", null);
        }

        void host_Opened(object sender, EventArgs e)
        {
            LoggerHelper.CreateInstance().Error(typeof(MainFrame), "服务已启动。", null);
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseService()
        {
            try
            {
                if (host != null)
                {
                    host.Close();
                    host.Opened -= host_Opened;
                    host.Closed -= host_Closed;
                }
            }
            catch (Exception ex)
            {
                host.Abort();
            }
        }

        #endregion
    }
}
