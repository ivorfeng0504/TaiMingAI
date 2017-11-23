using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiMingAI.WebApi.Models
{
    public enum ResultEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 1,
        /// <summary>
        /// 错误
        /// </summary>
        error = 2,
        /// <summary>
        /// 异常
        /// </summary>
        exception = 3

    }
    /// <summary>
    /// Api统一返回类型
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    public class ResponseMsg<T>
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public ResultEnum ResultEnum;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get
            {
                if (string.IsNullOrEmpty(Msg))
                {
                    return "处理成功";
                }
                return Msg;
            }
            set { Msg = value; }
        }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMsg { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        public T ResponseData { get; set; }
    }
}