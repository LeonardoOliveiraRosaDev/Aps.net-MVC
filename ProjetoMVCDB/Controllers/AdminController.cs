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

        // definir uma nova prop private para auxiliar na recuperação e "tradução" do elemento hash para que a senha possa ser sobrescrita/atualizada
        private IPasswordHasher<AppUser> passwordHasher;

        // definir o elemento publico que possibilita o acesso a prop private. ( Com um construtor)
        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> pswHash)
        {
            userManager = usrMgr;
            passwordHasher = pswHash;
        }


        /*
         ===================================================================
                               CRIAÇÕS DAS ACTIONS
         ================================================================== 
         */
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

        /*==================================================================
                        O model User é responsável por dados 
                        de cadastro de um determinado usuário

                      O model AppUser é o responsável por receber
                      do model User os dados necessários para os
                      processos de autenticação, e autorização e
                      atualização de acesso restrito
                      =================================================================*/

         

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


        // ACTION UPDATE - responsavel pela alteração/atualização dos dados de um registro

        // definir a action que retorna a consulta que será realziada a partir do valor dado ao parametro da action
        public async Task<IActionResult> Update(string id)
        {
            // definir a consulta para verificar a existencia do registro
            AppUser user = await userManager.FindByIdAsync(id);

            // avaliar a consulta
            if (user != null)
            {
                return View(user);
            }
            else
                return View("Index");
        }

        // praticar a sobrecarga da action/método Update para que seja possivel REenviar os dados para a base
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            // repetir a consulta
            AppUser user = await userManager.FindByIdAsync(id);

            // é necessario avaliar a consulta e, se tudo for verdadeiro, empreender a atualização dos valores das propriedades indicadas
            if (user != null)
            {
                // avaliação do valor para atualização da prop email
                if (!string.IsNullOrEmpty(email))
                {
                    // acessar  - do model User - a prop Email
                    user.Email = email;
                }
                else
                    ModelState.AddModelError("", "O campo email não pode ser vazio.");


                // avaliação do valor para atualização da prop password
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                }
                else
                    ModelState.AddModelError("", "O campo senha/password não pode ser vazio.");

                // "juntar" os dados de email e senha para envia-los e salva-los na base
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    // definição do envio de dados
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                        Errors(result);
                }

            }
            else
                ModelState.AddModelError("", "Usuario não encontrado.");
            return View(user);
        }

        //definir o método void Errors()
        private void Errors(IdentityResult result)
        {
            // estabelecer um loop para iterar sobre um ou mais erros que, eventualamente, possam ocorrer com a funcionalidade Update
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


    }
}
