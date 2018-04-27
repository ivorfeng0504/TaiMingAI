using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TaiMingAI.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket("",true,1);
            FormsAuthentication.Encrypt(ticket);
            return View();
        }
    }
}
