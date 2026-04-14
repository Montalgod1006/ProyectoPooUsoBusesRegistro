using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPooBuses.Entities
{
    public class TotalDailyStatisticsEntity : BaseEntity
    {
        [Column("date")]
        [Required]
        public DateTime Date { get; set; }
        [Column("total_passenger_day")]
        public int TotalPassengersDay { get; set; }
        [Column("rush_hour")]
        public int RushHour { get; set; }
        [Column("off_peak_hour")]
        public int OffPeakHour { get; set; }
        [Column("most_used_route")]
        public string MostUsedRoute { get; set; }
    }
}