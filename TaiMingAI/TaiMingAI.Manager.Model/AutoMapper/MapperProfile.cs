using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Manager.Model.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<Administrator, AdministratorDto>().ReverseMap();

            CreateMap<Navbar, NavbarDto>().ReverseMap();
        }
    }
}
