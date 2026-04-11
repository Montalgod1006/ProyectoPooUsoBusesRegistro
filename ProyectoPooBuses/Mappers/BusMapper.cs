using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Entities;

namespace ProyectoPooBuses.Mappers
{
    public static class BusMapper
    {
        public static BusRegisterEntity CreateDtoToEntity(BusCreateDto dto)
        {
            return new BusRegisterEntity
            {
                Id = Guid.NewGuid().ToString(),
                RouteNumber = dto.RouteNumber,
                BusModel = dto.BusModel,
                DNI = dto.DNI,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender

            };
        }

        public static BusRegisterEntity EditDtoToEntity(BusRegisterEntity entity, BusEditDto dto)
        {
            entity.RouteNumber = dto.RouteNumber;
            entity.BusModel = dto.BusModel;
            entity.DNI = dto.DNI;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Gender = dto.Gender;

            return entity;
        }

        public static List<BusDto> ListEntityToListDto(List<BusRegisterEntity> entities)
        {
            return entities.Select(bus => new BusDto
            {
                Id = bus.Id,
                RouteNumber = bus.RouteNumber, 
                BusModel = bus.BusModel,
                DNI = bus.DNI,
                FirstName = bus.FirstName,
                LastName = bus.LastName,
                Gender = bus.Gender

            }).ToList();
        }
    }
}