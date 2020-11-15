using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AlagoasTour.Models;
using Microsoft.AspNetCore.Http;

namespace AlagoasTour.Controllers
{
    public class EntrarController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login l)
        {
            LoginRepository loginRepo = new LoginRepository();
            Login login = loginRepo.ValidaLogin(l);
            if(login != null && login.perfil == "admin")
            {
                ViewBag.Mensagem = "Você está logado como ADM";
                HttpContext.Session.SetInt32("idLogin", login.id);
                HttpContext.Session.SetString("emailLogin", login.email);
                HttpContext.Session.SetString("perfilLogin", login.perfil);
                HttpContext.Session.SetString("nomeLogin", login.nome);
                return RedirectToRoute(new {controller = "Home", Action = "Index"});
            }
            else if(login != null)
            {
                ViewBag.Mensagem = "Você está logado como funcionario";
                HttpContext.Session.SetInt32("idLogin", login.id);
                HttpContext.Session.SetString("emailLogin", login.email);
                HttpContext.Session.SetString("perfilLogin", login.perfil);
                HttpContext.Session.SetString("nomeLogin", login.nome);
                return RedirectToRoute(new {controller = "Home", Action = "Dashboard"});
            }
            else{
                ViewBag.Mensagem = "Dados incorretos, repita o processo";
                return View();
            }
        }

        public IActionResult Cadastro()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null || HttpContext.Session.GetString("perfilLogin") != "admin")
                return RedirectToAction("Login");
            return View();
        }

        
        [HttpPost]
        public IActionResult Cadastro(Login l)
        {
            LoginRepository loginRepo = new LoginRepository();
            loginRepo.Cadastra(l);
            ViewBag.Mensagem = "Cadastro feito com sucesso!";
            ModelState.Clear();
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//limpa toda a sessão
            return Redirect("Login");
        }
    }
}