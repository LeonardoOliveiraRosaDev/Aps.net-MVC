using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVCDB.Models;
using System.Diagnostics;

namespace ProjetoMVCDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Definir o objeto referencial de UserManager
        private UserManager<AppUser> userManager;

        // Definir a injeção de dependencia

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userMgr)
        {
            _logger = logger;
            userManager= userMgr;
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
        public async Task<IActionResult> Restrita()
        {
            // Definir uma propriedade/consulta, para receber como valor o nome/username do conjunto de dados que acessa a área restrita
            AppUser recUsuario = await userManager.GetUserAsync(HttpContext.User);

            // Criar uma propriedade para receber como valor uma mensagem de saudação, juntamente com o nome do usuário
            string mensagem ="Olá " +recUsuario.UserName + " você esta na área restrita do app!";

             return View((object)mensagem);
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}