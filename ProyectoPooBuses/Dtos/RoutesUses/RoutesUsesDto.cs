using ProyectoPooBuses.Dtos.Buses;

namespace ProyectoPooBuses.Dtos.RoutesUses
{
    public class RoutesUsesDto
    {
        public string Id { get; set; }
        public BusOneDto BusId { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int TotalPassengers { get; set; }
    }
}