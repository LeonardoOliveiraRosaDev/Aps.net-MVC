using System.ComponentModel.DataAnnotations;

namespace ProjetoMVCDB.Models
{
    public class User
    {
        // definir um conjunto de props para compor o model 
        // serão 3 props
        // definir algumas validações
        [Required(ErrorMessage = "Por favor, informe um nome.")]
        public string? Nome { get; set; }
        [Required]          //  leonardo_mail@gmail.com
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]                       
        public string? Email { get; set; }
        [Required (ErrorMessage ="Por favor, informe uma senha. Pode ser simples! :)")]
        public string? Password { get; set; }
    }
}
