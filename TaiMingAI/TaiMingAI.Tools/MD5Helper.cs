using System;
using System.Security.Cryptography;
using System.Text;

namespace TaiMingAI.Tools
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密用户密码
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="key">密钥</param>
        /// <returns>加密密码</returns>
        public static string MD5UPassword(string pwd, string key)
        {
            if (string.IsNullOrEmpty(pwd)) return null;
            var str = pwd + key;
            return MD5Encrypt(str);
        }

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="bit">加密位数;32:32位加密;16:16位加密</param>
        /// <param name="isToLower">是否小写;true:小写;false:大写</param>
        /// <returns>加密字符串（默认32位小写）</returns>
        public static string MD5Encrypt(string str, int bit = 32, bool isToLower = true)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue = Encoding.UTF8.GetBytes(str);
                byte[] bytHash = md5.ComputeHash(bytValue);
                md5.Clear();

                if (bit == 32)//32位加密
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < bytHash.Length; i++)
                    {
                        sb.Append(bytHash[i].ToString("X").PadLeft(2, '0'));
                    }
                    str = sb.ToString();
                }
                else if (bit == 16)//16位加密
                {
                    str = BitConverter.ToString(bytHash, 4, 8);
                    str = str.Replace("-", "");
                }
                else//原文返回
                {
                    return str;
                }

                if (isToLower)//是否小写
                {
                    str = str.ToLower();
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("MD5加密异常", ex);
            }

            return str;
        }
        #endregion
    }
}
