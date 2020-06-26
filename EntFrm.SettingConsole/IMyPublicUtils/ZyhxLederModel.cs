using System;
using System.Runtime.InteropServices;

namespace EntFrm.SettingConsole
{
    public class ZyhxLederModel
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

    }
}