using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using TaiMingAi.WebApi.Model;
using TaiMingAI.WebApi.App_Start;

namespace TaiMingAI.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //跨域配置
            config.EnableCors(new EnableCorsAttribute(TaiMingAi.WebApi.Model.WebApiConfig.CorsOrigins, TaiMingAi.WebApi.Model.WebApiConfig.CorsHeaders, TaiMingAi.WebApi.Model.WebApiConfig.CorsMethods));
            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new SignCheckFilter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("datatype", "json", "application/json"));
        }
    }
}
