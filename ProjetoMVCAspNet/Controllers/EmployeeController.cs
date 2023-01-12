using Microsoft.AspNetCore.Mvc;

namespace ProjetoMVCAspNet.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        //action do método Create()
        public IActionResult Create()
        {
            return View();
        }
    }
}
