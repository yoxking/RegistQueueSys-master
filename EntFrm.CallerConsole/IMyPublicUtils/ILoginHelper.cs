using System;

namespace EntFrm.CallerConsole
{
    public class ILoginHelper 
    {
        private string StafferNo = "";
        private string LoginId = "";
        private string Password = "";
        private string StafferName = "";
        private string CounterNo = "";
        private string CounterName = "";
        private string WorkingMode = "STAFF";
        private bool isLogin = false;
        private DateTime LoginTime;


        private static ILoginHelper _CurrentUser = null;

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public static ILoginHelper CurrentUser
        {
            get
            {
                if (_CurrentUser == null) _CurrentUser = new ILoginHelper(); //空对象
                return _CurrentUser;
            }
            set
            {
                _CurrentUser = value;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string sStafferNo { get { return StafferNo; } set { StafferNo = value; } }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string sLoginId { get { return LoginId; } set { LoginId = value; } }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string sPassword { get { return Password; } set { Password = value; } }

        /// <summary>
        /// 用户名
        /// </summary>
        public string sStafferName { get { return StafferName; } set { StafferName = value; } } 
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime dLoginTime { get { return LoginTime; } set { LoginTime = value; } } 

        /// <summary>
        /// 窗口
        /// </summary>
        public string sCounterNo { get { return CounterNo; } set { CounterNo = value; } }

        /// <summary>
        /// 窗口名称
        /// </summary>
        public string sCounterName { get { return CounterName; } set { CounterName = value; } }
        public string sWorkingMode { get { return WorkingMode; } set { WorkingMode = value; } }

        public bool IsLogin { get { return isLogin; } set { isLogin = value; } } 

    }
}
