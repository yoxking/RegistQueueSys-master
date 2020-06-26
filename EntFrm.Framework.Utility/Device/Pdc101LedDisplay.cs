using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace EntFrm.Framework.Utility
{
    public class Pdc101LedDisplay
    {   
        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendDatafun(int comport, int bps, int address, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]string sendData, int color, int showMode, int showSpeed, int showTime);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendDatagen(int comport, int bps, int address, int commandbyte, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]string sendData, int color, int showMode, int showSpeed, int showTime);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int portOpen(string com, int bps, Byte par, byte dbit, byte sbit);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int portClose(int mm_handle);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int picSend(int mm_handle, int address, int commandbyte, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]string sendData, int color);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int planSend(int mm_handle, int address, int plannum, byte[] planData, long planDataSize);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int dataSend(int mm_handle, int address, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]string sendData, int color, int showMode, int showSpeed, int showTime);

        [DllImport(@"dlltpzp.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int dataSendgen(int mm_handle, int address, int commandbyte, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]string sendData, int color, int showMode, int showSpeed, int showTime);

        public int Open(string port,int bps)
        {
            try
            {
                return portOpen(port, bps, (byte)'n', 8, 1);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public int SendData(int mhandler, int address, string sendData, int color, int showMode, int showSpeed, int showTime)
        {
            try
            {
                int result = -1; 

                if (mhandler > 0)
                {
                    result = dataSend(mhandler, address, sendData, color, showMode, showSpeed, showTime);
                }

                return result;
            }
            catch (Exception ex)
            { 
                return -1;
            } 
        }
    }
}
