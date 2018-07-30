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
        /// <summary>
        /// 获取导航菜单列表
        /// </summary>
        /// <param name="isAll">是否获取全部数据;默认只获取显示的菜单</param>
        /// <returns></returns>
        internal List<NavbarDto> GetNavbarDtoList(bool isAll)
        {
            NavbarDal navbarDal = new NavbarDal();
            var list = navbarDal.GetNavbarList();
            if (list == null || list.Count == 0) return null;
            if (!isAll)
            {
                list = list.FindAll(x => x.IsShow);
            }
            return Mapper.Map<List<Navbar>, List<NavbarDto>>(list);
        }

        internal bool InsertNavber(Navbar navBar)
        {
            NavbarDal navbarDal = new NavbarDal();
            return navbarDal.InsertNavber(navBar);
        }

        internal bool UpdateNavber(Navbar navBar)
        {
            NavbarDal navbarDal = new NavbarDal();
            return navbarDal.UpdateNavber(navBar);
        }
    }
}
