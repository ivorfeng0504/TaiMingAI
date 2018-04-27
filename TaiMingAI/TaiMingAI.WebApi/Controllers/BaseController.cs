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
        protected ApiResult<T> SuccessResponseMsg<T>(T data)
        {
            var sucessMsg = new ApiResult<T>
            {
                ResultEnum = ResultEnum.success,
                Msg = "接口处理成功",
                ResultData = data
            };
            return sucessMsg;
        }
        /// <summary>
        ///错误响应消息
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">响应数据</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        protected ApiResult<T> ErrorResponseMsg<T>(T data, string msg)
        {
            var errorMsg = new ApiResult<T>
            {
                ResultEnum = ResultEnum.error,
                Msg = "接口处理未成功",
                ResultData = data,
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
        protected ApiResult<T> ExceptionResponseMsg<T>(string actionName, Exception ex)
        {
            LogHelper.FatalLog("接口：" + actionName + "异常", ex);
            var exMsg = new ApiResult<T>
            {
                ResultEnum = ResultEnum.exception,
                ExceptionMsg = ex.Message,
                Msg = "接口处理异常",
                ResultData = default(T)
            };
            return exMsg;
        }

    }
}