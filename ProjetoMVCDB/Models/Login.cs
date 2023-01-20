using System.ComponentModel.DataAnnotations;

namespace ProjetoMVCDB.Models
{
    // Definição do model para a ação de autenticação/autorização de acesso !
    public class Login
    {
        //definir props obrigatorias com as validaçõs necessárias
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }

        //por padrão o Asp.Net vai adotar a seguinte estrutura de URL: http://localhost:xxxxx/Account/Login
    }
}
