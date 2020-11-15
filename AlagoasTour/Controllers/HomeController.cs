using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AlagoasTour.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null)
                return RedirectToRoute(new {controller = "Entrar", action = "Login"});
            else if(HttpContext.Session.GetString("perfilLogin") != "admin")
                return RedirectToRoute(new {controller = "Home", action = "Dashboard"});
                    
            return View();
        }
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("idLogin") == null && HttpContext.Session.GetString("perfilLogin") == "admin")
                return Redirect("Index");
            return View();
        }
    }
}
