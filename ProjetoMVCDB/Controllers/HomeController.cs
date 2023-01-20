using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVCDB.Models;
using System.Diagnostics;

namespace ProjetoMVCDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        
        // definir abaixo o atributo de restrição de acesso as n estruturas dessa Action
        [Authorize]
        // definir uma action que vai lidar com as ocorrencias referentes a área restrita do web app
        public IActionResult Restrita()
        {
            return View((object)"Olá eu sou a área restrita do projeto!");
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}