using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Manager.DAL;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.BLL
{
    public class AdminBll
    {
        AdminDal dal = AdminDal.CreatDal();

        internal List<AdministratorDto> GetAdminList()
        {
            var list = dal.GetAdminList();
            var dtoList = Mapper.Map<List<Administrator>, List<AdministratorDto>>(list);
            return dtoList;
        }

        internal bool InsertAdmin(AdministratorDto dto)
        {
            dto.Password = MD5Helper.MD5UPassword(dto.Password, ManagerConst.PaswordKey);
            return dal.InsertAdmin(dto);
        }

        internal bool UpdateAdmin(AdministratorDto dto)
        {
            return dal.UpdateAdmin(dto);
        }

        internal bool ResetPassword(AdministratorDto dto)
        {
            dto.Password = MD5Helper.MD5UPassword(dto.Password, ManagerConst.PaswordKey);
            return dal.ResetPassword(dto);
        }
        internal bool UpdateAdminState(AdministratorDto dto)
        {
            return dal.UpdateAdminState(dto);
        }

        internal AdministratorDto AdminLogin(string name, string password)
        {
            password = MD5Helper.MD5UPassword(password, ManagerConst.PaswordKey);
            var info = dal.AdminLogin(name, password);
            return Mapper.Map<Administrator, AdministratorDto>(info);
        }
    }
}
