using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Models
{
    public class HttpFormsAuthentication
    {
        /// <summary>
        /// 将用户信息通过ticket加密保存到cookie
        /// </summary>
        /// <param name="account">用户信息</param>
        public static void SetAuthenticationCookie(AdministratorDto account)
        {
            if (account == null)
                throw new ArgumentNullException("SetAuthenticationCookie:用户信息为空");

            string accountJson = JsonHelper.ToJson(account);
            //创建用户票据
            var ticket = new FormsAuthenticationTicket(1, account.LoginName, DateTime.Now, DateTime.Now.AddHours(ManagerConfig.LoginTimeout), true, accountJson);
            //加密
            var encryptTicket = FormsAuthentication.Encrypt(ticket);
            //创建cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket)
            {
                HttpOnly = false,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                Expires = ticket.Expiration
            };
            //写入Cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static Principal TryParsePrincipal(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("TryParsePrincipal:请求信息为空");

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || cookie.Value == null) return null;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null) return null;

            AdministratorDto account = JsonHelper.FromJson<AdministratorDto>(ticket.UserData);
            if (account == null) return null;

            return new Principal(ticket, account);
        }
    }
}