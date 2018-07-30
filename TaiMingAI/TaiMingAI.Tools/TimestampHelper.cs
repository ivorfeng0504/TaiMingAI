using System;

namespace TaiMingAI.Tools
{
    /// <summary>
    /// 时间戳帮助类
    /// <![CDATA[
    /// 首先要清楚JavaScript与Unix的时间戳的区别：
    /// JavaScript时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的【总毫秒数】。
    /// Unix时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的【总秒数】。
    /// ]]>
    /// </summary>
    public class TimestampHelper
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <remarks>Unix时间戳</remarks>
        /// <returns>当前时间戳</returns>
        public static long GetNowTimestamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; //相差秒数
            return timeStamp;
        }
        /// <summary>
        /// 由时间戳转换为日期
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <remarks>Unix时间戳</remarks>
        /// <returns>日期</returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(timestamp);
            return dt;
        }
        /// <summary>
        /// 判断两个时间戳相隔是否在某个范围能
        /// </summary>
        /// <remarks>Unix时间戳</remarks>
        /// <param name="x">一个时间戳</param>
        /// <param name="y">另一个时间戳</param>
        /// <param name="interval">相隔时间/秒</param>
        /// <returns></returns>
        public static bool ConfineToTimestamp(long x, long y, int interval)
        {
            return interval >= Math.Abs(x - y);
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <remarks>JavaScript时间戳</remarks>
        /// <returns>当前时间戳</returns>
        public static long GetNowTimestampOfJs()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; //相差毫秒数
            return timeStamp;
        }
        /// <summary>
        /// 由时间戳转换为日期
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <remarks>JavaScript时间戳</remarks>
        /// <returns>日期</returns>
        public static DateTime TimestampToDateTimeOfJs(long timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(timestamp);
            return dt;
        }
        /// <summary>
        /// 判断两个时间戳相隔是否在某个范围能
        /// </summary>
        /// <param name="x">一个时间戳</param>
        /// <param name="y">另一个时间戳</param>
        /// <remarks>JavaScript时间戳</remarks>
        /// <param name="interval">相隔时间/秒</param>
        /// <returns></returns>
        public static bool ConfineToTimestampOfJs(long x, long y, int interval)
        {
            return interval * 1000 >= Math.Abs(x - y);
        }
    }
}
