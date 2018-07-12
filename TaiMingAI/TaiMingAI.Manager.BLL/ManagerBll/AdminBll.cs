using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Manager.DAL;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.BLL
{
    public class AdminBll
    {
        AdminDal dal = AdminDal.CreatDal();

        internal List<Administrator> GetAdminList()
        {
            var list = dal.GetAdminList();
            return list;
        }

        internal bool InsertAdmin(AdministratorDto dto)
        {
            return dal.InsertAdmin(dto);
        }

        internal bool UpdateAdmin(AdministratorDto dto)
        {
            return dal.UpdateAdmin(dto);
        }

        internal bool ResetPassword(AdministratorDto dto)
        {
            return dal.ResetPassword(dto);
        }
        internal bool UpdateAdminState(AdministratorDto dto)
        {
            return dal.UpdateAdminState(dto);
        }

        internal AdministratorDto AdminLogin(string name, string password)
        {
            var info = dal.AdminLogin(name, password);
            return DataHelper.DataConvertHelper.ModelToModel<Administrator, AdministratorDto>(info);
        }
    }
}
