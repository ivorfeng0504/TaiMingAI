using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.DataHelper;
using TaiMingAI.Manager.Model;
using TaiMingAI.Manager.Models;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public AdministratorDto UserInfo { get; set; }
        /// <summary>
        ///  在调用操作方法前调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var principal = User as Principal;
            if (principal == null)
            {
                if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Home")
                    filterContext.Result = new RedirectResult("~/Login/Index");
                else
                    filterContext.Result = new RedirectResult("~/Error/Timeout");
            }
            else
            {
                UserInfo = principal.Account;
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 在调用操作方法后调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="cookieInfo">cookieInfo</param>
        /// <returns>验证结果</returns>
        private bool CheckCookies(HttpCookie cookieInfo)
        {
            try
            {
                if (cookieInfo == null) return false;
                var sign = cookieInfo["Sign"];
                var timestamp = cookieInfo["Timestamp"];
                var password = cookieInfo["Password"];
                //验证签名
                var _sign = (password + timestamp).Sha256();
                if (!sign.Equals(_sign)) return false;
                //验证失效
                var newTimestamp = TimestampHelper.GetNowTimestamp();
                var tamp = (newTimestamp - DataConvertHelper.ToInt(timestamp)) / 60 / 60;
                if (tamp > ManagerConfig.LoginTimeout) return false;
                //赋值
                var id = cookieInfo["Id"];
                var loginName = cookieInfo["LoginName"];
                var nickName = Server.UrlDecode(cookieInfo["NickName"]);
                var role = cookieInfo["Role"];
                UserInfo = new AdministratorDto
                {
                    Id = DataConvertHelper.ToInt(id),
                    LoginName = loginName,
                    NickName = nickName,
                    Password = password,
                    Role = role
                };
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogFormat(ex, "验证登录信息 异常：{0}", ex.Message);
                return false;
            }
        }
    }
}