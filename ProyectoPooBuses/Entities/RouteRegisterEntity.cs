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
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("hour")]
        public int Hour { get; set; }
        [Column("total_passenger")]
        public int TotalPassengers { get; set; }
    }
}