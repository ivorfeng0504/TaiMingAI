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
        /// eg:CheckMobile(13888888888);
        /// </remarks>
        /// <returns>true:验证通过;false:验证失败;</returns>
        public static bool CheckMobile(string number)
        {
            if (string.IsNullOrEmpty(number)) return false;
            return Regex.IsMatch(number.ToString(), @"^[1]+[3,4,5,7,8]+\d{9}");
        }

        /// <summary>
        /// 验证字符串必须是有数字和字母组成
        /// </summary>
        /// <param name="value">需要验证的字符串</param>
        /// <param name="lenght">字符串长度，默认6位</param>
        /// <returns>true:验证通过;false:验证失败;</returns>
        public static bool Check(string value, int lenght = 6)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var regex = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{" + lenght + ",}$";
            return Regex.IsMatch(value, regex);
        }

    }
}
