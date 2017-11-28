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
    public class AlunoController : Controller
    {
        private Context db = new Context();

        // GET: Aluno
        public ActionResult Index()
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Professor"))
            {
                return RedirectToAction("Erro", "Home");
            }

            var aluno = db.Aluno.Where(x => x.Professor.Usuario.Login == User.Identity.Name).ToList();
            return View(aluno);
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModel alunoModel = db.Aluno.Find(id);
            if (alunoModel == null)
            {
                return HttpNotFound();
            }
            return View(alunoModel);
        }

        // GET: Aluno/Create
        public ActionResult Create(int idUsuario)
        {
            ViewBag.ProfessorFK = new SelectList(db.Professor, "Id", "Nome");
            ViewData["idUsuario"] = idUsuario;
            return View();
        }

        // POST: Aluno/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cpf,Idade,Peso,Altura,Objetivo,ProfessorFK")] AlunoModel alunoModel, int idUsuario)
        {
            if (ModelState.IsValid)
            {
                alunoModel.UsuarioFK = idUsuario;
                alunoModel.Cpf = Util.Validation.RemCaracteresEsp(alunoModel.Cpf, true);

                db.Aluno.Add(alunoModel);
                db.SaveChanges();
                return RedirectToAction("Login", "Usuario");
            }

            ViewBag.ProfessorFK = new SelectList(db.Professor, "Id", "Nome", alunoModel.ProfessorFK);
            ViewData["idUsuario"] = idUsuario;
            return View(alunoModel);
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole("Aluno"))
            {
                return RedirectToAction("Erro", "Home");
            }

            var usuarioLogado = db.Aluno.Where(x => x.Id == id).Select(x => x.Usuario).SingleOrDefault();

            if (User.Identity.Name != usuarioLogado.Login)
            {
                return RedirectToAction("Erro", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AlunoModel alunoModel = db.Aluno.Find(id);
            if (alunoModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfessorFK = new SelectList(db.Professor, "Id", "Nome", alunoModel.ProfessorFK);
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", alunoModel.UsuarioFK);
            return View(alunoModel);
        }

        // POST: Aluno/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cpf,Idade,Peso,Altura,Objetivo,UsuarioFK,ProfessorFK")] AlunoModel alunoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorFK = new SelectList(db.Professor, "Id", "Nome", alunoModel.ProfessorFK);
            ViewBag.UsuarioFK = new SelectList(db.Usuario, "Id", "Login", alunoModel.UsuarioFK);
            return View(alunoModel);
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoModel alunoModel = db.Aluno.Find(id);
            if (alunoModel == null)
            {
                return HttpNotFound();
            }
            return View(alunoModel);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunoModel alunoModel = db.Aluno.Find(id);
            db.Aluno.Remove(alunoModel);
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
