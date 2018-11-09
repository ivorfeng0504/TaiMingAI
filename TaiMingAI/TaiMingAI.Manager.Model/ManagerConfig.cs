using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Manager.Model
{
    public class ManagerConfig
    {
        /// <summary>
        /// Manager数据库连接字符串
        /// </summary>
        public static string DbManager
        {
            get { return ConfigurationManager.ConnectionStrings["DbManagerConnectionString"].ConnectionString; }
        }

        /// <summary>
        /// 登录超时时间 单位/h
        /// </summary>
        public static int LoginTimeout
        {
            get
            {
                var loginTimeout = ConfigurationManager.AppSettings["LoginTimeout"];
                return string.IsNullOrEmpty(loginTimeout) ? 12 : Convert.ToInt32(loginTimeout);
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public static string UploadImageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadImageUrl"];
            }
        }
    }
}
