using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Constants;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Entities;
using ProyectoPooBuses.Mappers;

namespace ProyectoPooBuses.Services.Buses
{ 
    public class BusesServices : IBusesServices
    {
        private readonly BusUseRegisterDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BusesServices(BusUseRegisterDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("Pagination:PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("Pagination:PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<BusDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<BusRegisterEntity> busesQuery = _context.Buses;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                busesQuery = busesQuery.Where(x => (x.RouteNumber + " " + x.BusModel).Contains(searchTerm)); 
            }

            int totalRows = await busesQuery.CountAsync();

            var buses = await busesQuery
                .OrderBy(x => x.RouteNumber)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var busesDto = BusMapper.ListEntityToListDto(buses);

            return new ResponseDto<PageDto<List<BusDto>>>
            {
                StatusCode = HttpStatusCode.OK,  
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = new PageDto<List<BusDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize),
                    Items = busesDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<BusDto>> GetOneByIdAsync(string id)
        {
            var busEntity = await _context.Buses
            .FirstOrDefaultAsync(b => b.Id == id);

            if (busEntity is null)
            {
                return new ResponseDto<BusDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                };
            }

            return new ResponseDto<BusDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new BusDto
                {
                    Id = busEntity.Id,
                    RouteNumber = busEntity.RouteNumber,
                    BusModel = busEntity.BusModel,
                    DNI = busEntity.DNI,
                    FirstName = busEntity.FirstName,
                    LastName = busEntity.LastName,
                    Gender = busEntity.Gender
                }
            };
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

        public async Task<ResponseDto<BusesActionResponseDto>> EditAsync(string id, BusEditDto dto)
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