using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public delegate void SerialPortEventHandler(Object sender, SerialPortEventArgs e);

    public class SerialPortEventArgs : EventArgs
    {
        public bool isOpend = false;
        public string receivedData = null;
    }

    public interface ISerialPortBase
    { 
        bool Send(string sData);

        bool Send(Byte[] bytes);

        bool Open(string portName, String baudRate);

        bool Open(string portName, String baudRate,string dataBits, string stopBits, string parity,string handshake);
        
        void Close();
         
    }
}


