using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Web.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        private Context db = new Context();

        // GET: Professor
        public ActionResult Index()
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Admin"))
            {
                return RedirectToAction("Erro", "Home");
            }

            var professor = db.Professor.Include(p => p.Usuario);
            return View(professor.ToList());
        }

        // GET: Professor/Details/5
        public ActionResult Details(string user)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor") || !(user == User.Identity.Name))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorModel professorModel = db.Professor.Where(x => x.Usuario.Login == user).SingleOrDefault();
            if (professorModel == null)
            {
                return HttpNotFound();
            }
            return View(professorModel);
        }

        // GET: Professor/Create
        [AllowAnonymous]
        public ActionResult Create(int idUsuario)
        {
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login");
            ViewData["idUsuario"] = idUsuario;
            return View();
        }

        // POST: Professor/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UsuarioFK,Nome,Cpf,Cref")] ProfessorModel professorModel, int idUsuario)
        {
            if (ModelState.IsValid)
            {
                professorModel.UsuarioFK = idUsuario;
                professorModel.Cpf = Util.Validation.RemCaracteresEsp(professorModel.Cpf, true);

                var cpfValid = Util.Validation.ValidaCPF(professorModel.Cpf);

                if (!cpfValid)
                {
                    ModelState.AddModelError("Erro", "CPF inválido!");
                    ViewData["idUsuario"] = idUsuario;
                    return View(professorModel);
                }

                db.Professor.Add(professorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", professorModel.UsuarioFK);
            ViewData["idUsuario"] = idUsuario;
            return View(professorModel);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorModel professorModel = db.Professor.Find(id);
            if (professorModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", professorModel.UsuarioFK);
            return View(professorModel);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UsuarioFK,Nome,Cpf,Cref")] ProfessorModel professorModel)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (ModelState.IsValid)
            {
                professorModel.Cpf = Util.Validation.RemCaracteresEsp(professorModel.Cpf, true);

                var cpfValid = Util.Validation.ValidaCPF(professorModel.Cpf);

                if (!cpfValid)
                {
                    ModelState.AddModelError("Erro", "CPF inválido!");
                    ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", professorModel.UsuarioFK);
                    return View(professorModel);
                }

                db.Entry(professorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", professorModel.UsuarioFK);
            return View(professorModel);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int? id)
        {

            return RedirectToAction("Erro", "Home");


            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ProfessorModel professorModel = db.Professor.Find(id);
            //if (professorModel == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(professorModel);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            return RedirectToAction("Erro", "Home");

            //ProfessorModel professorModel = db.Professor.Find(id);
            //db.Professor.Remove(professorModel);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
