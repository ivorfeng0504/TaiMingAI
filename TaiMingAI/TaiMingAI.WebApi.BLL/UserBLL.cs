using System;
using System.Collections.Generic;
using TaiMingAi.WebApi.Model;
using TaiMingAI.Tools;
using TaiMingAI.WebApi.DLL;
using TaiMingAI.WebApi.Models;

namespace TaiMingAI.WebApi.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 内部注册用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InternalRegisterUser(TmingUserInfo info)
        {
            info.Powsword = MD5Helper.MD5UPassword(info.Powsword, WebApiConst.PaswordKey);
            return UserDll.CreateUserDll.InternalRegisterUser(info);
        }

        public List<TmingUserInfo> GetUserList()
        {
            return UserDll.CreateUserDll.GetUserInfoList();
        }

        public TmingUserInfo GetUserInfoById(int id)
        {
            return UserDll.CreateUserDll.GetUserInfoById(id);
        }

        public LoginDoRespons LoginDo(LoginDoRequest model)
        {
            return new LoginDoRespons
            {
                UserName = "齐天大圣",
                Phone = 18888888888
            };
        }
    }
}
