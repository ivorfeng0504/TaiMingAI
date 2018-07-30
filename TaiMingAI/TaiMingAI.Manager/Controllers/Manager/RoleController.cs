using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiMingAI.Manager.BLL;
using TaiMingAI.Manager.Model;
using TaiMingAI.Manager.Models;

namespace TaiMingAI.Manager.Controllers.Manager
{
    public class RoleController : Controller
    {
        public ActionResult Index()
        {
            NavbarManager navbarBll = new NavbarManager();
            ViewBag.zTreeJson = navbarBll.EditRoleGetNavbarJson();
            return View();
        }

        public JsonResult GetRoleList()
        {
            RoleManager roleManager = new RoleManager();
            ControllerResult<RoleDto> result = roleManager.GetRoleList();
            TableResponse<RoleDto> response = new TableResponse<RoleDto>
            {
                code = result.IsSuccess ? 0 : -1,
                msg = result.Message,
                data = result.List,
                count = result.List.Count
            };
            return Json(response);
        }


        [HttpPost]
        public JsonResult SubmitRole(RoleDto dto)
        {
            RoleManager roleManager = new RoleManager();
            var result = roleManager.SubmitRole(dto);
            return Json(result);
        }
    }
}