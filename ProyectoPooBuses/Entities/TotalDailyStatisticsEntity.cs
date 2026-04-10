using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPooBuses.Entities
{
    public class TotalDailyStatisticsEntity : BaseEntity
    {
        [Column("date")]
        [Required]
        public DateTime Date { get; set; }
        public int TotalPassengersDay { get; set; }
        public int RushHour { get; set; }
        public int OffPeakHour { get; set; }
        public string MostUsedRoute { get; set; }
    }
}