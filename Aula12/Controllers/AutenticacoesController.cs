using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aula12.Models;

namespace Aula12.Controllers
{
    public class AutenticacoesController : Controller
    {
        private Aula12Context db = new Aula12Context();

        // GET: Autenticacoes
        public ActionResult Index()
        {
            var autenticacoes = db.Autenticacoes.Include(a => a.Usuario);
            return View(autenticacoes.ToList());
        }

        // GET: Autenticacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autenticacao autenticacao = db.Autenticacoes.Find(id);
            if (autenticacao == null)
            {
                return HttpNotFound();
            }
            return View(autenticacao);
        }

        // GET: Autenticacoes/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome");
            return View();
        }

        // POST: Autenticacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutenticacaoId,Data,UsuarioId")] Autenticacao autenticacao)
        {
            if (ModelState.IsValid)
            {
                db.Autenticacoes.Add(autenticacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", autenticacao.UsuarioId);
            return View(autenticacao);
        }

        // GET: Autenticacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autenticacao autenticacao = db.Autenticacoes.Find(id);
            if (autenticacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", autenticacao.UsuarioId);
            return View(autenticacao);
        }

        // POST: Autenticacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutenticacaoId,Data,UsuarioId")] Autenticacao autenticacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autenticacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", autenticacao.UsuarioId);
            return View(autenticacao);
        }

        // GET: Autenticacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autenticacao autenticacao = db.Autenticacoes.Find(id);
            if (autenticacao == null)
            {
                return HttpNotFound();
            }
            return View(autenticacao);
        }

        // POST: Autenticacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autenticacao autenticacao = db.Autenticacoes.Find(id);
            db.Autenticacoes.Remove(autenticacao);
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
