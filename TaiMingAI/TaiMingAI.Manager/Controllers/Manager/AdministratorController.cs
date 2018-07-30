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
    public class AdministratorController : Controller
    {
        public ActionResult Index()
        {
            RoleManager roleManager = new RoleManager();
            Dictionary<int, string> roleDic = roleManager.GetRoleDic();
            ViewBag.RoleDic = roleDic;
            return View();
        }

        public JsonResult GetAdminList()
        {
            AdminManager manager = new AdminManager();
            var result = manager.GetAdminList();
            var response = new TableResponse<AdministratorDto>
            {
                code = result.IsSuccess ? 0 : -1,
                msg = result.Message,
                data = result.List,
                count = result.List.Count
            };
            return Json(response);
        }

        public JsonResult SubmitAdmin(AdministratorDto dto)
        {
            AdminManager manager = new AdminManager();
            var result = manager.SubmitAdmin(dto);
            return Json(result);
        }

        public JsonResult UpdateAdminProperty(AdministratorDto dto, AdminProperty property)
        {
            AdminManager manager = new AdminManager();
            var result = manager.UpdateAdminProperty(dto, property);
            return Json(result);
        }
    }
}