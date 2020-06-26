using System;
using WebSocketSharp.Server;

namespace EntFrm.DataAdapter.Services
{
    public class WebSocketService
    {
        private volatile static WebSocketService _instance = null;
        private static readonly object lockHelper = new object();
        private WebSocketServer webSocketServer = null;

        public static WebSocketService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new WebSocketService();
                }
            }
            return _instance;
        }

        private WebSocketService() { }

        public void StartWebSocket()
        {
            try
            {
                int stcpPort = int.Parse(IUserContext.GetConfigValue("STcpPort"));
                webSocketServer = new WebSocketServer(stcpPort);
                webSocketServer.AddWebSocketService<WebSocketHandler>("/IQueueService"); 
                webSocketServer.Start();
                if (webSocketServer.IsListening)
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "WebSocket服务启动完成...");
                    //foreach (var path in webSocketServer.WebSocketServices.Paths)
                    //    Console.WriteLine("- {0}", path);
                }
            }
            catch(Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "WebSocket服务启动失败,(" + ex.Message + ")");
            }
        }

        public void StopWebSocket()
        {
            if (webSocketServer != null)
            {
                webSocketServer.Stop();
            }
        }
    }
}
