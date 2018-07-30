using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.Manager.Model.AutoMapper
{
    public static class Configuration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MapperProfile>());
        }
    }
}
