using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace EntFrm.TicketConsole.MPublicUtils
{

    #region 辅助定义
    /// <summary>
    /// 即插即用设备信息结构
    /// </summary>
    public struct PnPEntityInfo
    {
        public String PNPDeviceID;  // 设备ID
        public String Name; // 设备名称
        public String Description;  // 设备描述
        public String Service;  // 服务
        public String Status;   // 设备状态
        public UInt16 VendorID; // 供应商标识
        public UInt16 ProductID;// 产品编号 
        public Guid ClassGuid;  // 设备安装类GUID
    }

    //打印机状态 
    [FlagsAttribute]
    internal enum PrinterStatus
    {
        PRINTER_STATUS_BUSY = 0x00000200,
        PRINTER_STATUS_DOOR_OPEN = 0x00400000,
        PRINTER_STATUS_ERROR = 0x00000002,
        PRINTER_STATUS_INITIALIZING = 0x00008000,
        PRINTER_STATUS_IO_ACTIVE = 0x00000100,
        PRINTER_STATUS_MANUAL_FEED = 0x00000020,
        PRINTER_STATUS_NO_TONER = 0x00040000,
        PRINTER_STATUS_NOT_AVAILABLE = 0x00001000,
        PRINTER_STATUS_OFFLINE = 0x00000080,
        PRINTER_STATUS_OUT_OF_MEMORY = 0x00200000,
        PRINTER_STATUS_OUTPUT_BIN_FULL = 0x00000800,
        PRINTER_STATUS_PAGE_PUNT = 0x00080000,
        PRINTER_STATUS_PAPER_JAM = 0x00000008,
        PRINTER_STATUS_PAPER_OUT = 0x00000010,
        PRINTER_STATUS_PAPER_PROBLEM = 0x00000040,
        PRINTER_STATUS_PAUSED = 0x00000001,
        PRINTER_STATUS_PENDING_DELETION = 0x00000004,
        PRINTER_STATUS_PRINTING = 0x00000400,
        PRINTER_STATUS_PROCESSING = 0x00004000,
        PRINTER_STATUS_TONER_LOW = 0x00020000,
        PRINTER_STATUS_USER_INTERVENTION = 0x00100000,
        PRINTER_STATUS_WAITING = 0x20000000,
        PRINTER_STATUS_WARMING_UP = 0x00010000
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct structPrinterDefaults
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public String pDatatype;
        public IntPtr pDevMode;
        [MarshalAs(UnmanagedType.I4)]
        public int DesiredAccess;
    };

    //Nested Types
    [StructLayout(LayoutKind.Sequential)]
    public class DOCINFOA
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDataType;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PRINTER_INFO_2
    {
        public string pServerName;
        public string pPrinterName;
        public string pShareName;
        public string pPortName;
        public string pDriverName;
        public string pComment;
        public string pLocation;
        public IntPtr pDevMode;
        public string pSepFile;
        public string pPrintProcessor;
        public string pDatatype;
        public string pParameters;
        public IntPtr pSecurityDescriptor;
        public uint Attributes;
        public uint Priority;
        public uint DefaultPriority;
        public uint StartTime;
        public uint UntilTime;
        public uint Status;
        public uint cJobs;
        public uint AveragePPM;
    }

    #endregion

    /// <summary>
    /// 打印机状态
    /// </summary>
    public enum ePrinterState
    {
        UnknownStatus = -1,
        Ready = 0,
        Paused = 1,
        Error = 2,
        PendingDeletion = 4,
        PaperJam = 8,
        NoPaper = 0x10,
        ManualFeedPaper = 0x20,
        PaperProblem = 0x40,
        OffLine = 0x80,
        Transmitting = 0x100,
        Busy = 0x200,
        Printing = 0x400,
        OutputBinFull = 0x800,
        NotAvailable = 0x1000,
        Processing = 0x4000,
        Initializing = 0x8000,
        /// <summary>
        /// 热机中
        /// </summary>
        WarmingUp = 0x10000,
        /// <summary>
        /// 墨粉不足
        /// </summary>
        TonerLow = 0x20000,
        /// <summary>
        /// 无墨粉
        /// </summary>
        NoToner = 0x40000,
        /// <summary>
        /// 当前页无法打印
        /// </summary>
        PagePunt = 0x80000,
        /// <summary>
        /// 需要用户干预
        /// </summary>
        UserIntervention = 0x100000,
        OutOfMemory = 0x200000,
        DoorIsOpen = 0x400000,
        Waiting = 0x20000000,
    }

    /// <summary>
    /// 打印的文件类型
    /// </summary>
    public enum ePrintFileType
    {
        Invalid = -1,
        TextFile = 0,
        Word = 1,
        Excel = 2
    }

    /// <summary>
    /// 打印类
    /// 使用方法:
    /// GetPrinterList 获取所有可用打印机名称列表
    /// CheckPrinterValid 检查某个打印机是否可用.
    /// GetPrinterStatus 获取打印机状态
    /// PrintFile 使用指定打印机打印文件.
    /// PrintFileBySystem 使用指定打印机打印文件,调用操作系统默认程序打印
    /// </summary>
    public class PrinterHelper
    {

        #region 对外接口

        /// <summary>
        /// 获取本机安装的所有打印机
        /// </summary>
        /// <returns></returns>
        public static List<String> GetPrinterList()
        {
            List<String> ret = new List<String>();
            if (PrinterSettings.InstalledPrinters.Count < 1)
            {
                return ret;
            }

            foreach (String printername in PrinterSettings.InstalledPrinters)
            {
                ret.Add(printername);
            }
            return ret;
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="PNPDeviceID"></param>
        /// <returns></returns>
        public static List<PnPEntityInfo> GetUSBDeviceList()
        {
            List<PnPEntityInfo> UsbDevices = new List<PnPEntityInfo>();
            // 获取USB控制器及其相关联的设备实体
            ManagementObjectCollection col = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (col == null || col.Count < 1)
            {
                return null;
            }

            foreach (ManagementObject dev in col)
            {
                // 获取设备实体的DeviceID
                String Dependent = dev["Dependent"] as String;

                PnPEntityInfo Element = new PnPEntityInfo();
                if (dev["PNPDeviceID"] == null)
                {
                    continue;
                }
                Element.PNPDeviceID = dev["PNPDeviceID"] as String;  // 设备ID
                Element.Name = dev["Name"] as String;// 设备名称
                Element.Description = dev["Description"] as String;  // 设备描述
                Element.Service = dev["Service"] as String;  // 服务
                Element.Status = dev["Status"] as String;// 设备状态 
                Element.ClassGuid = new Guid(dev["ClassGuid"] as String);// 设备安装类GUID
                UsbDevices.Add(Element);
            }

            return UsbDevices;
        }

        /// <summary>
        /// 检查打印机是否可用
        /// </summary>
        /// <param name="printername"></param>
        /// <returns></returns>
        public static Boolean CheckPrinterValid(String printername)
        {
            if (printername.Length < 1)
            {
                return false;
            }

            ePrinterState state = GetPrinterStatus(printername);
            if (state == ePrinterState.Ready || state == ePrinterState.Busy)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取打印机状态
        /// </summary>
        /// <param name="PrinterName"></param>
        /// <returns></returns>
        public static ePrinterState GetPrinterStatus(string PrinterName)
        {
            int intValue = GetPrinterStatusInt(PrinterName);
            ePrinterState ret = (ePrinterState)intValue;
            return ret;
            //string strRet = string.Empty;
            //switch (intValue)
            //{
            //    case 0:
            //        strRet = "准备就绪（Ready）";
            //        break;
            //    case 0x00000200:
            //        strRet = "忙(Busy）";
            //        break;
            //    case 0x00400000:
            //        strRet = "被打开（Printer Door Open）";
            //        break;
            //    case 0x00000002:
            //        strRet = "错误(Printer Error）";
            //        break;
            //    case 0x0008000:
            //        strRet = "初始化(Initializing）";
            //        break;
            //    case 0x00000100:
            //        strRet = "正在输入,输出（I/O Active）";
            //        break;
            //    case 0x00000020:
            //        strRet = "手工送纸（Manual Feed）";
            //        break;
            //    case 0x00040000:
            //        strRet = "无墨粉（No Toner）";
            //        break;
            //    case 0x00001000:
            //        strRet = "不可用（Not Available）";
            //        break;
            //    case 0x00000080:
            //        strRet = "脱机（Off Line）";
            //        break;
            //    case 0x00200000:
            //        strRet = "内存溢出（Out of Memory）";
            //        break;
            //    case 0x00000800:
            //        strRet = "输出口已满（Output Bin Full）";
            //        break;
            //    case 0x00080000:
            //        strRet = "当前页无法打印（Page Punt）";
            //        break;
            //    case 0x00000008:
            //        strRet = "塞纸（Paper Jam）";
            //        break;
            //    case 0x00000010:
            //        strRet = "打印纸用完（Paper Out）";
            //        break;
            //    case 0x00000040:
            //        strRet = "纸张问题（Page Problem）";
            //        break;
            //    case 0x00000001:
            //        strRet = "暂停（Paused）";
            //        break;
            //    case 0x00000004:
            //        strRet = "正在删除（Pending Deletion）";
            //        break;
            //    case 0x00000400:
            //        strRet = "正在打印（Printing）";
            //        break;
            //    case 0x00004000:
            //        strRet = "正在处理（Processing）";
            //        break;
            //    case 0x00020000:
            //        strRet = "墨粉不足（Toner Low）";
            //        break;
            //    case 0x00100000:
            //        strRet = "需要用户干预（User Intervention）";
            //        break;
            //    case 0x20000000:
            //        strRet = "等待（Waiting）";
            //        break;
            //    case 0x00010000:
            //        strRet = "热机中（Warming Up）";
            //        break;
            //    default:
            //        strRet = "未知状态（Unknown Status）";
            //        break;
            //} 
        }


        /// <summary>
        /// 调用COM接口打印文件
        /// </summary>
        /// <param name="printername"></param>
        /// <param name="filename"></param>
        /// <param name="printcopycount">打印的份数</param>
        /// <returns></returns>
        public static Boolean PrintFile(String printername, String filename, Int32 printcopycount)
        {
            Boolean bl = FileProcessor.FileExist(filename);
            if (!bl)
            {
                return false;
            }

            ePrintFileType filetype = GetFileType(filename);

            //判断是否为 txt 或word 或 Excel.
            //如果都不是,禁止打印 
            if (filetype == ePrintFileType.Invalid)
            {
                return false;
            }

            //先保存默认的打印机            //默认打印机 不是 指定打印机,需要修改默认打印机
            String defaultPrinter = GetDefaultPrinter();
            Boolean needchangeprinter = !(defaultPrinter.Equals(printername, StringComparison.OrdinalIgnoreCase));
            //将指定的打印机设为默认打印机
            if (needchangeprinter)
            {
                bl = SetDefaultPrinter(printername);
            }

            bl = false;
            if (filetype == ePrintFileType.Excel)
            {
                //bl = PrintExcel(printername, filename, printcopycount);
            }
            else
            {
                bl = PrintWord_Text(printername, filename, printcopycount);
            }


            //将默认打印机还原
            if (needchangeprinter)
            {
                SetDefaultPrinter(defaultPrinter);
            }

            return bl;
        }


        /// <summary>
        /// 使用操作系统命令执行打印
        /// </summary>
        /// <param name="printername"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean PrintFileBySystem(String printername, String filename)
        {
            Boolean bl = FileProcessor.FileExist(filename);
            if (!bl)
            {
                return false;
            }

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //不显示 调用程序窗口,但是对于某些应用无效
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //采用操作系统自动识别的模式
            p.StartInfo.UseShellExecute = true;

            //要打印的文件路径
            p.StartInfo.FileName = filename;

            //指定执行的动作，是打印，即print，打开是 open
            p.StartInfo.Verb = "print";

            //默认打印机 不是 指定打印机,需要修改默认打印机
            String defaultPrinter = GetDefaultPrinter();
            Boolean needchangeprinter = !(defaultPrinter.Equals(printername, StringComparison.OrdinalIgnoreCase));

            //将指定的打印机设为默认打印机
            if (needchangeprinter)
            {
                bl = SetDefaultPrinter(printername);
            }

            //开始打印
            p.Start();

            p.WaitForExit(15000);
            p.Dispose();

            p = null;

            //将默认打印机还原
            if (needchangeprinter)
            {
                SetDefaultPrinter(defaultPrinter);
            }
            return false;
        }

        #endregion

        #region 内部实现

        private static List<String> m_SuffixNameList = new List<string> { "txt", "doc", "docx", "xls", "xlsx", "csv" };

        /// <summary>
        /// 根据文件后缀名获取文件类型
        /// </summary>
        /// <param name="suffixname"></param>
        /// <returns></returns>
        private static ePrintFileType GetFileType(String filename)
        {
            String sfxname = FileProcessor.GetFileSuffixName(filename);
            sfxname = sfxname.ToLower();

            Int32 idx = m_SuffixNameList.IndexOf(sfxname);
            if (idx < 0)
            {
                return ePrintFileType.Invalid;
            }

            if (idx == 0)
            {
                return ePrintFileType.TextFile;
            }
            if (idx < 3)
            {
                return ePrintFileType.Word;
            }
            return ePrintFileType.Excel;
        }

        /// <summary>
        /// 打印word,Text
        /// </summary>
        /// <param name="printername"></param>
        /// <param name="filename"></param>
        /// <param name="copycount">打印的份数</param>
        /// <returns></returns>
        private static Boolean PrintWord_Text(String printername, String filename, Int32 copycount)
        {
            //object oMissing = Missing.Value;

            ////定义WORD Application相关
            //MSWord._Application appWord = new MSWord.Application();

            ////WORD程序不可见
            //appWord.Visible = false;
            ////不弹出警告框
            //appWord.DisplayAlerts = MSWord.WdAlertLevel.wdAlertsNone;

            //object oTrue = true;
            //object oFalse = false;

            ////打开要打印的文件
            //object wordFile = filename;
            //MSWord._Document doc = appWord.Documents.Open(
            //                    ref wordFile,
            //                    ref oMissing,
            //                    ref oTrue,
            //                    ref oFalse,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing,
            //                    ref oMissing);

            ////设置指定的打印机
            ////appWord.ActivePrinter = printername;

            ////true后台打印,false前台打印。
            ////必须false,否则后台打印时,尚未打印,就调用了后续的Close,造成打印任务无法执行
            //object printbackgroud = false;
            //object copycnt = copycount;

            //doc.PrintOut(ref printbackgroud,
            //    ref oFalse,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref copycnt,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing,
            //    ref oMissing
            //    );

            //object doNotSaveChanges = MSWord.WdSaveOptions.wdDoNotSaveChanges;

            ////打印完关闭WORD文件
            //doc.Close(ref doNotSaveChanges, ref oMissing, ref oMissing);

            ////退出WORD程序
            //appWord.Quit(ref doNotSaveChanges, ref oMissing, ref oMissing);

            //doc = null;
            //appWord = null;
            return true;
        }

        ///// <summary>
        ///// 打印Excel文件
        ///// </summary>
        ///// <param name="printername"></param>
        ///// <param name="filename"></param>
        ///// <param name="copycount"></param>
        ///// <returns></returns>
        //private static Boolean PrintExcel(String printername, String filename, Int32 copycount)
        //{
        //    object oMissing = Missing.Value;

        //    //定义WORD Application相关
        //    MSExcel._Application appxls = new MSExcel.Application();

        //    //WORD程序不可见
        //    appxls.Visible = false;
        //    //不弹出警告框
        //    appxls.DisplayAlerts = false;

        //    object oTrue = true;
        //    object oFalse = false;

        //    //打开要打印的文件 
        //    MSExcel._Workbook book = appxls.Workbooks.Open(filename);
        //    //Excel 规定,首个表格的下标为1
        //    MSExcel._Worksheet sheet = (MSExcel._Worksheet)book.Worksheets[1];

        //    sheet.Activate();
        //    sheet.PageSetup.PrintGridlines = true;

        //    object printbackgroud = false;
        //    object copycnt = copycount;

        //    sheet.PrintOut(oMissing, oMissing, copycnt);
        //    //for (Int32 sheetidx = 1; sheetidx <= book.Worksheets.Count; ++sheetidx)
        //    //{
        //    //    sheet = (MSExcel._Worksheet)book.Worksheets[sheetidx];
        //    //    if (sheet == null)
        //    //    {
        //    //        break;
        //    //    }
        //    //    sheet.PrintOut();
        //    //}

        //    sheet = null;
        //    book.Saved = false;
        //    object saved = false;
        //    book.Close(saved);
        //    appxls.Quit();

        //    book = null;
        //    appxls = null;

        //    //操作过Excel对象后,必须强制垃圾回收.否则无法退出Exccel进程
        //    CommonCompute.GCCollect();

        //    return true;
        //}

        #endregion

        #region 获取设置默认打印机

        /// <summary>
        /// 设置默认打印机
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        [DllImport("Winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);


        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int pcchBuffer);

        //static string GetDefaultPrinter()
        //{
        //    const int ERROR_FILE_NOT_FOUND = 2;
        //    const int ERROR_INSUFFICIENT_BUFFER = 122;
        //    int pcchBuffer = 0;
        //    if (GetDefaultPrinter(null, ref pcchBuffer))
        //    {
        //        return null;
        //    }

        //    int lastWin32Error = Marshal.GetLastWin32Error();
        //    if (lastWin32Error == ERROR_INSUFFICIENT_BUFFER)
        //    {
        //        StringBuilder pszBuffer = new StringBuilder(pcchBuffer);
        //        if (GetDefaultPrinter(pszBuffer, ref pcchBuffer))
        //        {
        //            return pszBuffer.ToString();
        //        }
        //        lastWin32Error = Marshal.GetLastWin32Error();
        //    }

        //    if (lastWin32Error == ERROR_FILE_NOT_FOUND)
        //    {
        //        return null;
        //    }

        //}

        /// <summary>
        /// 获取默认打印机
        /// </summary>
        /// <returns></returns>
        public static String GetDefaultPrinter()
        {
            StringBuilder strbuilder = new StringBuilder(2048);
            Int32 buflen = strbuilder.Capacity;

            GetDefaultPrinter(strbuilder, ref buflen);
            String ret = strbuilder.ToString();
            return ret;
        }
        #endregion

        #region 系统API

        // [Methods]
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        internal static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        internal static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        internal static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "GetPrinterA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern bool GetPrinter(IntPtr hPrinter, int dwLevel, IntPtr pPrinter, int dwBuf, out int dwNeeded);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);


        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        internal static extern bool StartPagePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        internal static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        /// <summary>
        /// 获取打印机状态字
        /// </summary>
        /// <param name="PrinterName"></param>
        /// <returns></returns>
        internal static int GetPrinterStatusInt(string PrinterName)
        {
            int intRet = 0;
            IntPtr hPrinter;
            //structPrinterDefaults defaults = new structPrinterDefaults();

            Boolean bl = OpenPrinter(PrinterName, out hPrinter, IntPtr.Zero);
            if (!bl)
            {
                return (int)ePrinterState.NotAvailable;
            }

            int cbNeeded = 0;
            bl = GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out cbNeeded);
            //此处不能判断bl.应该只判断 cbNeeded
            if (cbNeeded < 1)
            {
                ClosePrinter(hPrinter);
                return (int)ePrinterState.OutputBinFull;
            }

            IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
            bl = GetPrinter(hPrinter, 2, pAddr, cbNeeded, out cbNeeded);
            if (!bl)
            {
                ClosePrinter(hPrinter);
                return (int)ePrinterState.NotAvailable;
            }

            PRINTER_INFO_2 Info2 = new PRINTER_INFO_2();
            Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));
            intRet = System.Convert.ToInt32(Info2.Status);
            Marshal.FreeHGlobal(pAddr);
            ClosePrinter(hPrinter);
            return intRet;
        }

        #endregion
    }
}
