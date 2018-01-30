using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula12.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }
    }
}