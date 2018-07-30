using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiMingAI.WebApi.Models
{
    /// <summary>
    /// 登录请求
    /// </summary>
    public class LoginDoRequest
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// 登录响应
    /// </summary>
    public class LoginDoRespons
    {
        /// <summary>
        /// 票据
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public long Phone { get; set; }
    }
}