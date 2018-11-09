using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.Manager.Models;

namespace TaiMingAI.Manager.Filters
{
    public class FormAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 先进入此方法，此方法中会调用 AuthorizeCore 验证逻辑，验证不通过会调用 HandleUnauthorizedRequest 方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Principal user = httpContext.User as Principal;
            if (user != null)
                return user.IsInRole(base.Roles);
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //验证不通过，直接跳转到相应页面
            filterContext.Result = new RedirectResult("~/Login/Index");
        }
    }
}