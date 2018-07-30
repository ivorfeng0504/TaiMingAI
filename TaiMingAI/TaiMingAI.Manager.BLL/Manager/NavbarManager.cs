using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaiMingAI.Manager.Model;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.BLL
{
    public class NavbarManager
    {
        #region +首页左侧菜单：获取导航数据
        /// <summary>
        /// 获取导航json数据
        /// </summary>
        /// <param name="userInfo">登录用户信息</param>
        /// <returns>导航json</returns>
        public string GetNavbarJson(AdministratorDto userInfo)
        {
            if (userInfo == null || string.IsNullOrEmpty(userInfo.Role)) return string.Empty;

            RoleBll roleBll = new RoleBll();
            //获取权限范围
            List<int> limits = roleBll.GetRoleLimitsByIds(userInfo.Role);
            var navberList = GetNavbarListByLimits(limits);
            return JsonHelper.ToJson(navberList);
        }
        /// <summary>
        /// 获取导航列表_根据权限范围
        /// </summary>
        /// <param name="limits">权限范围</param>
        /// <returns>导航列表List_NavbarJson</returns>
        private List<NavbarDto> GetNavbarListByLimits(List<int> limits)
        {
            var newNavberList = new List<NavbarDto>();
            if (limits.Count() == 0) return newNavberList;

            NavbarBll navbarBll = new NavbarBll();
            var navbarDtoList = navbarBll.GetNavbarDtoList(false);
            if (navbarDtoList == null || navbarDtoList.Count == 0) return newNavberList;
            navbarDtoList = navbarDtoList.FindAll(x => limits.Contains(x.Id));

            var parentList = navbarDtoList.FindAll(x => x.ParentId == 0).OrderBy(x => x.Sort).ToList();
            foreach (var parent in parentList)
            {
                GetNavberJson(parent, navbarDtoList);
                newNavberList.Add(parent);
            }
            return newNavberList;
        }
        #endregion

        #region +导航管理页面：获取导航列表
        /// <summary>
        /// 导航管理页面 - 导航列表
        /// </summary>
        /// <param name="dic">父导航下拉列表数据源</param>
        /// <returns>导航列表JSON数据</returns>
        public string GetNavbarJson(out string dic)
        {
            Dictionary<int, string> dicNavbar;
            var navberList = GetNavbarList(out dicNavbar);
            dic = JsonHelper.ToJson(dicNavbar.Select(x => new
            {
                key = x.Key,
                value = x.Value
            }));
            return JsonHelper.ToJson(navberList);
        }
        private List<NavbarDto> GetNavbarList(out Dictionary<int, string> dic)
        {
            NavbarBll navbarBll = new NavbarBll();
            var newNavberList = new List<NavbarDto>();
            dic = new Dictionary<int, string>();

            var navbarDtoList = navbarBll.GetNavbarDtoList(true);
            if (navbarDtoList == null || navbarDtoList.Count == 0) return newNavberList;

            foreach (var item in navbarDtoList)
            {
                if (!item.IsShow) continue;
                dic.Add(item.Id, item.title);
            }

            var parentList = navbarDtoList.FindAll(x => x.ParentId == 0).OrderBy(x => x.Sort).ToList();
            foreach (var parent in parentList)
            {
                GetNavberJson(parent, navbarDtoList);
                newNavberList.Add(parent);
            }
            return newNavberList;
        }
        #endregion

        #region -导航数据递归重组
        /// <summary>
        /// 递归重组 导航数据
        /// </summary>
        /// <param name="nav">父节点</param>
        /// <param name="navbarList">导航数据</param>
        private void GetNavberJson(NavbarDto nav, List<NavbarDto> navbarList)
        {
            var childerList = navbarList.FindAll(x => x.ParentId == nav.Id);
            if (childerList == null || childerList.Count == 0)
            {
                return;
            }
            nav.children = childerList.OrderBy(x => x.Sort).ToList();
            foreach (var childer in childerList)
            {
                GetNavberJson(childer, navbarList);
            }
        }
        #endregion

        #region 提交导航菜单数据
        public ControllerResult SubmitNavber(Navbar navBar)
        {
            ControllerResult result = new ControllerResult();
            try
            {
                if (navBar == null || string.IsNullOrWhiteSpace(navBar.title))
                {
                    result.DefaultError("传入数据不完整！");
                    return result;
                }

                if (!string.IsNullOrWhiteSpace(navBar.icon))
                {
                    navBar.icon = HttpUtility.UrlDecode(navBar.icon);
                }
                if (!string.IsNullOrWhiteSpace(navBar.href))
                {
                    navBar.href = HttpUtility.UrlDecode(navBar.href);
                }

                NavbarBll navbarBll = new NavbarBll();
                bool flag = navBar.Id == 0
                    ? navbarBll.InsertNavber(navBar)
                    : navbarBll.UpdateNavber(navBar);

                if (flag)
                {
                    string dic;
                    var json = GetNavbarJson(out dic);
                    result.DefaultSuccess(dic + "$#$" + json);
                }
                else
                {
                    result.DefaultError();
                }
                return result;
            }
            catch (Exception ex)
            {
                result.DefaultError("保存导航菜单出现异常", ex.Message);
                return result;
            }
        }
        #endregion

        #region +角色页面:获取导航列表(zTree格式)
        /// <summary>
        /// 编辑角色页面获取导航列表
        /// </summary>
        /// <returns>导航列表Json字符串</returns>
        public string EditRoleGetNavbarJson()
        {
            var resultJson = string.Empty;
            try
            {
                NavbarBll navbarBll = new NavbarBll();
                var navbarList = navbarBll.GetNavbarDtoList(false);
                if (navbarList == null || navbarList.Count == 0) return resultJson;

                List<int> parentIds = navbarList.Where(x => x.ParentId == 0).Select(x => x.Id).ToList();
                List<int> ids = new List<int>();
                ids.AddRange(parentIds);
                foreach (var id in parentIds)
                {
                    GetShowNavberIdsByParent(ids, id, navbarList);
                }
                navbarList = navbarList.FindAll(x => ids.Contains(x.Id));

                var zTree = navbarList.Select(x => new
                {
                    id = x.Id,
                    pId = x.ParentId,
                    name = x.title,
                    open = true
                });
                resultJson = JsonHelper.ToJson(zTree);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogFormat(ex, "获取-编辑角色页面获取导航列表异常：{0}", ex.Message);
            }
            return resultJson;
        }

        private void GetShowNavberIdsByParent(List<int> ids, int parentId, List<NavbarDto> navbarList)
        {
            var childerList = navbarList.FindAll(x => x.ParentId == parentId);
            if (childerList == null || childerList.Count == 0)
            {
                return;
            }
            ids.AddRange(childerList.Select(x => x.Id).ToList());
            foreach (var childer in childerList)
            {
                GetShowNavberIdsByParent(ids, childer.Id, navbarList);
            }
        }
        #endregion
    }
}
