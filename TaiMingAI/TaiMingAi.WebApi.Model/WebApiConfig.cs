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
    }
}
