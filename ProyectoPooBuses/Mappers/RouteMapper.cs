using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.RoutesUses;
using ProyectoPooBuses.Entities;

namespace ProyectoPooBuses.Mappers
{
    public static class RouteMapper
    {
        public static RouteRegisterEntity CreateDtoToEntity(RoutesUsesCreateDto dto)
        {
            return new RouteRegisterEntity
            {
                Id = Guid.NewGuid().ToString(),
                BusId = dto.BusId,
                Date = dto.Date,
                Hour = dto.Hour,
                TotalPassengers = dto.TotalPassengers
            };
        }

        public static RouteRegisterEntity EditDtoToEntity(RouteRegisterEntity entity, RoutesUsesCreateDto dto)
        {
            entity.BusId = dto.BusId;
            entity.Date = dto.Date;
            entity.Hour = dto.Hour;
            entity.TotalPassengers = dto.TotalPassengers;

            return entity;
        }

        public static List<RoutesUsesDto> ListEntityToListDto(List<RouteRegisterEntity> entities)
        {
            return entities.Select(route => new RoutesUsesDto
            {
                Id = route.Id,
                BusId = new BusOneDto
                {
                    Id = route.Bus.Id,
                    RouteNumber = route.Bus.RouteNumber,
                },
                Date = route.Date,
                Hour = route.Hour,
                TotalPassengers = route.TotalPassengers
            }).ToList();
        }
    }
}