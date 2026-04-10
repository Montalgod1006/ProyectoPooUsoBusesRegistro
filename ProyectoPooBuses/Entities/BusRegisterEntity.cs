using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPooBuses.Entities
{
    public class BusRegisterEntity : BaseEntity
    {
        [Column("route_number")]
        [Required]
        public string RouteNumber { get; set; }
        [Column("bus_model")]
        public string BusModel { get; set; }
        [Column("dni")]
        [Required]
        public string DNI { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("last_name")]
        [Required]
        public string LastName { get; set; }
        public virtual IEnumerable<RouteRegisterEntity> Routes { get; set; }
    }
}