using EntWeb.MedicConsole.QServicePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml;

namespace EntWeb.MedicConsole.Common
{
    public class IUserContext
    {
        public static string OnExecuteCommand(string methodName, string[] paramList)
        {
            try
            {
                string uri = "net.tcp://" + PublicHelper.GetConfigValue("ServerIp") + ":" + PublicHelper.GetConfigValue("WTcpPort") + " / QueueServicePoint/";
                string sResult = "";
                string sParams = "";

                if (methodName.Length < 1)
                {
                    return "";
                }

                if (paramList != null && paramList.Length > 0)
                {
                    foreach (string param in paramList)
                    {
                        sParams += param + "$";
                    }
                    sParams = sParams.Substring(0, sParams.Length - 1);
                }

                XmlDictionaryReaderQuotas rQuotas = new XmlDictionaryReaderQuotas();
                rQuotas.MaxStringContentLength = int.MaxValue;
                rQuotas.MaxDepth = 32;
                rQuotas.MaxArrayLength = int.MaxValue;

                EndpointAddress address = new EndpointAddress(uri);
                System.ServiceModel.Channels.Binding bindinginstance = null;
                NetTcpBinding ntcp = new NetTcpBinding();
                ntcp.Security.Mode = SecurityMode.None;
                ntcp.MaxReceivedMessageSize = int.MaxValue;
                ntcp.MaxBufferPoolSize = int.MaxValue;
                ntcp.MaxBufferSize = int.MaxValue;
                ntcp.ReaderQuotas = rQuotas;

                bindinginstance = ntcp;

                using (ChannelFactory<IQueueService> channel = new ChannelFactory<IQueueService>(bindinginstance, address))
                {
                    IQueueService instance = channel.CreateChannel();
                    sResult = instance.OnExecuteCommand(methodName, sParams);
                }

                return sResult;
            }
            catch (Exception ex)
            {
                throw new Exception("连接主机服务失败，请检查主机服务和网络是否正常！");
            }
        }

        public static string OnExecuteCommand_Xp(string methodName, string[] paramList)
        {
            try
            {
                string uri = "net.tcp://" + PublicHelper.GetConfigValue("ServerIp") + ":" + PublicHelper.GetConfigValue("WTcpPort") + "/QueueServicePoint/";
                string sResult = "";
                string sParams = "";

                if (methodName.Length < 1)
                {
                    return "";
                }

                if (paramList != null && paramList.Length > 0)
                {
                    foreach (string param in paramList)
                    {
                        sParams += param + "$";
                    }
                    sParams = sParams.Substring(0, sParams.Length - 1);
                }

                XmlDictionaryReaderQuotas rQuotas = new XmlDictionaryReaderQuotas();
                rQuotas.MaxStringContentLength = int.MaxValue;
                rQuotas.MaxDepth = 32;
                rQuotas.MaxArrayLength = int.MaxValue;

                EndpointAddress address = new EndpointAddress(uri);
                System.ServiceModel.Channels.Binding bindinginstance = null;
                NetTcpBinding ntcp = new NetTcpBinding();
                ntcp.Security.Mode = SecurityMode.None;
                ntcp.MaxReceivedMessageSize = int.MaxValue;
                ntcp.MaxBufferPoolSize = int.MaxValue;
                ntcp.MaxBufferSize = int.MaxValue;
                ntcp.ReaderQuotas = rQuotas;

                bindinginstance = ntcp;

                QServicePoint.QueueServiceClient qsc = new QServicePoint.QueueServiceClient(ntcp, address);
                sResult = qsc.OnExecuteCommand(methodName, sParams);

                return sResult;
            }
            catch (Exception ex)
            {
                throw new Exception("连接主机服务失败，请检查主机服务和网络是否正常！");
            }
        }

        public static void OnExecuteCommandAsync_Xp(string methodName, string[] paramList, EventHandler<OnExecuteCommandCompletedEventArgs> myCallback = null)
        {
            try
            {
                string uri = "net.tcp://" + PublicHelper.GetConfigValue("ServerIp") + ":" + PublicHelper.GetConfigValue("WTcpPort") + " /QueueServicePoint/";
                string sParams = "";

                if (methodName.Length < 1)
                {
                    return;
                }

                if (paramList != null && paramList.Length > 0)
                {
                    foreach (string param in paramList)
                    {
                        sParams += param + "$";
                    }
                    sParams = sParams.Substring(0, sParams.Length - 1);
                }

                XmlDictionaryReaderQuotas rQuotas = new XmlDictionaryReaderQuotas();
                rQuotas.MaxStringContentLength = int.MaxValue;
                rQuotas.MaxDepth = 32;
                rQuotas.MaxArrayLength = int.MaxValue;

                EndpointAddress address = new EndpointAddress(uri);
                System.ServiceModel.Channels.Binding bindinginstance = null;
                NetTcpBinding ntcp = new NetTcpBinding();
                ntcp.Security.Mode = SecurityMode.None;
                ntcp.MaxReceivedMessageSize = int.MaxValue;
                ntcp.MaxBufferPoolSize = int.MaxValue;
                ntcp.MaxBufferSize = int.MaxValue;
                ntcp.ReaderQuotas = rQuotas;

                bindinginstance = ntcp;

                QServicePoint.QueueServiceClient qsc = new QServicePoint.QueueServiceClient(ntcp, address);

                qsc.OnExecuteCommandCompleted += new EventHandler<OnExecuteCommandCompletedEventArgs>(myCallback);
                qsc.OnExecuteCommandAsync(methodName, sParams);

            }
            catch (Exception ex)
            {
                throw new Exception("连接主机服务失败，请检查主机服务和网络是否正常！");
            }
        }
    }
}