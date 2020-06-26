using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;

namespace EntFrm.DataAdapter.Services
{
    public class NettyChannelMap
    {
        public static Dictionary<string, ISocketChannel> dict = new Dictionary<String, ISocketChannel>();
        public static void addChannel(String devCode, ISocketChannel socketChannel)
        {
            if (dict != null)
            {
                if (dict.ContainsKey(devCode))
                {
                    dict.Remove(devCode);
                }
                dict.Add(devCode, socketChannel);
            }
        }
        public static IChannel getChannel(String devCode)
        {
            if (dict != null && dict.Count > 0)
            {
                if (dict.ContainsKey(devCode))
                {
                    return dict[devCode];
                }
            }
            return null;
        }
        public static bool containChannel(String devCode)
        {
            if (dict != null && dict.Count > 0)
            {
                return dict.ContainsKey(devCode);
            }
            return false;
        }
        public static void delChannel(ISocketChannel socketChannel)
        {
            try
            {
                if (dict != null && dict.Count > 0)
                {  
                    foreach (KeyValuePair<string, ISocketChannel> entry in dict)
                    {
                        if (entry.Value.Equals(socketChannel))
                        {
                            dict.Remove(entry.Key);
                        }
                    }
                }
            }
            catch(Exception ex)
            { 
            }
        }
    }
}
