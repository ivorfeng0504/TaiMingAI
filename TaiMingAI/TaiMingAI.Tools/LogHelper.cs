using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Tools
{
    public class LogHelper
    {
        #region 日志模板
        /// <summary>
        /// 父模板
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
        /// <summary>
        /// 记录消息模板
        /// </summary>
        private static readonly log4net.ILog infoLog = log4net.LogManager.GetLogger("InfoLoger");
        /// <summary>
        /// 记录错误模板
        /// </summary>
        private static readonly log4net.ILog errorLog = log4net.LogManager.GetLogger("ErrorLoger");
        /// <summary>
        /// 致命错误模板
        /// </summary>
        private static readonly log4net.ILog fatalLog = log4net.LogManager.GetLogger("FatalLoger");
        #endregion

        #region 消息日志
        /// <summary>
        /// 信息记录
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void InfoLog(object message)
        {
            infoLog.Info(message);
        }
        public static void InfoLogFormat(string formatStr, object arg0, object arg1, object arg2)
        {
            infoLog.InfoFormat(formatStr, arg0, arg1, arg2);
        }
        public static void InfoLogFormat(string formatStr, object arg0, object arg1)
        {
            infoLog.InfoFormat(formatStr, arg0, arg1);
        }
        public static void InfoLogFormat(string formatStr, object arg0)
        {
            infoLog.InfoFormat(formatStr, arg0);
        }
        public static void InfoLogFormat(string formatStr, params object[] args)
        {
            infoLog.InfoFormat(formatStr, args);
        }
        #endregion

        #region 错误日志
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="message">日志消息</param>
        public static void ErrorLog(object message)
        {
            errorLog.Error(message);
        }
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常信息</param>
        public static void ErrorLog(object message, Exception ex)
        {
            errorLog.Error(message, ex);
        }
        public static void ErrorLogFormat(string formatStr, object arg0, object arg1, object arg2)
        {
            errorLog.ErrorFormat(formatStr, arg0, arg1, arg2);
        }
        public static void ErrorLogFormat(string formatStr, object arg0, object arg1)
        {
            errorLog.ErrorFormat(formatStr, arg0, arg1);
        }
        public static void ErrorLogFormat(string formatStr, object arg0)
        {
            errorLog.ErrorFormat(formatStr, arg0);
        }
        public static void ErrorLogFormat(Exception ex, string formatStr, params object[] args)
        {
            errorLog.Error(string.Format(formatStr, args), ex);
        }
        #endregion

        #region 致命错误日志 
        /// <summary>
        /// 致命错误记录
        /// </summary>
        /// <param name="message">致命消息</param>
        public static void FatalLog(object message)
        {
            fatalLog.Fatal(message);
        }
        /// <summary>
        /// 致命错误记录
        /// </summary>
        /// <param name="message">致命消息</param>
        /// <param name="ex">异常消息</param>
        public static void FatalLog(object message, Exception ex)
        {
            fatalLog.Fatal(message, ex);
        }
        public static void FatalLogFormat(string format, object arg0, object arg1, object arg2)
        {
            fatalLog.FatalFormat(format, arg0, arg1, arg2);
        }
        public static void FatalLogFormat(string format, object arg0, object arg1)
        {
            fatalLog.FatalFormat(format, arg0, arg1);
        }
        public static void FatalLogFormat(string format, object arg0)
        {
            fatalLog.FatalFormat(format, arg0);
        }
        public static void FatalLogFormat(string format, params object[] args)
        {
            fatalLog.FatalFormat(format, args);
        }
        #endregion
    }
}
