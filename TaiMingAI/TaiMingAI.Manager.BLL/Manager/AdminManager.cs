using System;
using System.Collections.Generic;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.BLL
{
    public class AdminManager
    {
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <returns>管理员列表 list_AdministratorDto</returns>
        public ControllerResult<AdministratorDto> GetAdminList()
        {
            ControllerResult<AdministratorDto> result = new ControllerResult<AdministratorDto>();
            try
            {
                AdminBll bll = new AdminBll();
                var dtoList = bll.GetAdminList();
                result.IsSuccess = true;
                result.List = dtoList;
                result.Message = "获取数据成功";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.List = new List<AdministratorDto>();
                result.Message = "获取数据异常：" + ex.Message;
                result.ErrMessage = JsonHelper.ToJson(ex);
                LogHelper.ErrorLogFormat(ex, "获取管理员列表异常：{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 提交管理员信息
        /// </summary>
        /// <param name="dto">管理员信息</param>
        /// <returns>操作是否成功</returns>
        public ControllerResult SubmitAdmin(AdministratorDto dto)
        {
            ControllerResult result = new ControllerResult();
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.LoginName))
                {
                    result.DefaultError("传入数据不完整！");
                    return result;
                }
                AdminBll bll = new AdminBll();
                bool flag = dto.Id == 0
                    ? bll.InsertAdmin(dto)
                    : bll.UpdateAdmin(dto);
                if (flag)
                {
                    result.DefaultSuccess("保存管理员成功~");
                }
                else
                {
                    result.DefaultError();
                }
            }
            catch (Exception ex)
            {
                result.DefaultError("提交管理员信息异常", ex.Message);
                LogHelper.ErrorLogFormat(ex, "提交管理员信息异常：{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 更新管理员属性
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="property"></param>
        /// <returns>操作是否成功</returns>
        public ControllerResult UpdateAdminProperty(AdministratorDto dto, AdminProperty property)
        {
            ControllerResult result = new ControllerResult();
            try
            {
                if (dto == null || dto.Id == 0)
                {
                    result.DefaultError("传入数据不完整！");
                    return result;
                }
                AdminBll bll = new AdminBll();
                bool flag;
                switch (property)
                {
                    case AdminProperty.Password: flag = bll.ResetPassword(dto); break;
                    case AdminProperty.State: flag = bll.UpdateAdminState(dto); break;
                    default: flag = true; break;
                }
                if (flag)
                {
                    result.DefaultSuccess();
                }
                else
                {
                    result.DefaultError();
                }
            }
            catch (Exception ex)
            {
                result.DefaultError("更新管理员属性异常", ex.Message);
                LogHelper.ErrorLogFormat(ex, "更新管理员属性异常：{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="name">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>
        public ControllerResult<AdministratorDto> AdminLogin(string name, string password)
        {
            ControllerResult<AdministratorDto> result = new ControllerResult<AdministratorDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
                {
                    result.IsSuccess = false;
                    result.Message = "请传入数据完整的登录信息";
                    return result;
                }
                AdminBll bll = new AdminBll();
                var adminInfo = bll.AdminLogin(name, password);
                if (adminInfo == null)
                {
                    result.IsSuccess = false;
                    result.Message = "登录名或密码不正确";
                }
                else
                {
                    result.IsSuccess = true;
                    result.Data = adminInfo;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "管理员登录异常:" + ex.Message;
                LogHelper.ErrorLogFormat(ex, "管理员登录异常：{0}", ex.Message);
            }
            return result;
        }
    }
}
