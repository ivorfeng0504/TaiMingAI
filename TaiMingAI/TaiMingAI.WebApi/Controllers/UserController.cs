using System;
using System.Collections.Generic;
using System.Web.Http;
using TaiMingAi.WebApi.Model;
using TaiMingAI.DataHelper;
using TaiMingAI.Tools;
using TaiMingAI.WebApi.BLL;

namespace TaiMingAI.WebApi.Controllers
{
    public class UserController : BaseController
    {
        #region 内部注册用户API
        [HttpGet]
        public ResponseMsg<bool> InternalRegisterUser([FromUri] InternalRegisterUserReq req)
        {
            ResponseMsg<bool> res = null;
            try
            {
                if (req == null || string.IsNullOrEmpty(req.Name) ||
                    (!CheckHelper.CheckEmail(req.Email) && !CheckHelper.CheckMobileNo(req.Mobile)))
                {
                    return ErrorResponseMsg(false, "传入参数有误");
                }

                var tmingUserInfo = DataConvertHelper.ModelToModel<InternalRegisterUserReq, TmingUserInfo>(req);
                UserBLL userBll = new UserBLL();
                var result = userBll.InternalRegisterUser(tmingUserInfo);
                res = SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                res = ExceptionResponseMsg<bool>("InternalRegisterUser", ex);
            }
            return res;
        }
        #endregion

        public ResponseMsg<List<TmingUserInfo>> GetUserList()
        {
            ResponseMsg<List<TmingUserInfo>> res = null;
            try
            {
                UserBLL userBll = new UserBLL();
                var result = userBll.GetUserList();
                res = SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                res = ExceptionResponseMsg<List<TmingUserInfo>>("GetUserList", ex);
            }
            return res;
        }

        public ResponseMsg<TmingUserInfo> GetUserInfoById(int id)
        {
            ResponseMsg<TmingUserInfo> res = null;
            try
            {
                if (id == 0)
                {
                    return ErrorResponseMsg<TmingUserInfo>(null, "用户ID不能为空||0");
                }
                UserBLL userBll = new UserBLL();
                var result = userBll.GetUserInfoById(id);
                res = result == null ? ErrorResponseMsg(result, "没有获取到用户信息") : SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                res = ExceptionResponseMsg<TmingUserInfo>("GetUserInfoById", ex);
            }
            return res;
        }
    }
}