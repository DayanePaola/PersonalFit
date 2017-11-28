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
{[Authorize]
    public class ProfessorController : Controller
    {
        private Context db = new Context();

        // GET: Professor
        public ActionResult Index()
        {
            var professor = db.Professor.Include(p => p.Usuario);
            return View(professor.ToList());
        }

        // GET: Professor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorModel professorModel = db.Professor.Find(id);
            if (professorModel == null)
            {
                return HttpNotFound();
            }
            return View(professorModel);
        }

        // GET: Professor/Create
        public ActionResult Create(int idUsuario)
        {
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login");
            ViewData["idUsuario"] = idUsuario;
            return View();
        }

        // POST: Professor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UsuarioFK,Nome,Cpf,Cref")] ProfessorModel professorModel, int idUsuario)
        {
            if (ModelState.IsValid)
            {
                professorModel.UsuarioFK = idUsuario;
                professorModel.Cpf = Util.Validation.RemCaracteresEsp(professorModel.Cpf, true);

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
            if (ModelState.IsValid)
            {
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorModel professorModel = db.Professor.Find(id);
            if (professorModel == null)
            {
                return HttpNotFound();
            }
            return View(professorModel);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfessorModel professorModel = db.Professor.Find(id);
            db.Professor.Remove(professorModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
