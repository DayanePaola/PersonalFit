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
{
    [Authorize]
    public class TreinoController : Controller
    {
        private Context db = new Context();

        // GET: Treino
        public ActionResult Index()
        {
            List<TreinoModel> treino = null;

            if (System.Web.HttpContext.Current.User.IsInRole("Aluno"))
                treino = db.Treino.Where(x => x.Aluno.Usuario.Login == User.Identity.Name).ToList();
            else if (System.Web.HttpContext.Current.User.IsInRole("Professor"))
                treino = db.Treino.Where(x => x.Aluno.Professor.Usuario.Login == User.Identity.Name).ToList();
            else
                treino = db.Treino.Include(t => t.Aluno).ToList();

            return View(treino.ToList());
        }

        // GET: Treino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreinoModel treinoModel = db.Treino.Find(id);
            if (treinoModel == null)
            {
                return HttpNotFound();
            }
            return View(treinoModel);
        }

        // GET: Treino/Create
        [Authorize(Roles = "Professor")]
        public ActionResult Create()
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            ViewBag.AlunoFK = new SelectList(db.Aluno, "Id", "Nome");
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public ActionResult Create([Bind(Include = "Id,AlunoFK,Situacao,DataCadastro")] TreinoModel treinoModel)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Treino.Add(treinoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlunoFK = new SelectList(db.Aluno, "Id", "Nome", treinoModel.AlunoFK);
            return View(treinoModel);
        }

        // GET: Treino/Edit/5
        [Authorize(Roles = "Professor")]
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

            TreinoModel treinoModel = db.Treino.Find(id);
            if (treinoModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.AlunoFK = new SelectList(db.Aluno, "Id", "Nome", treinoModel.AlunoFK);
            return View(treinoModel);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public ActionResult Edit([Bind(Include = "Id,AlunoFK,Situacao,DataCadastro")] TreinoModel treinoModel)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(treinoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlunoFK = new SelectList(db.Aluno, "Id", "Nome", treinoModel.AlunoFK);
            return View(treinoModel);
        }

        // GET: Treino/Delete/5
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int? id)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TreinoModel treinoModel = db.Treino.Find(id);
            if (treinoModel == null)
            {
                return HttpNotFound();
            }

            return View(treinoModel);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            TreinoModel treinoModel = db.Treino.Find(id);
            db.Treino.Remove(treinoModel);
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
