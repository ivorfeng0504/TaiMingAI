using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaiMingAI.Manager.Models;

namespace TaiMingAI.Manager
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //映射配置
            Model.AutoMapper.Configuration.Configure();
        }
        protected void Application_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            HttpContext.Current.User = HttpFormsAuthentication.TryParsePrincipal(HttpContext.Current);
        }
    }
}
