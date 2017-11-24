using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaiMingAI.Tools
{
    public class CheckHelper
    {
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email">email</param>
        /// <remarks>
        /// eg:CheckEmail("sayook@qq.com");
        /// </remarks>
        /// <returns>true:验证通过;false:验证失败;</returns>
        public static bool CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            Regex regex = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
            return regex.IsMatch(email);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="number">手机号码</param>
        /// <remarks>
        /// eg:CheckMobileNo(13888888888);
        /// </remarks>
        /// <returns>true:验证通过;false:验证失败;</returns>
        public static bool CheckMobileNo(string number)
        {
            if (string.IsNullOrEmpty(number)) return false;
            return Regex.IsMatch(number.ToString(), @"^[1]+[3,4,5,7,8]+\d{9}");
        }
    }
}
