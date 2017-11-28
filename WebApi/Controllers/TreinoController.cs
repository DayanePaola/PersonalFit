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
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                List<TreinoModel> treino = null;

                if (System.Web.HttpContext.Current.User.IsInRole("Aluno"))
                    treino = db.Treino.Include(i => i.Aluno.Professor).Where(x => x.Aluno.Usuario.Login == User.Identity.Name && x.Situacao == "A").ToList();
                else if (System.Web.HttpContext.Current.User.IsInRole("Professor"))
                    treino = db.Treino.Include(i => i.Aluno).Where(x => x.Aluno.Professor.Usuario.Login == User.Identity.Name && x.Situacao == "A").ToList();
                else
                    treino = db.Treino.Include(t => t.Aluno).ToList();

                return View(treino.ToList());
            }

            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            var treinoAl = db.Treino.Include(i => i.Aluno).Where(x => x.Aluno.Id == id).ToList();
            return View(treinoAl);
        }

        // GET: Treino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<TreinoModel> treino = null;

            if (System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                treino = db.Treino.Where(x => x.Aluno.Professor.Usuario.Login == User.Identity.Name).ToList();

                if (!treino.Any(x => x.Id == id))
                    return RedirectToAction("Erro", "Home");
            }
            else if (System.Web.HttpContext.Current.User.IsInRole("Aluno"))
            {
                treino = db.Treino.Where(x => x.Aluno.Usuario.Login == User.Identity.Name).ToList();

                if (!treino.Any(x => x.Id == id))
                    return RedirectToAction("Erro", "Home");
            }

            TreinoModel treinoModel = db.Treino.Include(i => i.TreinoExercicio).Where(x => x.Id == id).SingleOrDefault();

            foreach (var item in treinoModel.TreinoExercicio)
            {
                item.Exercicio = db.Exercicio.Where(x => x.Id == item.ExercicioFK).SingleOrDefault();
            }

            if (treinoModel == null)
            {
                return HttpNotFound();
            }
            return View(treinoModel);
        }

        // GET: Treino/Create
        public ActionResult Create()
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            ViewBag.AlunoFK = new SelectList(db.Aluno.Where(x => x.Professor.Usuario.Login == User.Identity.Name).ToList(), "Id", "Nome");
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AlunoFK")] TreinoModel treinoModel)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            treinoModel.Situacao = "A";
            treinoModel.DataCadastro = DateTime.Now;

            try
            {
                db.Treino.Add(treinoModel);
                db.SaveChanges();
                return RedirectToAction("CreateTreinoExercicio", new { id = treinoModel.Id });
            }
            catch (Exception)
            {
                ViewBag.AlunoFK = new SelectList(db.Aluno.Where(x => x.Professor.Usuario.Login == User.Identity.Name).ToList(), "Id", "Nome", treinoModel.AlunoFK);
                return View(treinoModel);
            }
        }

        // GET: Treino/Edit/5
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

        public ActionResult CreateTreinoExercicio(int? id)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ExercicioFK = new SelectList(db.Exercicio.ToList(), "Id", "Nome");
            ViewData["idTreino"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateTreinoExercicio([Bind(Include = "Id,ExercicioFK,Peso,Repeticoes")]TreinoExercicioModel treinoExModel, int idTreino, bool chk_continua)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            treinoExModel.TreinoFK = idTreino;

            if (ModelState.IsValid)
            {
                db.TreinoExercicio.Add(treinoExModel);
                db.SaveChanges();

                if (chk_continua)
                    return RedirectToAction("CreateTreinoExercicio", new { id = idTreino });

                return RedirectToAction("Details", "Treino", new { id = idTreino });
            }

            ViewBag.ExercicioFK = new SelectList(db.Exercicio.ToList(), "Id", "Nome", treinoExModel.ExercicioFK);
            ViewData["idTreino"] = idTreino;
            return View(treinoExModel);
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
