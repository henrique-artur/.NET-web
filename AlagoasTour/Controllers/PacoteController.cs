using Microsoft.AspNetCore.Mvc;
using AlagoasTour.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace AlagoasTour.Controllers
{
    public class PacoteController : Controller
    {
        public IActionResult Listar()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null)
                return RedirectToRoute(new {controller = "Entrar", action = "Login"});
            PacoteRepository pr = new PacoteRepository();
            List<Pacotes> pacotes = pr.Query();
            return View(pacotes);
        }

        public IActionResult Cadastro()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null)
                return RedirectToRoute(new {controller = "Entrar", action = "Login"});
            return View();
        }

        [HttpPost]
        public IActionResult Confirma(Pacotes p)
        {
            PacoteRepository pr = new PacoteRepository();
            pr.Cadastrar(p);
            ViewBag.Msg = "Pacote cadastrado!";
            return View();
        }

        public IActionResult Excluir()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null)
                return RedirectToRoute(new {controller = "Entrar", action = "Login"});
            return View();
        }

        [HttpPost]
        public IActionResult Excluir(Pacotes p)
        {
            int id = p.id;
            PacoteRepository pr = new PacoteRepository();
            pr.Excluir(id);
            ModelState.Clear();
            ViewBag.msg = "Exclusão feita com sucesso!";
            return View();
        }
    }
}