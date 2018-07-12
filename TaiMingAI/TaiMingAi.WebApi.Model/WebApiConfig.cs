using System.Configuration;


namespace TaiMingAi.WebApi.Model
{
    /// <summary>
    /// 配置参数类
    /// </summary>
    public class WebApiConfig
    {
        /// <summary>
        /// User数据库连接字符串
        /// </summary>
        public static string DbUserConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DbUserConnectionString"].ConnectionString;
            }
        }
        /// <summary>
        /// 资源支持的域名
        /// </summary>
        ///  <remarks>
        /// 多个用逗号（,）分隔；星号（*）表示支持全部
        /// </remarks>
        public static string CorsOrigins
        {
            get
            {
                var origins = ConfigurationManager.AppSettings["CorsOrigins"];
                return string.IsNullOrEmpty(origins) ? "*" : origins;
            }
        }
        /// <summary>
        /// 资源支持的请求方法，例如GET,POST,PUT, DELETE等等
        ///</summary>
        /// <remarks>
        /// 多个用逗号（,）分隔,大小写不敏感;星号（*）表示支持全部
        /// </remarks>
        public static string CorsMethods
        {
            get
            {
                var methods = ConfigurationManager.AppSettings["CorsMethods"];
                return string.IsNullOrEmpty(methods) ? "*" : methods;
            }
        }
        /// <summary>
        /// 资源支持的自定义头部
        /// </summary>
        /// <remarks>
        /// 多个用逗号（,）分隔；星号（*）表示支持全部；大小写不敏感
        /// </remarks>
        public static string CorsHeaders
        {
            get
            {
                var headers = ConfigurationManager.AppSettings["CorsHeaders"];
                return string.IsNullOrEmpty(headers) ? "*" : headers;
            }
        }

        public static string BaiduSongApi
        {
            get
            {
                return ConfigurationManager.AppSettings["BaiduSongApi"];
            }
        }
    }
}
