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
            var list = dal.Query("xml.Administrator.GetAdminList");
            var dtoList = Mapper.Map<List<Administrator>, List<AdministratorDto>>(list);
            return dtoList;
        }

        internal bool InsertAdmin(AdministratorDto dto)
        {
            dto.Password = MD5Helper.MD5UPassword(dto.Password, ManagerConst.PaswordKey);
            dto.HeadImg = "/images/head/" + (dto.Sex == 1 ? "man" : "woman") + ".png";
            return dal.Insert("xml.Administrator.InsertAdmin", Mapper.Map<AdministratorDto, Administrator>(dto)) > 0;
        }

        internal bool UpdateAdmin(AdministratorDto dto)
        {
            return dal.Update("xml.Administrator.UpdateAdmin", Mapper.Map<AdministratorDto, Administrator>(dto)) > 0;
        }

        internal bool ResetPassword(AdministratorDto dto)
        {
            dto.Password = MD5Helper.MD5UPassword(dto.Password, ManagerConst.PaswordKey);
            return dal.Update("xml.Administrator.ResetPassword", Mapper.Map<AdministratorDto, Administrator>(dto)) > 0;
        }

        internal bool UpdateAdminState(AdministratorDto dto)
        {
            return dal.Update("xml.Administrator.UpdateAdminState", Mapper.Map<AdministratorDto, Administrator>(dto)) > 0;
        }

        internal AdministratorDto AdminLogin(string name, string password)
        {
            password = MD5Helper.MD5UPassword(password, ManagerConst.PaswordKey);
            var info = dal.Query("xml.Administrator.AdminLogin", new Administrator { LoginName = name, Password = password });
            return Mapper.Map<Administrator, AdministratorDto>(info);
        }
    }
}
