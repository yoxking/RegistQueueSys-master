using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public enum FontColor
    {
        Red = 0,
        Green = 1,
        Yellow = 2
    }

    public enum ShowMode
    {
        Stop = 0,
        ToLeft = 1,
        Flash = 2,
        Inverse = 3
    }

    public enum ShowType
    {
        Normal = 0,
        Inverse = 1
    }

    public enum ShowOE
    {
        Normal = 0,
        Inverse = 1
    }

    public enum ShowAddr
    {
        Hidden = 0,
        Visible = 1
    }

    public class Pdc102LedDisplay
    {
        private bool isOpend;
        private LederSerialPort sp;
        private string baudRate;
        private string dataBits;
        private string stopBits;
        private string parity;
        private string handShak;

        public Pdc102LedDisplay(string baudRate = "9600", string dataBits = "8", string stopBits = "One", string parity = "None", string handShak = "None")
        {
            isOpend = false;
            sp = new LederSerialPort();
            sp.OpenEvent += new SerialPortEventHandler(LedOpenComEvent);
            sp.CloseEvent += new SerialPortEventHandler(LedCloseComEvent);
            //myCom.ReceiveDataEvent += new SerialPortEventHandler(LedReceiveDataEvent);

            this.baudRate = baudRate;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
            this.parity = parity;
            this.handShak = handShak;
        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LedOpenComEvent(Object sender, SerialPortEventArgs e)
        {
            isOpend = e.isOpend;
        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LedCloseComEvent(Object sender, SerialPortEventArgs e)
        {
            if (!e.isOpend) //close successfully
            {
                isOpend = false;
            }
        }

        /// <summary>
        /// Display received data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LedReceiveDataEvent(Object sender, SerialPortEventArgs e)
        {

            //string s=Encoding.Default.GetString(e.receivedBytes);
            //string h=ConvertUtil.Bytes2Hex(e.receivedBytes);

        }

        public bool Open(string sComPort)
        {
            try
            {
                sp.Open(sComPort, this.baudRate, this.dataBits, this.stopBits, this.parity, this.handShak);

                return isOpend;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void Close()
        {
            sp.Close();
        }

        public bool SendData(int iCardNum, string sShowText, int iShowSpeed = 1, int iShowSec = 0, FontColor eFontColor = FontColor.Red, int iFontSize = 16)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (Char c in sShowText)
                {
                    sb.Append("" + c + (int)eFontColor);
                }
                sShowText = sb.ToString();

                string DataHeader = "A090";
                string DataAddress = Convert.ToString(iCardNum, 16).PadLeft(2, '0');
                string DataInstr = "A5";    //A1(显示开机信息),A2(设置开机信息),A5(即时显示)
                string DataText = System.Text.RegularExpressions.Regex.Replace(ByteConvertUtil.String2Hex(sShowText), "-", "");

                byte[] DataByte = ByteConvertUtil.Hex2Bytes(DataText);
                string DataLength = Convert.ToString(DataByte.Length, 16).PadLeft(4, '0');

                string FormatStr = DataAddress + DataInstr + DataLength + DataText;
                string CheckCode = ByteConvertUtil.HexStrXor(DataAddress + DataInstr + DataLength + DataText);

                FormatStr = DataHeader + FormatStr + CheckCode;
                FormatStr = System.Text.RegularExpressions.Regex.Replace(FormatStr, @"(\w{2})", "$1-").Trim('-');

                if (isOpend)
                {

                    Byte[] datas = ByteConvertUtil.Hex2Bytes(FormatStr);
                    sp.Send(datas);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
