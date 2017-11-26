using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var teste3 = System.Web.HttpContext.Current.User.IsInRole("Admin");

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Erro()
        {
            return View();
        }
    }
}