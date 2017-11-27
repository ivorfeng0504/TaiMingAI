namespace TaiMingAi.WebApi.Model
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class TmingUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码（MD5 32位加密）
        /// </summary>
        public string Powsword { get; set; }

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
