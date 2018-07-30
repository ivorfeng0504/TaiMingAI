using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.Manager.BLL;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public JsonResult Login(string username, string password)
        {
            AdminManager manager = new AdminManager();
            var result = manager.AdminLogin(username, password);
            if (result.IsSuccess)
            {
                HttpCookie adminCookie = new HttpCookie(ManagerConst.CkAdminInfo);
                adminCookie["Id"] = result.Data.Id.ToString();
                adminCookie["LoginName"] = result.Data.LoginName;
                adminCookie["NickName"] = Server.UrlEncode(result.Data.NickName);
                adminCookie["Password"] = result.Data.Password;
                adminCookie["Role"] = result.Data.Role;

                var timestamp = TimestampHelper.GetNowTimestamp().ToString();
                adminCookie["Sign"] = (result.Data.Password + timestamp).Sha256();
                adminCookie["Timestamp"] = timestamp;

                adminCookie.Expires = DateTime.Now.AddHours(ManagerConfig.LoginTimeout);
                Response.Cookies.Add(adminCookie);
            }
            return Json(result);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            var cookies = Request.Cookies[ManagerConst.CkAdminInfo];
            if (cookies != null)
            {
                cookies.Expires = DateTime.Now.AddDays(-1);
                Response.AppendCookie(cookies);
            }
            return Redirect("/Login/Index");
        }
    }
}