using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Dtos.RoutesUses;
using ProyectoPooBuses.Mappers;
using ProyectoPooBuses.Constants;

namespace Routes.Services
{
    public class ServicesRoutes 
    {
        private readonly BusUseRegisterDbContext _context;

        public ServicesRoutes(BusUseRegisterDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<RoutesUsesActionResponseDto>> CreateAsync(RoutesUsesCreateDto dto)
        {
            RouteRegisterEntity routeEntity = RouteMapper.CreateDtoToEntity(dto);

            _context.Routes.Add(routeEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<RoutesUsesActionResponseDto>
            {
                StatusCode = ProyectoPooBuses.Constants.HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new RoutesUsesActionResponseDto
                {
                    Id = routeEntity.Id
                }
            };
        }

        public async Task<ResponseDto<RoutesUsesActionResponseDto>> EditAsync(string id, RoutesUsesEditDto dto)
        {
            var routeEntity = await _context.Routes.FirstOrDefaultAsync(r => r.Id == id);

            if (routeEntity is null)
            {
                return new ResponseDto<RoutesUsesActionResponseDto>
                {
                    StatusCode = ProyectoPooBuses.Constants.HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }

            RouteMapper.EditDtoToEntity(routeEntity, dto);

            await _context.SaveChangesAsync();

            return new ResponseDto<RoutesUsesActionResponseDto>
            {
                StatusCode = ProyectoPooBuses.Constants.HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new RoutesUsesActionResponseDto
                {
                    Id = id
                }
            };
        }

        public async Task<ResponseDto<RoutesUsesActionResponseDto>> DeleteAsync(string id)
        {
            var routeEntity = await _context.Routes.FirstOrDefaultAsync(r => r.Id == id);

            if (routeEntity is null)
            {
                return new ResponseDto<RoutesUsesActionResponseDto>
                {
                    StatusCode = ProyectoPooBuses.Constants.HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }

            _context.Routes.Remove(routeEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<RoutesUsesActionResponseDto>
            {
                StatusCode = ProyectoPooBuses.Constants.HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new RoutesUsesActionResponseDto
                {
                    Id = id
                }
            };
        }
    }
}