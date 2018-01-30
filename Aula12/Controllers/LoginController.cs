using Aula12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula12.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Email,Senha")] LoginValidator login)
        {

            
            Aula12Context db = new Aula12Context();
            // GET: Login
          
                var usuario = db.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (usuario != null)
                {

                    db.Autenticacoes.Add(new Autenticacao()
                    {
                        UsuarioId = usuario.UsuarioId,
                        Data = DateTime.Now
                    });
                    db.SaveChanges();
                    Session["Usuario"] = usuario;
                    return RedirectToAction("Create", "Usuarios");
                }
            else
            {
                ModelState.AddModelError("usuario.Login", "Senha ou email inválidos");
            }
                return View();
        }
    }
}