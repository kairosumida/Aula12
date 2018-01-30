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
    public class StatusUsuariosController : Controller
    {
        private Aula12Context db = new Aula12Context();

        // GET: StatusUsuarios
        public ActionResult Index()
        {
            return View(db.StatusUsuarios.ToList());
        }

        // GET: StatusUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusUsuario statusUsuario = db.StatusUsuarios.Find(id);
            if (statusUsuario == null)
            {
                return HttpNotFound();
            }
            return View(statusUsuario);
        }

        // GET: StatusUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusUsuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusUsuarioId,NomeStatus")] StatusUsuario statusUsuario)
        {
            if (ModelState.IsValid)
            {
                db.StatusUsuarios.Add(statusUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusUsuario);
        }

        // GET: StatusUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusUsuario statusUsuario = db.StatusUsuarios.Find(id);
            if (statusUsuario == null)
            {
                return HttpNotFound();
            }
            return View(statusUsuario);
        }

        // POST: StatusUsuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusUsuarioId,NomeStatus")] StatusUsuario statusUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusUsuario);
        }

        // GET: StatusUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusUsuario statusUsuario = db.StatusUsuarios.Find(id);
            if (statusUsuario == null)
            {
                return HttpNotFound();
            }
            return View(statusUsuario);
        }

        // POST: StatusUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusUsuario statusUsuario = db.StatusUsuarios.Find(id);
            db.StatusUsuarios.Remove(statusUsuario);
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
