namespace EntFrm.CallerConsole
{
    public class IPublicConsts
    {
        public const string DEF_WORKINGMODE = "WorkingMode";  //工作模式，按医生叫号模式（COUNTER），按科室叫号模式(STAFF)
        public const string DEF_WAITAREAMODE = "WaitAreaMode";   //多级候诊区模式(0,1,2,3)
        public const string DEF_CALLINGMODE = "CallingMode";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)
        public const string DEF_REDIAGNOSISINTERVAL = "RediagnosisInterval";//叫号模式：自动叫号（AUTO），手动叫号(MANUAL)

        public const string SUBJECT_COUNTERSNUM = "SubjectCountersNum";
        public const string SUBJECT_SERVICESNUM = "SubjectServicesNum";
        public const string SUBJECT_STAFFSSNUM = "SubjectStaffsNum";
    }
}
