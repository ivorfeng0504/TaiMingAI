using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.Manager.BLL;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.Controllers.Manager
{
    public class NavbarController : BaseController
    {
        public ActionResult Index()
        {
            NavbarManager navbarManager = new NavbarManager();
            string dic;
            ViewBag.NavBarJson = navbarManager.GetNavbarJson(out dic);
            ViewBag.NavBarDic = dic;
            return View();
        }

        [HttpPost]
        public JsonResult SubmitNavbar(NavbarDto navBar)
        {
            NavbarManager navbarManager = new NavbarManager();
            var result = navbarManager.SubmitNavber(navBar);
            return Json(result);
        }
    }
}