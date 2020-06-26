using System;

namespace EntFrm.MainService
{
    public class LoggerHelper
    {
        public static LoggerClass myLoggerClass = LoggerClass.CommonLog;
        private volatile static LoggerHelper _instance = null;
        private static readonly object lockHelper = new object();

        private LoggerHelper()
        {
        }

        public static LoggerHelper CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new LoggerHelper();
                }
            }
            return _instance;
        }

        public void Debug(Type t, object message, Exception e)
        {
            Insert_Logger(t, message.ToString(), e);
        }

        public void Error(Type t, object message, Exception e)
        {
            Insert_Logger(t, message.ToString(), e);
        }

        public void Info(Type t, object message, Exception e)
        {
            Insert_Logger(t, message.ToString(), e);
        }

        public void Fatal(Type t, object message, Exception e)
        {
            Insert_Logger(t, message.ToString(), e);
        }

        public void Warn(Type t, object message, Exception e)
        {
            Insert_Logger(t, message.ToString(), e);
        }

        private bool Insert_Logger(Type t, string message, Exception e)
        {
            try
            {
                //WriteLog(t, message);
                //if (e != null)
                //{
                //    WriteLog(t, e);
                //}
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(t, ex);
                return false;
            }
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region void WriteLog( Exception ex)

        public void WriteLog(Type t, Exception ex)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(t);
            //log.Error("错误信息", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region void WriteLog( string msg)

        public void WriteLog(Type t, string msg)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(t);
            //log.Info(msg);
        }

        #endregion
    }


    public enum LoggerClass
    {
        CommonLog,            //一般性信息
        DebugLog            //调试信息
    }


    //FATAL（致命错误）、ERROR（一般错误）、WARN（警告）、INFO（一般信息）、DEBUG（调试信息）
    public enum LoggerGrade
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug
    }
}
