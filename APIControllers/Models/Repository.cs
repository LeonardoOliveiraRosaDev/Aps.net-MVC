using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace APIControllers.Models
{
    // A 1ª coisa a se fazer é praticar o mecanismo de herança entre a classe concreta Repository e a Interface IRepository.
    public class Repository : IRepository
    {
        // Definição de uma prop que será um "auxiliador" - objeto referencial - para criar a estrutura adequada para armazenar os dados.
        private Dictionary<int, Reservation> items;

        // Definir um contrutor padrão que "prioriza" a inserção de registro no dicionario para armazenamento
        public Repository()
        {
            // definir a instacia do dicionario
            // injeção de dependencia
            items = new Dictionary<int, Reservation>();

            // Definição do conjunto de dados numa lista
            new List<Reservation>
            {

            // Praticar a instancia da classe Reservation  para criar alguns dados iniciais, e armazena-los
            new Reservation { Id = 1, Name = "Anderson", StartLocation = "São Paulo - Centro", EndLocation = "Miami" },
            new Reservation { Id = 2, Name = "Amanda", StartLocation = "São Paulo - Sul", EndLocation = "Bora Bora" },
            new Reservation { Id = 3, Name = "Eduardo", StartLocation = "São Paulo - Vila Mariana", EndLocation = "Sienna -Italia", },
            new Reservation { Id = 4, Name = "Kauane", StartLocation = "Guaruja", EndLocation = "Roma - Italia" },
            new Reservation { Id = 5, Name = "Leonardo", StartLocation = "São Paulo - Oeste", EndLocation = "Ibiza" },
            new Reservation { Id = 6, Name = "Renata", StartLocation = "São Paulo - Norte", EndLocation = "Paris - França" }
            }.ForEach(d => AddReservation(d));
        }

        // "montar" o dicionario em duas partes 
        // 1ª parte: definir os nomeados elementos key - pertencentes aos pares key-value tipicos de um dicionario
        public Reservation this[int id] => items.ContainsKey(id) ? items[id] : null;

        // 2ª parter: definir os nomeados elementos value - pertecentes aos pares key-value tipicos de um dicionario
        public IEnumerable<Reservation> Reservations => items.Values;

        // implementar o método AddReservation()
        public Reservation AddReservation(Reservation reservation)
        {
            //verificar o valor do paramentro id
            if(reservation.Id == 0)
            {
                int key = items.Count;
                
                // estabelecer um loop para iterar sobre o dicionario propriamente dito
                while (items.ContainsKey(key))
                {
                    // aqui, dentro do loop, é necessário percorrer um a um dos elementos do dicionario
                    key++;
                }
            }
            items[reservation.Id] = reservation;
            //definir a expressão de retorno
            return reservation;
        }

        // implementar - da interface - o método de atualização/alteraçao dos dados
        public Reservation UpdateReservation(Reservation reservation) => AddReservation(reservation);

        // impementar a tarefa de exclusao dos dados
        public void DeleteReservation(int id) => items.Remove(id);

    }
}

