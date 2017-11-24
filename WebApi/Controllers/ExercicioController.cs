using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace WebApi.Controllers
{[Authorize]
    public class ExercicioController : Controller
    {
        private Context db = new Context();

        // GET: Exercicio
        public ActionResult Index()
        {
            return View(db.Exercicio.ToList());
        }

        // GET: Exercicio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExercicioModel exercicioModel = db.Exercicio.Find(id);
            if (exercicioModel == null)
            {
                return HttpNotFound();
            }
            return View(exercicioModel);
        }

        // GET: Exercicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Equipamento")] ExercicioModel exercicioModel)
        {
            if (ModelState.IsValid)
            {
                db.Exercicio.Add(exercicioModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exercicioModel);
        }

        // GET: Exercicio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExercicioModel exercicioModel = db.Exercicio.Find(id);
            if (exercicioModel == null)
            {
                return HttpNotFound();
            }
            return View(exercicioModel);
        }

        // POST: Exercicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Equipamento")] ExercicioModel exercicioModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercicioModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercicioModel);
        }

        // GET: Exercicio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExercicioModel exercicioModel = db.Exercicio.Find(id);
            if (exercicioModel == null)
            {
                return HttpNotFound();
            }
            return View(exercicioModel);
        }

        // POST: Exercicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExercicioModel exercicioModel = db.Exercicio.Find(id);
            db.Exercicio.Remove(exercicioModel);
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
