using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using static Util.Validation;
using System.Web.Security;

namespace WebApi.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly Context db;

        public UsuarioController()
        {
            db = Singleton.Instance.Context;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = db.Usuario.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuarioModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioModel);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = db.Usuario.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarioModel);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = db.Usuario.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioModel usuarioModel = db.Usuario.Find(id);
            db.Usuario.Remove(usuarioModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UsuarioModel usuario)
        {
            var senhaDecode = GeraMD5.GeraHash(usuario.Senha);
            var result = db.Usuario.Include(x => x.Aluno)
                                   .Include(x => x.Professor)
                                   .Where(x => x.Login == usuario.Login && x.Senha == senhaDecode).SingleOrDefault();

            if (result == null)
            {

            }

            var role = string.Empty;

            if (result.Aluno != null)
                role = "Aluno";
            else if (result.Professor != null)
                role = "Professor";

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario.Login, DateTime.Now, DateTime.Now.AddDays(1), false, role, "/");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
