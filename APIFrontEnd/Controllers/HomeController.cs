using APIFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace APIFrontEnd.Controllers
{
    // Aqui, neste controller será praticado o mesmo contexto dos projetos mvc anteriores
    // Esta classe não é uma API - é um controller de um projeto MVC que nos retorna uma View(). No entanto, é a partir deste controller que a API é acessada/chamada/consumida/referenciada.
    public class HomeController : Controller
    {

        // definir as actions como Task - Tarefas assincronas/ essas precisam ser definidas de forma explicita

        // Action Index: Responsável por chamar todos os registros da Base - a partir da chamada da API para exibir na View().
        public async Task <IActionResult> Index()
        {
            // aqui, como elemento fundamental, é preciso definir uma estrutura de dados que receba os registro da API
            List<Reservation> reservationList= new List<Reservation>();

            // passos necessários para contruir o acesso/chamada da API e Consumir/Resgatar dados e exibi-los na View()
            // 1º passo: criar um objeto que auxilie na contrução das requisições http.-> TAREFA -> OK
            // 2º passo: Fazer a chamada da API e aguardar a resposta dessa chamada. -> TAREFA -> OK
            // 3º passo: "Envelopar" (no formato Json) a lista no formato adequado para seu transporte. -> TAREFA - OK
            // 4º passo: "Desenvelopar" a resposta da API - que tras os dados para o front e exibi-los na View.

            /* ====== CONSTRUIRNDO O 1º PASSO DAS TAREFAS ======*/

            // aqui sera instanciada uma var apartir da classe embarcada HttpClient - que dentre varias caracteristicas, nos ajuda a fazer uso das requisições Http
            using ( var httpReq = new HttpClient())
            {
                /* ====== CONSTRUIRNDO O 2º PASSO DAS TAREFAS ======*/

                // aqui será definida a chamada/acesso a API
                // definir uma var e atribuir como valor a rotada API para que seja acessada
                using (var call = await httpReq.GetAsync("http://localhost:5012/api/Reservation"))
                {
                    // definir uma nova prop - no formato string - para receber a "resposta" chamada da API
                    string apiR = await call.Content.ReadAsStringAsync();

                    /* ====== CONSTRUIRNDO O 3º E 4º PASSO DAS TAREFAS ======*/

                    // criar uma propriedade para receber como valor o formato adequado para os dados.
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiR);
                }

            }


            // Este é o retorno da View com od dados da lista para serem exibidos na tela.
            return View(reservationList);
        }
        // ACTION GetReservation() - Será responsável por recuperar a partir da chamada da API um úinico registro dede que esteja devidamente identificado pelo valor do paramentro que será, aqui definido.

        // 1º passo: retornar a View() par que todas as operações que serão definidas posteriomente possam ser executadas plenamente.

        public ViewResult GetReservation() => View();

        // Praticar a sobrecarga de GetReservation()

        // 2º passo: criar tarefa assincrona, para resgatar un único registro identificado e armazenado na base
        [HttpPost] // é preciso enviar com a chamada da API o valor referente ao parametro de identificação do regitro.
        public async Task<IActionResult> GetReservation(int id)
        {
            // 3º passo: praticar a instancia da classe/model Reservation para ter acesso as suas props
            Reservation? reservation = new Reservation();

            // 4º passo: praticar a instancia da classe HttpClient
            using (var httpReq = new HttpClient())
            {
                //5º passo: executar a chamada da API; para esta chamada, será passado o parametro id
               using (var call = await httpReq.GetAsync("http://localhost:5012/api/Reservation/" + id))
                {
                    // 6º passo: aguardar a resposta de chamada de API
                    // e verificar se existe valor informado e seu registro correspondente
                    if (call.StatusCode == System.Net.HttpStatusCode.OK) 
                    {
                        string apiR = await call.Content.ReadAsStringAsync();

                        reservation = JsonConvert.DeserializeObject<Reservation>(apiR);
                    }
                    else
                        ViewBag.StatusCode = call.StatusCode;
                }
            }
            // 7º passo: retornar a View() com o registro selecionado 
            return View(reservation);
        }

        // ACTION QUE INSERE DADOS NA BASE - esta action será responsável - recebendo os dados de um form na View - por chamar a API do Back End para que os dados seja inseridos na Base.

        // 1º passo: Definir a Action para retornar a view
        public ViewResult AddReservation() => View();

        // 2º passo: sobrecarga da action AddReservation() o objetivo é criar uma tarefa assincrona, para - chamando a API - armazenar os registros na base.
        [HttpPost]// atributo necessário para enviar dados de uma aplicação para outra
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            // 3º passo: praticar a instancia da classe/model para que seja possivel acessar suas props
            Reservation? bornData = new Reservation();

            // 4º passo: criar a instancia da classe HttpClient que auxilia na criação/definição da requisição http
          
            using (var httpReq = new HttpClient())
            {
                // 5º passo:  uma vez que os dados estejão a disposição da action - é necessário "envelopa-los" para que seja possivel fazer a requisição Http para envia-los ao repositorio.
                StringContent dataContent = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                // 6º passo: Chamar a API e aguardar a resposta
                using (var call = await httpReq.PostAsync("http://localhost:5012/api/Reservation", dataContent ))
                {
                    // aguardar a resposta e atribuila a uma prop
                    string apiR = await call.Content.ReadAsStringAsync();
                    bornData = JsonConvert.DeserializeObject<Reservation>(apiR);
                }
            }

            return View(bornData);
        }



    }

}