using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.DataHelper;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.BLL
{
    public class RoleManager
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns>角色列表 list_RoleDto</returns>
        public ControllerResult<RoleDto> GetRoleList()
        {
            ControllerResult<RoleDto> result = new ControllerResult<RoleDto>();
            try
            {
                RoleBll roleBll = new RoleBll();
                List<Role> list = roleBll.GetRoleList();
                var dtoList = DataConvertHelper.ListToList<Role, RoleDto>(list);
                result.IsSuccess = true;
                result.List = dtoList;
                result.Message = "获取数据成功";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.List = new List<RoleDto>();
                result.Message = "获取数据异常：" + ex.Message;
                result.ErrMessage = JsonHelper.ToJson(ex);
                LogHelper.ErrorLogFormat(ex, "获取角色列表异常：{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 提交角色信息
        /// </summary>
        /// <param name="dto">角色信息</param>
        /// <returns>操作是否成功</returns>

        public ControllerResult SubmitRole(RoleDto dto)
        {
            ControllerResult result = new ControllerResult();
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                {
                    result.DefaultError("传入数据不完整！");
                    return result;
                }
                RoleBll roleBll = new RoleBll();
                bool flag = dto.Id == 0
                    ? roleBll.InsertRole(dto)
                    : roleBll.UpdateRole(dto);
                if (flag)
                {
                    result.DefaultSuccess("保存角色成功~");
                }
                else
                {
                    result.DefaultError();
                }
            }
            catch (Exception ex)
            {
                result.DefaultError("保存角色出现异常", ex.Message);
                LogHelper.ErrorLogFormat(ex, "获取角色列表异常：{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取角色键值对
        /// </summary>
        /// <returns>角色键值对_Dictionary</returns>
        public Dictionary<int, string> GetRoleDic()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            try
            {
                RoleBll roleBll = new RoleBll();
                List<Role> list = roleBll.GetRoleList();
                list = list.FindAll(x => x.IsUse);
                foreach (var item in list)
                {
                    dic.Add(item.Id, item.Name);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogFormat(ex, "获取角色键值对异常：{0}", ex.Message);
            }
            return dic;
        }
    }
}
