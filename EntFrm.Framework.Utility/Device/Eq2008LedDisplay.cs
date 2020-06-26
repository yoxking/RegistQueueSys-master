using System;
using System.Runtime.InteropServices;
using static EntFrm.Framework.Utility.Eq2008DataStruct;

namespace EntFrm.Framework.Utility
{
    public class Eq2008LedDisplay
    {
        /************************************************************************/
        /*                         实时发送数据-可并发                           */
        /************************************************************************/
        //实时发送文本
        [DllImport("EQ2013_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_UpdateTextSuper(int CardNum, int x, int y, int iWidth, int iHeight, string strText, ref User_FontSet pFontInfo);

        //实时发送图片句柄
        [DllImport("EQ2013_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_UpdateBmpSuper(int CardNum, int x, int y, int iWidth, int iHeight, IntPtr hBitmap);

        //====================================================================//


        public static int g_iCardNum = 1;      //控制卡地址
        public static int g_iGreen = 0xFF00; //绿色
        public static int g_iYellow = 0xFFFF; //黄色
        public static int g_iRed = 0x00FF; //红色
        public static int g_iProgramIndex = 0;
        public static int g_iProgramCount = 0;

        public static bool ScreenClear(int iCardNum)
        {
            try
            {
                //return User_RealtimeScreenClear(iCardNum);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendDatafun(int iCardNum, string sShowText, int iPosX, int iPosY, int iWidth, int iHeight, int iFontSize = 12, int iFontAlign = 1)
        {
            try
            {
                bool bResult = false;
                g_iCardNum = iCardNum;

                User_FontSet FontInfo = new User_FontSet();

                FontInfo.bFontBold = false;
                FontInfo.bFontItaic = false;
                FontInfo.bFontUnderline = false;
                FontInfo.colorFont = 0xFFFF;
                FontInfo.iFontSize = iFontSize;
                FontInfo.strFontName = "宋体";
                FontInfo.iAlignStyle = iFontAlign;
                FontInfo.iVAlignerStyle = 0;
                FontInfo.iRowSpace = 4;

                bResult = User_UpdateTextSuper(g_iCardNum, iPosX, iPosY, iWidth, iHeight, sShowText, ref FontInfo);
                return bResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
