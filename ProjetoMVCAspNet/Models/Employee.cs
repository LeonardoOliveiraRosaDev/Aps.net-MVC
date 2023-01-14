using System.ComponentModel.DataAnnotations;

namespace ProjetoMVCAspNet.Models
{
    public class Employee
    {

        // implementação validações para o preenchimento dos inputs da
        // view e atribuição dos valores a estas props

        //preenchimento obrigatórios
        [Required(ErrorMessage = " -> Por favor, informe seu nome!")]
        public string? Name { get; set; }

        [Range(16,100, ErrorMessage =" ->Somente idade entre 16 e 100 anos!")]
        public int Age { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage =" -> Valor inválido. Sugestão: valor ou valor.valor valor")]
        public decimal Salary { get; set; }


        public string? Department { get; set; }

        [RegularExpression(@"^[MFO]+$", ErrorMessage ="  -> Selecione ao menos uma opção.")]
        public Char Gen { get; set; }
    }
}
