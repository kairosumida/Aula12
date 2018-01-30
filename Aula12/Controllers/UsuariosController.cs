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
    public class UsuariosController : Controller
    {
        private Aula12Context db = new Aula12Context();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (!IsAdmin()) { return RedirectToAction("Index", "Inicio", new { msg = "Você não está autorizado" }); }
            if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            var usuarios = db.Usuarios.Include(u => u.StatusUsuario);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (!IsAdmin()) { return RedirectToAction("Index", "Inicio", new { msg = "Você não está autorizado" }); }
            if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            IsAdmin();
            if ((Usuario)Session["Usuario"] !=null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            ViewBag.StatusUsuarioId = new SelectList(db.StatusUsuarios, "StatusUsuarioId", "NomeStatus");
            return View();
        }
        
        // POST: Usuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,Nome,Email,Senha, SenhaRepete, StatusUsuarioId")] Usuario usuario)
        {
            IsAdmin();
            if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            if (usuario.Senha != usuario.SenhaRepete)
            {
                ModelState.AddModelError("usuario.Senha", "Senha não confere");
            }
            if (db.Usuarios.FirstOrDefault(x=>x.Email.Equals(usuario.Email)) != null)
            {
                ModelState.AddModelError("usuario.Email", "Email já cadastrado");
            }
            
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                
                db.SaveChanges();
                return RedirectToAction("Index","Login","");
            }

            ViewBag.StatusUsuarioId = new SelectList(db.StatusUsuarios, "StatusUsuarioId", "NomeStatus", usuario.StatusUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            IsAdmin();
            if ((Usuario)Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Inicio", new { msg = "Não tem um usuario logado para mudar a senha" });
            }
            if ("Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            if (id == null)
            {
                id = ((Usuario)Session["Usuario"]).UsuarioId;
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusUsuarioId = new SelectList(db.StatusUsuarios, "StatusUsuarioId", "NomeStatus", usuario.StatusUsuarioId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,Nome,Email,Senha,SenhaAntiga,NovaSenha,SenhaRepete,DataCadastro,UltimoAcesso,StatusUsuarioId")] Usuario usuario)
        {
            if (!IsAdmin())
            {
                if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                    return RedirectToAction("Index", "Banido");
                if (usuario.SenhaAntiga != usuario.Senha) //O Adm não precisa lembrar da senha antiga, afinal ele nem sabe, não mostrar para o adm a opcao de digitar a senha antiga
                {//O que significa que independente da senha antiga, a nova senha deverá ser trocada pela atual
                    ModelState.AddModelError("usuario.Senha", "Senha invalida");//Isso é o q barra a troca
                }
            }
            if(usuario.NovaSenha == null)
            {
                ModelState.AddModelError("usuario.SenhaBranca", "Senha inválida");
            }
            if (usuario.NovaSenha != usuario.SenhaRepete)//Mesmo o adm deve conferir as 2 senhas
            {
                ModelState.AddModelError("usuario.SenhaRepete", "Senha não confere");
            }
            usuario.Senha = usuario.NovaSenha;
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Inicio", new { msg = "Senha alterada com sucesso" });
            }
            ViewBag.StatusUsuarioId = new SelectList(db.StatusUsuarios, "StatusUsuarioId", "NomeStatus", usuario.StatusUsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAdmin()) { return RedirectToAction("Index", "Inicio", new { msg = "Você não está autorizado" }); }
            if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin()) { return RedirectToAction("Index", "Inicio", new { msg = "Você não está autorizado" }); }
            if ((Usuario)Session["Usuario"] != null && "Bloqueado" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)
                return RedirectToAction("Index", "Banido");
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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

        public bool IsAdmin()
        {
            if (Session["Usuario"] != null)
            {
                if ("Administrador" == ((Usuario)Session["Usuario"]).StatusUsuario.NomeStatus)//Verifica se o usuario logado é um adm
                {
                    ViewBag.IsAdmin = true;
                    return true;
                }
            }
                ViewBag.IsAdmin = false;
            return false;
        }
    }

    
}
