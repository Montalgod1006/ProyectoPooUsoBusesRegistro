using System.ComponentModel.DataAnnotations;

namespace ProyectoPooBuses.Dtos.RoutesUses
{
    public class RoutesUsesCreateDto
    {
        [Display(Name = "id del bus")]
        [Required(ErrorMessage = "Se requiere el {0}")]
        public string BusId { get; set; }
        [Required(ErrorMessage = "Se requiere la fecha")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="Se requiere la hora(formato 24 horas)")]
        public int Hour { get; set; }
        [Required(ErrorMessage ="Se requiere la cantidad de pasajeros")]
        public int TotalPassengers { get; set; }
    }
}