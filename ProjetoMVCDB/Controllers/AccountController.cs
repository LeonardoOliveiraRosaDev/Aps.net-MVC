using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVCDB.Models;

namespace ProjetoMVCDB.Controllers
{
    [Authorize] // ESTE ATRIBUITO FAZ COM QUE TODAS AS ESTRUTURAS DE INSTRUÇÕES LÓGICAS FIQUEM INACESSIVEIS PARA QUALQUER TENTATIVA DE MANIPULAÇÃO EXTERNA -> OU SEJA SEM A DEVIDA AUTORIZAÇÃO.
    public class AccountController : Controller
    {

        // Definir os "auxiliadores" - são os objetos referenciais gerados a partir das referencias de instancias praticadas com classe embarcadas com origem nas dependencias instaladas no projeto.
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        // Estabelecer um construtor customizado para esta classe:
        // Objetivos desta implementação: Criar um elemento publico para "lidar" com as props privadas; estabelecer as injeções de dependencias necessárias - com origem nos recursos assiciados aos objetos referenciais gerados acima; priorizar a inicialização destes recursos.
        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            userManager= userMgr;
            signInManager = signinMgr;
        }

        // definir as actions para a estrutura de login.
        [AllowAnonymous] // ESTE ATRIBUTO PERMITE QUE O USUARIO ACESSE AS ACTIONS NECESSÁRIAS PARA QUE MESMO RESTRINGINDO A CLASSE CONTROLLER - OS PROCESSOS DE AUTENTICAÇÃO/AUTORIZAÇÃO FUNCIONEM.

        public IActionResult Login (string returnUrl)
        {
            // definir um objeto com uma instancia direta a partir do model Login.
            Login login = new Login();
            // fazer uso deste objeto.
            login.ReturnUrl = returnUrl;

            return View(login);
        }

        // sobrecarga do método/action com o objetivo de realizar a autentição.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        // definir - explicitamente - a tarefa assincrona para efeturar a autenticação.
        public async Task<IActionResult> Login(Login login)
        {
            // observar - a partir de uma verificação se o ModelState é válido.
            if (ModelState.IsValid)
            {
                // estabelecer uma consulta para que o valor da propr Email informado pelo usuário - seja devidamente identificada.
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);


                // avaliar a consulta
                if (appUser != null)
                {
                    // Se existe algum usuário logado com credenciais semelhantes, este login será, então desconectado.
                    // Login solicitado pode ocorrer com plenitude.
                    await signInManager.SignOutAsync();

                    // Agora, neste próximo passo, o sitema recebera os dados para a autenticação.
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                    // manipular as mensagem de sucesso ou falha.
                    if (result.Succeeded)
                    {
                        return Redirect(login.ReturnUrl ?? "/Home/Restrita");
                    }      
                }
                ModelState.AddModelError(nameof(login.Email), "Seu login falhou!");

            }
            return View(login);
        }

        // Definir a action que será responsavel pelo processo de desconexão do usuario da área restrita.
        public async Task<IActionResult> Logout()
        {
            // Se existe algum usuário logado com credenciais semelhantes, este login será, então desconectado.
            // E direcionado para a View Index do Projeto
            await signInManager.SignOutAsync();

            // Definindo o redirecionamento
            // Estabelecer uma relação indireta entre o AccountController e a View Index do projeto.
            return RedirectToAction("Index", "Home");
        }

    }
}
