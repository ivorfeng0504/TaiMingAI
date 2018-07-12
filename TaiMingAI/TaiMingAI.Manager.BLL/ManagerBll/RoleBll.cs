using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Manager.DAL;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.BLL
{
    public class RoleBll
    {
        internal List<Role> GetRoleList()
        {
            RoleDal roleDal = new RoleDal();
            var list = roleDal.GetRoleList();
            return list;
        }

        internal bool InsertRole(RoleDto dto)
        {
            RoleDal roleDal = new RoleDal();
            return roleDal.InsertRole(dto);
        }

        internal bool UpdateRole(RoleDto dto)
        {
            RoleDal roleDal = new RoleDal();
            return roleDal.UpdateRole(dto);
        }
    }
}
