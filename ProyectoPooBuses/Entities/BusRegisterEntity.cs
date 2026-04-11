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
        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }
        [Column("last_name")]
        [Required]
        public string LastName { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        public virtual IEnumerable<RouteRegisterEntity> Routes { get; set; }
    }
}