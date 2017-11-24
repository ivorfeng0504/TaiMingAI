using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaiMingAI.Tools;

namespace TaiMingAI.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        ///成功响应消息
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">响应数据</param>
        /// <returns></returns>
        protected ResponseMsg<T> SuccessResponseMsg<T>(T data)
        {
            var sucessMsg = new ResponseMsg<T>
            {
                ResultEnum = ResultEnum.success,
                Msg = "接口处理成功",
                ResponseData = data
            };
            return sucessMsg;
        }
        /// <summary>
        ///错误响应消息
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="msg">错误消息</param>
        /// <param name="data">响应数据</param>
        /// <returns></returns>
        protected ResponseMsg<T> ErrorResponseMsg<T>(string msg, T data)
        {
            var errorMsg = new ResponseMsg<T>
            {
                ResultEnum = ResultEnum.error,
                Msg = "接口处理未成功",
                ResponseData = data,
                ErrorMsg = msg,
            };
            return errorMsg;
        }
        /// <summary>
        ///异常响应消息
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="actionName">接口名称</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        protected ResponseMsg<T> ExceptionResponseMsg<T>(string actionName, Exception ex)
        {
            LogHelper.FatalLog("接口：" + actionName + "异常", ex);
            var exMsg = new ResponseMsg<T>
            {
                ResultEnum = ResultEnum.exception,
                ExceptionMsg = ex.Message,
                Msg = "接口处理异常",
                ResponseData = default(T)
            };
            return exMsg;
        }

    }
}