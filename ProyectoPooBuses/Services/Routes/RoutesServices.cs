using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Entities;
using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Dtos.RoutesUses;
using ProyectoPooBuses.Mappers;
using ProyectoPooBuses.Constants;
using ProyectoPooBuses.Dtos.Buses;

namespace ProyectoPooBuses.Services.Routes
{
    public class RoutesServices : IRoutesServices
    {
        private readonly BusUseRegisterDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public RoutesServices(BusUseRegisterDbContext context, IConfiguration configuration)
        {
            _context = context;
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<RoutesUsesDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1)* pageSize;

            IQueryable<RouteRegisterEntity> routesQuery = _context.Routes.Include(p => p.Bus);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routesQuery = routesQuery.Where( x => (x.BusId + " " + x.Date + "" + x.Hour).Contains(searchTerm) );
            }

            int totalRows = await routesQuery.CountAsync(); 

            var RouteRegisterEntity = await routesQuery  
                .OrderBy(x => x.Date)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return new ResponseDto<PageDto<List<RoutesUsesDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<RoutesUsesDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize),
                    Items = RouteMapper.ListEntityToListDto(RouteRegisterEntity),
                    HasNextPage = startIndex +pageSize < PAGE_SIZE_LIMIT && 
                        page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }
        public async Task<ResponseDto<RoutesUsesDto>> GetOneByIdAsync(string id)
        {
            var routeRegisterEntity = await _context.Routes
            .Include(p => p.Bus)
            .FirstOrDefaultAsync( 
               p => p.Id == id
            );
            
             if(routeRegisterEntity is null)
            {
                return new ResponseDto<RoutesUsesDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                };
            }
            return new ResponseDto<RoutesUsesDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new RoutesUsesDto
                {
                    Id = routeRegisterEntity.Id,
                BusId = new BusOneDto
                {
                    Id = routeRegisterEntity.Bus.Id,
                    RouteNumber = routeRegisterEntity.Bus.RouteNumber,
                },
                Date = routeRegisterEntity.Date,
                Hour = routeRegisterEntity.Hour,
                TotalPassengers = routeRegisterEntity.TotalPassengers
                    
                },
            };
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