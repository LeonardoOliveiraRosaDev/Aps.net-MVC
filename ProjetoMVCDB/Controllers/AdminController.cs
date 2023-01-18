using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVCDB.Models;

namespace ProjetoMVCDB.Controllers
{
    public class AdminController : Controller
    {
        // definir uma propriedade private prop, para criar a referência de auxilio
        // na manipulação de dados com o qual o Controller vai lidar
        private UserManager<AppUser> userManager;

        // definir o elemento publico que possibilita o acesso a prop private. ( Com um construtor)
        public AdminController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        // esta action será usada para fazer com que o registro seja todos lidos
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        // definir a action para a inserção de dados
        // do CRUD, será implementada a funcionalidade Create

        // retornando a View de uma forma diferente como mesmo resultado
        // public IActionResult Create() { return View(); }
        public ViewResult Create() => View();

        /*====================================================================
                        O model User é responsável por dados 
                        de cadastro de um determinado usuário

                      O model AppUser é o responsável por receber
                      do model User os dados necessários para os
                      processos de autenticação e autorização de
                      acesso restrito
         ====================================================================*/



        // definir o action de envio dos dados para a base
        [HttpPost]
        // definir explicitamente - uma tarefa assincrona
        public async Task<IActionResult> Create(User user)
        {
            // verificação se o Model State é válido
            if(ModelState.IsValid)
            {
                // definir um objeto - a partir do model AppUser para fins
                // de autenticação e autorização de acesso a área restrita
                // que ocorrerão posteriormente
                AppUser appUser = new AppUser
                {
                    UserName = user.Nome,
                    Email = user.Email
                };
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                // fazer das instruções embarcadas do sucesso ou do erro ao executar as instruçõs acima
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            return View(user);
        }
    }
}
