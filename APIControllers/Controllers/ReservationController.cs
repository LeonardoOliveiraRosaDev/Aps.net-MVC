using APIControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIControllers.Controllers
{
    // para definir um controller para uma API são necessários recursos especificos para a construção/definição/Identificação de uam API
    [ApiController] // este atributo defini o "papel" que este controller assume dentro do projeto

    [Route("api/[controller]")] // esta é a rota para acesso as funcionalidades da API
                                //http://localhost:xxxxx/api/Reservation
    public class ReservationController : ControllerBase
    {
        // definir um "auxiliar" - objeto referencial - para manipular os dados
        private IRepository repository;

        // praticar a injeção de dependencia
        public ReservationController(IRepository repo)
        {
            repository = repo;
        }

        // 1ª operação: resgatar todos os registros da base
        [HttpGet]
        public IEnumerable<Reservation> get() => repository.Reservations;

        // 2ª operação: resgatar um único registro devidamente identificado
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            // avaliar o valor do parametro
            if (id == 0)
            {
                // esta avaliação resultará em um valor false
                // isto é um problema - por que o valor do parametro, provavelmente, não existe
                return BadRequest("Por favor, informe um valor para que a requisição seja atendida");
            }
            return Ok(repository[id]);
        }
        // definir a tarefa de inserção de dados
        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
            // definir o processo de inserção
            repository.AddReservation(new Reservation
            {
                Name = res.Name,
                StartLocation= res.StartLocation,
                EndLocation= res.EndLocation
            });
        // definir a tarefa de atualização de dados
        [HttpPut]
        public Reservation Put([FromFormAttribute] Reservation res) => repository.UpdateReservation(res);

        // definir a tarefa de exclusão dos dados
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
