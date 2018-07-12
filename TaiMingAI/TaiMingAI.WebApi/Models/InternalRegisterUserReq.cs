using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiMingAI.WebApi
{
    public class InternalRegisterUserReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码（MD5 32位加密）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号码（11位）
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 注册邮箱
        /// </summary>
        public string Email { get; set; }
    }
}