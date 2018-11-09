using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaiMingAI.Manager.DAL;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.BLL
{
    public class RoleBll
    {
        RoleDal dal = RoleDal.CreatDal();
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色List</returns>
        internal List<RoleDto> GetRoleList()
        {
            var list = dal.Query("xml.RoleSql.GetRoleList");
            return Mapper.Map<List<Role>, List<RoleDto>>(list);
        }

        internal bool InsertRole(RoleDto dto)
        {
            return dal.Insert("xml.RoleSql.InsertRole", Mapper.Map<RoleDto, Role>(dto)) > 0;
        }

        internal bool UpdateRole(RoleDto dto)
        {
            return dal.Update("xml.RoleSql.UpdateRole", Mapper.Map<RoleDto, Role>(dto)) > 0;
        }

        /// <summary>
        /// 根据角色Id 获取权限范围
        /// </summary>
        /// <param name="roleIds">角色IDs(每个ID“,”分开)</param>
        /// <returns>导航ID_List</returns>
        internal List<int> GetRoleLimitsByIds(string roleIds)
        {
            var limits = new List<int>();
            var roleIdArr = Array.ConvertAll(roleIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries), x => Convert.ToInt32(x));
            if (roleIdArr.Count() == 0) return limits;

            var list = GetRoleList();
            if (list == null || list.Count == 0) return limits;

            list = list.FindAll(x => x.IsUse && roleIdArr.Contains(x.Id));
            list.ForEach(x =>
            {
                var navIds = Array.ConvertAll(x.Limits.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries), l => Convert.ToInt32(l));
                limits.AddRange(navIds);
            });

            return limits.Distinct().ToList();
        }

    }
}
