
using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Entities;
using SQLitePCL;

namespace Buses.Services
{
    
    public class ServicesBuses
    {
        private readonly BusUseRegisterDbContext _context;

        public ServicesBuses(BusUseRegisterDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<BusesActionResponseDto>> CreateAsync(BusCreateDto dto)

            {
                BusRegisterEntity busEntity = BusMapper.CreateDtoToEntity(dto);

                _context.Buses.Add(busEntity);
                await _context.SaveChangesAsync();
                    return new ResponseDto<BusesActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = HttpMessageResponse.REGISTER_CREATED,
                        Status = true,
                        Data = new BusesActionResponseDto
                        {
                            Id = busEntity.Id 
                        }
                    };
            }
        public async Task<ResponseDto<BusesActionResponseDto>> EditAsync (string id, BusCreateDto dto)
        {
            var busEntity = await _context.Buses.FirstOrDefaultAsync(b => b.Id == id);

                if (busEntity is null)
                {
                    return new ResponseDto<BusesActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    };
                }

                BusMapper.EditDtoToEntity(busEntity, dto);

                await _context.SaveChangesAsync();

                return new ResponseDto<BusesActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = HttpMessageResponse.REGISTER_UPDATED,
                    Data = new BusesActionResponseDto
                    {
                        Id = id
                    }
                };
        }

        public async Task<ResponseDto<BusesActionResponseDto>> DeleteAsync (string id)
        {
             var busEntity = await _context.Buses.FirstOrDefaultAsync(b => b.Id == id);

                if (busEntity is null)
                {
                    return new ResponseDto<BusesActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.NOT_FOUND,
                        Status = false,
                        Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    };
                }
                
                _context.Buses.Remove(busEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<BusesActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = HttpMessageResponse.REGISTER_DELETED,
                    Data = new BusesActionResponseDto
                    {
                        Id = id
                    }
                };
        }

    }
}