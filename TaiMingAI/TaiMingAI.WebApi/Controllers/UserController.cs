using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaiMingAi.WebApi.Model;
using TaiMingAI.DataHelper;
using TaiMingAI.Tools;
using TaiMingAI.Tools.Xml;
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
                    return ErrorResponseMsg("传入参数有误", false);
                }

                var tmingUserInfo = DataConvertHelper.ModelToModel<InternalRegisterUserReq, TmingUserInfo>(req);
                UserBll userBll = new UserBll();
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
                UserBll userBll = new UserBll();
                var result = userBll.GetUserList();
                res = SuccessResponseMsg(result);
            }
            catch (Exception ex)
            {
                res = ExceptionResponseMsg<List<TmingUserInfo>>("GetUserList", ex);
            }
            return res;
        }
    }
}