using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.DataHelper;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Controllers
{
    public class BaseController : Controller
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
            //验证登录
            var cookieInfo = Request.Cookies[ManagerConst.CkAdminInfo];
            if (!CheckCookies(cookieInfo))
            {
                filterContext.Result = Redirect("/Login/Index?msg=登录超时,请重新登录！");
                return;
            }
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