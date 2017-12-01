using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAi.WebApi.Model;
using TaiMingAI.Tools;
using TaiMingAI.WebApi.DLL;

namespace TaiMingAI.WebApi.BLL
{
    public class UserBll
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
    }
}
