using EntFrm.Framework.Utility;
using System.Collections.Generic;

namespace EntFrm.SettingConsole
{
    public enum UpdateType
    {
        Add,
        Upt
    }
    public class IPublicConsts
    {
        public const string PFUNCT_SVCMODULE_SERVICE = "20130505";
        public const string PFUNCT_SVCMODULE_COUNTER = "20130504";
        public const string PFUNCT_SVCMODULE_STAFFS = "16092412";
        public const string PFUNCT_SVCMODULE_WORKFLOW = "20130302";
        public const string PFUNCT_SVCMODULE_UINTERFACE = "14024762";
        public const string PFUNCT_SVCMODULE_WORKVOICE = "14020415";
        public const string PFUNCT_SVCMODULE_WORKTTS = "14020415";
        public const string PFUNCT_SVCMODULE_LEDDISPLAY = "14011040";
        public const string PFUNCT_SVCMODULE_SYSPARAMS = "16092654";
        public const string PFUNCT_SVCMODULE_PERIPHERAL = "13064024";
        public const string PFUNCT_SVCMODULE_PRINTSTYLE = "20130506";

        /*        
          System,Others,Shutdown,Background,ButtonTemplate,LED,Dispenser,Evaluator,IdCard,Printer,DBMaintenance,Permission,SMS,          
         */
        public const string TYPE_SYSTEM = "System";
        public const string TYPE_OTHERS = "Others";
        public const string TYPE_SHUTDOWN = "Shutdown";
        public const string TYPE_BACKGROUND = "Background";
        public const string TYPE_BUTTONTEMPLATE = "ButtonTemplate";
        public const string TYPE_LED = "LED";
        public const string TYPE_DISPENSER = "Dispenser";
        public const string TYPE_EVALUATOR = "Evaluator";
        public const string TYPE_IDCARD = "IdCard";
        public const string TYPE_PRINTER = "Printer";
        public const string TYPE_DBMAINTENANCE = "DBMaintenance";
        public const string TYPE_PERMISSION = "Permission";
        public const string TYPE_SMS = "SMS";

        public const string READER_MODEL = "IdRdModel";
        public const string READER_SPORT = "IdRdSPort";
        public const string READER_BRATE = "IdRdBaudrate";

        public const string DEF_CALLVOICEENABLE = "CallVoiceEnable";
        public const string DEF_CALLVOICEFORMAT = "CallVoiceFormat";
        public const string DEF_CALLVOICEVOLUME = "CallVoiceVolume";
        public const string DEF_CALLVOICERATE = "CallVoiceRate";
        public const string DEF_CALLVOICESTYLE = "CallVoiceStyle";
        public const string DEF_CALLINTERVAL = "CallInterval";

        public const string DEF_DBVERSION = "DbVersion";
        public const string DEF_DISPENSERTITLE = "DispenserTitle";
        public const string DEF_ENABLEPRIVILEGEBTN = "EnablePrivilegedBtn";
        public const string DEF_EVALDEFAULTVALUE = "EvalDefaultValue";
        public const string DEF_EVALUATORLANGUAGE = "EvaluatorLanguage";
        public const string DEF_EVALUATORSERIALPORT = "EvaluatorSerialPort";
        public const string DEF_HWCALLERPROTOCOL = "HWCallerProtocol";
        public const string DEF_IDCARDSERVICE = "IdCardService";
        public const string DEF_IDCARDTICKETSCOUNT = "IdCardTicketsCount";
        public const string DEF_IDCARDTICKETSLIMIT = "IdCardTicketsLimit";
        public const string DEF_INPUTWNDSERVICE = "InputWndService";
        public const string DEF_INPUTWNDTIPS = "InputWndTips";
        public const string DEF_INPUTWNDTITLE = "InputWndTitle";
        public const string DEF_INTERFACEALARM = "InterfaceAlarm";
        public const string DEF_LASTRUNTIME = "LastRunTime";
        public const string DEF_LASTSTATTIME = "LastStatTime";
        public const string DEF_LEDALIGNMENT = "LedAlignment";
        public const string DEF_LEDCOUNTERNOLEN = "LedCounterNoLen";
        public const string DEF_LEDLINESPACING = "LedLineSpacing";
        public const string DEF_LEDSENDDELAY = "LEDSendDelay";
        public const string DEF_LOGINWITHSTAFFID = "LoginWithStaffID";
        public const string DEF_MULTICALLERLOGIN = "MultiCallerLogin";
        public const string DEF_MULTITICKETCALL = "MultiTicketCall";
        public const string DEF_ONLYSHOWSVCWAITNUM = "OnlyShowSvcWaitNum";
        public const string DEF_PLAYCONTERALIASASDIGIT = "PlayCounterAliasAsDigit";
        public const string DEF_PLAYDINGDONG = "PlayDingDong";
        public const string DEF_PLAYTICKETASDIGIT = "PlayTicketAsDigit";
        public const string DEF_PRINTERPAPERWIDTH = "PrinterPaperWidth";
        public const string DEF_PRINTERTYPE = "PrinterType";
        public const string DEF_QUITPASSWORD = "QuitPassword";
        public const string DEF_REGCODE = "RegCode";
        public const string DEF_SATURDAYSHUTDOWN = "SaturdayShutdown";
        public const string DEF_SHOWINPUTWND = "ShowInputWnd";
        public const string DEF_SHOWWAITINGNUM = "ShowWaitingNum";
        public const string DEF_SHUTDOWNTIME = "ShutdownTime";
        public const string DEF_SINGLESERVICELIMIT = "SingleServiceLimit";
        public const string DEF_SMSBAUDRATE = "SMSBaudrate";
        public const string DEF_SMSCOUNTRYCODE = "SMSCountryCode";
        public const string DEF_SMSFORMAT = "SMSFormat";
        public const string DEF_SMSREMINPEOPLENUM = "SMSRemindPeopleNum";
        public const string DEF_SMSREMINDTIME = "SMSRemindTime";
        public const string DEF_SMSREMINDWAY = "SMSRemindWay";
        public const string DEF_SMSSERIALPORT = "SMSSerialPort";
        public const string DEF_SMSSN = "SMSSn";
        public const string DEF_SMSUSEMODEN = "SMSUseModem";
        public const string DEF_SMSUSEREMIND = "SMSUseRemind";
        public const string DEF_SOUNDALARM = "SoundAlarm";
        public const string DEF_SOUNDPLAYTIMES = "SoundPlayTimes";
        public const string DEF_STATRETAINDAYS = "StatRetainDays";
        public const string DEF_SUNDAYSHUTDOWN = "SundayShutdown";
        public const string DEF_SUSPENDPRINT = "SuspendPrint";
        public const string DEF_SWIPECARD = "SwipeCard";
        public const string DEF_SWIPEIDCARD = "SwipeIdCard";
        public const string DEF_TCCHECKDELAY = "TcCheckDelay";
        public const string DEF_TCINPUTTITLE = "TcInputTitle";
        public const string DEF_TCNEEDINPUT = "TcNeedInput";
        public const string DEF_TICKETPRINTCOUNT = "TicketPrintCount";
        public const string DEF_TICKETRETAINDAYS = "TicketRetainDays";
        public const string DEF_USEBGMUSIC = "UseBgMusic";
        public const string DEF_USEFIXEDCOUNTERLEN = "UseFixedCounterLen";
        public const string DEF_USENETEVALUATOR = "UseNetEvaluator";
        public const string DEF_USEQUITPASSWORD = "UseQuitPassword";
        public const string DEF_USETTS = "UseTTS";
        public const string DEF_WAITTITLE = "WaitTime";
        public const string DEF_TICKETMESSAGE = "TicketMessage";
        public const string DEF_EVATITLE = "EvaTitle";
        public const string DEF_EVABULLETIN = "EvaBulletin";
        public const string DEF_EVAIMAGES = "EvaImages";
        public const string DEF_SLIDETITLE = "SlideTitle";
        public const string DEF_SLIDEBULLETIN = "SlideBulletin";
        public const string DEF_SLIDEHTML = "SlideHtml";
        public const string DEF_SLIDEIMAGES = "SlideImages";
        public const string DEF_BRANCHNO = "BranchNo";

        public static List<ItemObject> GetPublicVariables()
        {
            List<ItemObject> variables = new List<ItemObject>();

            variables.Add(new ItemObject("[票号]", "[TicketNo]"));
            variables.Add(new ItemObject("[科室名称]", "[CounterName]"));
            variables.Add(new ItemObject("[科室别名]", "[CounterAlias]"));
            variables.Add(new ItemObject("[科室等候人数]", "[CounterWaiterNumber]")); 
            variables.Add(new ItemObject("[业务名称]", "[ServiceName]"));
            variables.Add(new ItemObject("[业务别名]", "[ServiceAlias]"));
            variables.Add(new ItemObject("[业务等候人数]", "[ServiceWaiterNumber]")); 
            variables.Add(new ItemObject("[医生名称]", "[StafferName]"));
            variables.Add(new ItemObject("[医生职称]", "[StaffRank]"));
            variables.Add(new ItemObject("[医生等候人数]", "[StaffWaiterNumber]")); 
            variables.Add(new ItemObject("[下一票号]", "[NextTicketNo]"));
            variables.Add(new ItemObject("[姓名]", "[FullName]"));
            variables.Add(new ItemObject("[性别]", "[UserSex]"));
            variables.Add(new ItemObject("[身份证号]", "[IdNumber]"));
            variables.Add(new ItemObject("[卡号]", "[CardNumber]"));
            variables.Add(new ItemObject("[电话]", "[Telephone]"));
            variables.Add(new ItemObject("[其他]", "[Summary]"));
            variables.Add(new ItemObject("[二维码]", "[Qrcode]"));
            variables.Add(new ItemObject("[年-月-日 时:分:秒]", "[yyyy-MM-dd-HH:mm:ss]"));
            variables.Add(new ItemObject("[年/月/日 时:分:秒]", "[yyyy/MM/dd-HH:mm:ss]"));
            variables.Add(new ItemObject("[24小时制时间]", "[HH:mm:ss]"));
            variables.Add(new ItemObject("[12小时制时间]", "[hh:mm:ss]"));

            return variables;
        }

        public static List<ItemObject> GetPublicShowPosStyle()
        {
            List<ItemObject> styles = new List<ItemObject>();

            styles.Add(new ItemObject("在按钮上边显示", "Top"));
            styles.Add(new ItemObject("在按钮下边显示", "Bottom"));
            styles.Add(new ItemObject("在按钮左边显示", "Left"));
            styles.Add(new ItemObject("在按钮右边显示", "Right"));

            return styles;
        }

        public static List<ItemObject> GetPublicCallerProtocol()
        {
            List<ItemObject> protocols = new List<ItemObject>();

            protocols.Add(new ItemObject("中意德信(zyjh)", "ZYJH201"));
            protocols.Add(new ItemObject("永泰(ytjh)", "YTJH201"));
            protocols.Add(new ItemObject("前进者(jh201)", "QJZJH201"));

            return protocols;
        }

        public static List<ItemObject> GetPublicLEDProtocol()
        {
            List<ItemObject> protocols = new List<ItemObject>();

            protocols.Add(new ItemObject("中意德信(zyled)", "LccZyhx"));
            protocols.Add(new ItemObject("永泰(ytled)", "Lc3xFont"));
            protocols.Add(new ItemObject("前进者(Lc3x1)", "Lc3xFont"));
            protocols.Add(new ItemObject("前进者(Lc3x2)", "Lc3xNofont"));
            protocols.Add(new ItemObject("前进者(Lcc1)", "LccChs&Cht"));
            protocols.Add(new ItemObject("前进者(Lcc2)", "LccLowcost"));
            protocols.Add(new ItemObject("前进者(Lcc3)", "LccNofont"));

            return protocols;
        }

        public static List<ItemObject> GetPublicVoiceRate()
        {
            List<ItemObject> rates = new List<ItemObject>();

            rates.Add(new ItemObject("快", 10));
            rates.Add(new ItemObject("中快", 5));
            rates.Add(new ItemObject("中", 0));
            rates.Add(new ItemObject("中慢", -5));
            rates.Add(new ItemObject("慢", -10));

            return rates;
        }

        public static List<ItemObject> GetPublicVoiceVolume()
        {
            List<ItemObject> volumes = new List<ItemObject>();

            volumes.Add(new ItemObject("大", 100));
            volumes.Add(new ItemObject("中", 50));
            volumes.Add(new ItemObject("小", 10));

            return volumes;
        }

        public static List<ItemObject> GetShowDialogs()
        {
            List<ItemObject> dialogs = new List<ItemObject>();

            dialogs.Add(new ItemObject("无", "Null"));
            dialogs.Add(new ItemObject("刷身份证（姓名）", "ScanCardDialog1"));
            dialogs.Add(new ItemObject("刷身份证（票号）", "ScanCardDialog2"));
            dialogs.Add(new ItemObject("刷身份证+手动输入(姓名)", "MyIdCardDialog1"));
            dialogs.Add(new ItemObject("刷身份证+手动输入(票号)", "MyIdCardDialog2"));
            dialogs.Add(new ItemObject("刷身份证+车牌输入(姓名)", "PlateInputDialog1"));
            dialogs.Add(new ItemObject("刷身份证+车牌输入(票号)", "PlateInputDialog2"));
            dialogs.Add(new ItemObject("刷社保卡(票号)", "ScanDecardDialog"));//德卡t6
            dialogs.Add(new ItemObject("扫条形码(票号)", "ScanBarcodeDialog"));
            //dialogs.Add(new ItemObject("全数字输入", "NumBoardDialog"));

            return dialogs;
        }
    }
}
