namespace APIControllers.Models
{
    public interface IRepository
    {
        // descrever as instruções lógicas que serão Implementadas
        // Pela classe concreta Repository.cs e, posteriormente, também referenciadas na API

        // Recuperar todos os registros
        IEnumerable<Reservation> Reservations { get; }
        // buscar por um unico registro
        Reservation this[int id] { get; }
        // Adicionar um registro
        Reservation AddReservation(Reservation reservation);
        // Atualizar um registro
        Reservation UpdateReservation(Reservation reservation);
        // Remover/Excluir um registro
        void DeleteReservation(int id);
    }
}

// A interface acima descreve instruções que manipularão de dados através da API - aqui descrito um CRUD (Create = Criar/Inserir, Read = Ler 1 ou + registro, Update = Atualizar/alterar um registro, Delete = excluir um registro).