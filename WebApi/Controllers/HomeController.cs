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
        // GET: Home
        public ActionResult Index()
        {
            var auth = System.Web.HttpContext.Current.User.Identity.Name;

            var teste = System.Web.HttpContext.Current.User.IsInRole("Aluno");
            var teste2 = System.Web.HttpContext.Current.User.IsInRole("Professor");

            return View();
        }
    }
}