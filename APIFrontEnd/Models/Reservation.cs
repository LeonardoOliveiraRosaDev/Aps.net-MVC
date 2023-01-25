namespace APIFrontEnd.Models
{
    public class Reservation
    {
        // Definir as propriedades do model
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? StartLocation { get; set; }
        public string? EndLocation { get; set;}
    }
}
