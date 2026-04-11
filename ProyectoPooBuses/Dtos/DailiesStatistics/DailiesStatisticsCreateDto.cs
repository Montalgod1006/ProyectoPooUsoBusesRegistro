using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPooBuses.Dtos.DailiesStatistics
{
    public class DailiesStatisticsCreateDto
    {
        [Required(ErrorMessage = "Se requiere la fecha")]
        public DateTime Date { get; set; }
        public int TotalPassengersDay { get; set; }
        public int RushHour { get; set; }
        public int OffPeakHour { get; set; }
        public string MostUsedRoute { get; set; }
    }
}