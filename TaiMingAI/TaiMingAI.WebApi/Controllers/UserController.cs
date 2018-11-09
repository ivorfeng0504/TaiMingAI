using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Security;
using TaiMingAi.WebApi.Model;
using TaiMingAI.DataHelper;
using TaiMingAI.Tools;
using TaiMingAI.WebApi.App_Start;
using TaiMingAI.WebApi.BLL;
using TaiMingAI.WebApi.Models;

namespace TaiMingAI.WebApi.Controllers
{
    public class UserController : BaseController
    {
        #region 内部注册用户API
        [HttpGet]
        public ApiResult<bool> InternalRegisterUser([FromUri] InternalRegisterUserReq req)
        {
            ApiResult<bool> res = null;
            try
            {
                if (req == null || string.IsNullOrEmpty(req.Name) ||
                    (!CheckHelper.CheckEmail(req.Email) && !CheckHelper.CheckMobile(req.Mobile)))
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

        [HttpGet]

        public ApiResult<LoginDoRespons> LoginDo([FromUri] LoginDoRequest model)
        {
            try
            {
                UserBLL userBll = new UserBLL();
                LoginDoRespons result = userBll.LoginDo(model);
                if (result == null)
                {
                    return ErrorResponseMsg(result, "登录失败！");
                }
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, model.LoginName, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", model.LoginName, model.Password),
                            FormsAuthentication.FormsCookiePath);
                result.Ticket = FormsAuthentication.Encrypt(ticket);
                return SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                return ExceptionResponseMsg<LoginDoRespons>("LoginDo", ex);
            }
        }
        public ApiResult<List<TmingUserInfo>> GetUserList()
        {
            ApiResult<List<TmingUserInfo>> res = null;
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

        [AuthenticationAttribute]
        public ApiResult<TmingUserInfo> GetUserInfoById(int id)
        {
            ApiResult<TmingUserInfo> res = null;
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