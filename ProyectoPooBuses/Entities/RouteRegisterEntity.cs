using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPooBuses.Entities
{
    public class RouteRegisterEntity : BaseEntity
    {
        [Column("bus_id")]
        [Required]
        [ForeignKey(nameof(BusId))]
        public string BusId { get; set; }
        public virtual BusRegisterEntity Bus { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public int TotalPassengers { get; set; }
    }
}