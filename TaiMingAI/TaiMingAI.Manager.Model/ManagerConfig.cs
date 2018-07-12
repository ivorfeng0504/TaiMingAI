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
    }
}
