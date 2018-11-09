using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.DataHelper;
using TaiMingAI.Manager.DAL;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.BLL
{
    public class NavbarBll
    {
        NavbarDal dal = NavbarDal.CreatDal();
        /// <summary>
        /// 获取导航菜单列表
        /// </summary>
        /// <param name="isAll">是否获取全部数据;默认只获取显示的菜单</param>
        /// <returns></returns>
        internal List<NavbarDto> GetNavbarDtoList(bool isAll)
        {
            var list = dal.Query("xml.NavbarSql.GetNavbarList");
            if (list == null || list.Count == 0) return null;
            if (!isAll)
            {
                list = list.FindAll(x => x.IsShow);
            }
            return Mapper.Map<List<Navbar>, List<NavbarDto>>(list);
        }

        internal bool InsertNavber(NavbarDto dto)
        {
            return dal.Insert("xml.NavbarSql.InsertNavber", Mapper.Map<NavbarDto, Navbar>(dto)) > 0;
        }

        internal bool UpdateNavber(NavbarDto dto)
        {
            return dal.Update("xml.NavbarSql.UpdateNavber", Mapper.Map<NavbarDto, Navbar>(dto)) > 0;
        }
    }
}
