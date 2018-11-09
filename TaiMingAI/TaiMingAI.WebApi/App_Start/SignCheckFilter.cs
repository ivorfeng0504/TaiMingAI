using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TaiMingAi.WebApi.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.WebApi.App_Start
{
    /// <summary>
    /// 签名验证
    /// </summary>
    public class SignCheckFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string appId = string.Empty;
            string sign = string.Empty;
            string time = string.Empty;
            if (actionContext.Request.Headers.Contains("appId")
                && actionContext.Request.Headers.Contains("sign")
                && actionContext.Request.Headers.Contains("time"))
            {
                appId = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("appid").FirstOrDefault());
                sign = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("sign").FirstOrDefault());
                time = HttpUtility.UrlDecode(actionContext.Request.Headers.GetValues("time").FirstOrDefault());
            }
            if (string.IsNullOrEmpty(appId)
                || string.IsNullOrEmpty(sign)
                || string.IsNullOrEmpty(time))
            {
                var responseMsg = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
                responseMsg.ReasonPhrase = "缺失签名信息！";
                actionContext.Response = responseMsg;
            }
            if (!ValidateSign(appId, sign, time))
            {
                var responseMsg = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
                responseMsg.ReasonPhrase = "签名信认证失败！";
                actionContext.Response = responseMsg;
            }
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="appId">appId</param>0
        /// <param name="sign">签名</param>
        /// <param name="time">时间戳</param>
        /// <returns>验证结果（true:验证通过；false:验证失败）</returns>
        private bool ValidateSign(string appId, string sign, string time)
        {
            long timestamp;
            var flag = long.TryParse(time, out timestamp);
            if (!flag) return false;

            var nowtimestamp = TimestampHelper.GetNowTimestampOfJs();
            flag = TimestampHelper.ConfineToTimestampOfJs(nowtimestamp, timestamp, 5 * 60);
            if (!flag) return false;
            int key;
            flag = int.TryParse(appId, out key);
            if (!flag || !WebApiConst.SecretKeyDic.ContainsKey(key)) return false;

            //秘钥
            var secretKey = WebApiConst.SecretKeyDic[key];
            var singStr = appId + time + secretKey;
            singStr = singStr.Sha256();

            return sign.Equals(singStr, StringComparison.InvariantCulture);
        }
    }
}