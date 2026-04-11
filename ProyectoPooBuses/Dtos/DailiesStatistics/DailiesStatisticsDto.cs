namespace ProyectoPooBuses.Dtos.DailiesStatistics
{
    public class DailiesStatisticsDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int TotalPassengersDay { get; set; }
        public int RushHour { get; set; }
        public int OffPeakHour { get; set; }
        public string MostUsedRoute { get; set; }
    }
}