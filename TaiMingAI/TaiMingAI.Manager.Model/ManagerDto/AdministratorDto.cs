using System;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Model
{
    public class AdministratorDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码（MD5 32位加密）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 手机号码（11位）
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别；1：男，0：女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// -1:注销；0:未审核；1:审核通过；
        /// </summary>
        public int State { get; set; }
        public string StateStr
        {
            get
            {
                switch (State)
                {
                    case -1: return "已注销";
                    case 0: return "未审核";
                    case 1: return "审核通过";
                    default: return "--";
                }
            }
        }

        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStr
        {
            get { return UpdateTime.Year > 2000 ? UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") : "--"; }
        }
    }
    /// <summary>
    /// 管理员属性
    /// </summary>
    public enum AdminProperty
    {
        /// <summary>
        /// 密码
        /// </summary>
        Password = 1,
        /// <summary>
        /// 状态
        /// </summary>
        State
    }
}
