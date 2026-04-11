using ProyectoPooBuses.Dtos.DailiesStatistics;
using ProyectoPooBuses.Entities;

namespace ProyectoPooBuses.Mappers
{
    public static class DailiesStatisticsMapper
    {
        public static TotalDailyStatisticsEntity CreateDtoToEntity(DailiesStatisticsCreateDto dto)
        {
            return new TotalDailyStatisticsEntity
            { 
                Id = Guid.NewGuid().ToString(),
                Date = dto.Date,
                TotalPassengersDay = dto.TotalPassengersDay,
                RushHour = dto.RushHour,
                OffPeakHour = dto.OffPeakHour,
                MostUsedRoute = dto.MostUsedRoute
            };
        }

        public static TotalDailyStatisticsEntity EditDtoToEntity(TotalDailyStatisticsEntity entity, DailiesStatisticsCreateDto dto)
        {
            entity.Date = dto.Date;
            entity.TotalPassengersDay = dto.TotalPassengersDay;
            entity.RushHour = dto.RushHour;
            entity.OffPeakHour = dto.OffPeakHour;
            entity.MostUsedRoute = dto.MostUsedRoute;

            return entity;
        }
    }
}