using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.Manager.Models;
using TaiMingAI.Tools;

namespace TaiMingAI.Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            NavBar navBar1 = new NavBar
            {
                title = "内容管理",
                icon = "&#xe634;",
                spread = false,
                children = new List<NavBar>
                {
                    new NavBar{
                         title= "文章列表",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    },
                     new NavBar{
                         title= "图片管理",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    },
                      new NavBar{
                         title= "其他页面",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    },
                }
            };
            NavBar navBar2 = new NavBar
            {
                title = "用户中心",
                icon = "&#xe634;",
                spread = false,
                children = new List<NavBar>
                {
                    new NavBar{
                         title= "用户中心",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    },
                     new NavBar{
                         title= "会员等级",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    }
                }
            };
            NavBar navBar3 = new NavBar
            {
                title = "系统基本参数",
                icon = "&#xe634;",
                spread = false,
                children = new List<NavBar>
                {
                    new NavBar{
                         title= "系统日志",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    },
                     new NavBar{
                         title= "友情链接",
                         icon= "&#xe634;",
                         spread=false,
                         href="https://home.cnblogs.com/"
                    }
                }
            };
            var navbars = new List<NavBar> { navBar1, navBar2, navBar3 };
            ViewBag.NavBar = JsonHelper.ToJson(navbars);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}