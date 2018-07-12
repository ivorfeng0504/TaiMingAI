using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Manager.Model
{
    public class ManagerConst
    {
        /// <summary>
        /// Cookie-管理员登录信息
        /// </summary>
        public static string CkAdminInfo
        {
            get { return "manager.admin.logininfo"; }
        }
        /// <summary>
        /// 用户密码加密密钥
        /// </summary>
        public const string PaswordKey = "TmingUsesPasword:md5";
    }
}
