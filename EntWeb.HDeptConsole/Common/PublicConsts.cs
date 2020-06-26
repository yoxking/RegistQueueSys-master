using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.HDeptConsole
{
    public class PublicConsts
    {
        public const int WORKTIMETYPE1 = 0;  //休息
        public const int WORKTIMETYPE2 = 1;  //全天
        public const int WORKTIMETYPE3 = 2;  //上午
        public const int WORKTIMETYPE4 = 3;  //下午

        public const int REGISTETYPE1 = 1;   //挂号
        public const int REGISTETYPE2 = 2;   //预约

        public const int REGISTEFROM1 = 1;   // 手动录入
        public const int REGISTEFROM2 = 2;   // 现场挂号
        public const int REGISTEFROM3 = 3;   // 刷卡挂号
        public const int REGISTEFROM4=4;   // 微信预约
        public const int REGISTEFROM5=5;   // 检查申请
        public const int REGISTEFROM6=6;   // 住院部预约

        public const int ROTATYPE1 = 1;   //正常排班
        public const int ROTATYPE2 = 2;   //暂时排班

        public const int PRIORITY_TYPE0 = 0; //普通
        public const int PRIORITY_TYPE1 = 1; //预约优先 
        public const int PRIORITY_TYPE2 = 2;  //过号优先
        public const int PRIORITY_TYPE3 = 3; //军人优先
        public const int PRIORITY_TYPE4 = 4;//离休优先
        public const int PRIORITY_TYPE5 = 5;//幼儿优先
        public const int PRIORITY_TYPE6 = 6; //老人优先
        public const int PRIORITY_TYPE7 = 7; //急诊优先

        public const int PROCSTATE_OUTQUEUE = 10;//未入队
        public const int PROCSTATE_DIAGNOSIS = 11;//初诊
        public const int PROCSTATE_TRIAGE = 12;   //分诊
        public const int PROCSTATE_EXCHANGE = 13;   //转诊
        public const int PROCSTATE_PASSTICKET = 14;   //过号初诊
        public const int PROCSTATE_DELAY = 15;   //延迟
        public const int PROCSTATE_REDIAGNOSIS = 16;   //复诊
        public const int PROCSTATE_WAITING = 20;   //综合区域-等候中
        public const int PROCSTATE_WAITAREA1 = 21;   //等候区域1-等候中
        public const int PROCSTATE_WAITAREA2 = 22;   //等候区域2-等候中
        public const int PROCSTATE_WAITAREA3 = 23;   //等候区域3-等候中
        public const int PROCSTATE_WAITAREA9 = 29;   //等候区域9-等候中
        public const int PROCSTATE_CALLING = 30;   //叫号中
        public const int PROCSTATE_PROCESSING = 31;   //就诊中
        public const int PROCSTATE_FINISHED = 32;   //已就诊
        public const int PROCSTATE_NONARRIVAL = 33;   //未到过号
        public const int PROCSTATE_HANGUP = 34;   //挂起
        public const int PROCSTATE_GREENCHANNEL = 35;   //绿色通道
        public const int PROCSTATE_ARCHIVE = 40;   //归档

        public const string DEF_CALLVOICEENABLE = "CallVoiceEnable";
        public const string DEF_CALLVOICEFORMAT = "CallVoiceFormat";
        public const string DEF_CALLVOICEVOLUME = "CallVoiceVolume";
        public const string DEF_CALLVOICERATE = "CallVoiceRate";
        public const string DEF_CALLVOICESTYLE = "CallVoiceStyle";

        public const string DEF_WORKINGMODE = "WorkingMode";  //工作模式，按医生叫号模式（STAFF），按业务叫号模式(SERVICE)
        public const string DEF_WAITAREAMODE = "WaitAreaMode";   //多级候诊区模式(0,1,2,3)
        public const string DEF_CALLINGMODE = "CallingMode";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)
        public const string DEF_REGISTEMODE = "RegisteMode";//报到模式：自动报到（AUTO），手动报到(MANUAL)
        public const string DEF_REDIAGNOSISINTERVAL = "RediagnosisInterval";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)

        public const string SUBJECT_COUNTERSNUM = "SubjectCountersNum";
        public const string SUBJECT_SERVICESNUM = "SubjectServicesNum";
        public const string SUBJECT_STAFFSSNUM = "SubjectStaffsNum";

    }
}