using Microsoft.AspNetCore.Mvc;
using ProjetoMVCAspNet.Models;
using System.Diagnostics;

namespace ProjetoMVCAspNet.Controllers
{
    // aqui esta sendo praticador o mecanismo de herança.
    public class HomeController : Controller
    {
        // propriedade privada que servirá como um elemento de informação
        // e execução dos elementos que compõe a Subclasse HomeControler
        private readonly ILogger<HomeController> _logger;

        // método especial construtor da classe
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // 1º Método que retorna algo = ACTION
        public IActionResult Index()
        {
            // retornando um método !
            return View();
        }

        // 2º Método que retorna algo = ACTION
        public IActionResult Privacy()
        {
            // retornando um método
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}