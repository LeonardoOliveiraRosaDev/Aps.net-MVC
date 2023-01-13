using Microsoft.AspNetCore.Mvc;
using ProjetoMVCAspNet.Models;

namespace ProjetoMVCAspNet.Controllers
{
    public class EmployeeController : Controller
    {
        // esta action sera usada para recuperar todos os registros do repositorio
        public IActionResult Index()
        {
            return View(Repository.AllEmployees);
        }

        //action do método Create()
        // aqui o atribuito requisicao web é [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // agora a action Create precisa enviar para o Repositorio
        // os dados obtidos atraves da view de formulario
        // Praticar a sobrecarga do action/médoto create()
        // para que os dados sejam enviados para o repositorio !
        [HttpPost] //atributo de envio de dados
        public IActionResult Create(Employee employee)
        {
            // estabelecer a expressao de retorno do método
            Repository.Create(employee);
            return View("Thanks", employee);
        }

        // criar a action referente a modalidade de atualizacao/alteração de um registro
        // [HttpGet]
        public IActionResult Update(string N)
        {
            // definir uma consulta para resgatar um registro especifico
            Employee search = Repository.AllEmployees.Where(e => e.Name == N).First();
            return View(search);
        }

        // definir a sobrecarga da action/metodo update
        // com o proposito de criar um novo contexto de registro
        // baseado em um registro ja existente
        [HttpPost] // atributo de envio de dados
        public IActionResult Update(Employee employee, string N)
        {
            Repository.AllEmployees.Where(e => e.Name == N).First().Age = employee.Age;
            Repository.AllEmployees.Where(e => e.Name == N).First().Salary = employee.Salary;
            Repository.AllEmployees.Where(e => e.Name == N).First().Department = employee.Department;
            Repository.AllEmployees.Where(e => e.Name == N).First().Gen = employee.Gen;
            Repository.AllEmployees.Where(e => e.Name == N).First().Name = employee.Name;
            //return View();
            return RedirectToAction("Index");
        }
    }
}
