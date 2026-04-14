using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Constants;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Dtos.DailiesStatistics;
using ProyectoPooBuses.Entities;
using ProyectoPooBuses.Mappers;

namespace ProyectoPooBuses.Services.Statistics
{
    public class DailyStatisticsServices : IDailyStatistics
    {
        private readonly BusUseRegisterDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;
        public DailyStatisticsServices(BusUseRegisterDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("Pagination:PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("Pagination:PageSizeLimit");
        }
        public async Task<ResponseDto<PageDto<List<DailiesStatisticsDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<TotalDailyStatisticsEntity> statisticQuery = _context.Dailies;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                statisticQuery = statisticQuery.Where(x => (x.Date + " " + x.MostUsedRoute).Contains(searchTerm)); 
            }

            int totalRows = await statisticQuery.CountAsync();

            var statistics = await statisticQuery
                .OrderBy(x => x.Date)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var statisticDto = DailiesStatisticsMapper.ListEntityToListDto(statistics);

            return new ResponseDto<PageDto<List<DailiesStatisticsDto>>>
            {
                StatusCode = HttpStatusCode.OK,  
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = new PageDto<List<DailiesStatisticsDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize),
                    Items = statisticDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<DailiesStatisticsDto>> GetOneByIdAsync(string id)
        {
            var statisticsEntity = await _context.Dailies.FirstOrDefaultAsync(b => b.Id == id);

            if (statisticsEntity is null)
            {
                return new ResponseDto<DailiesStatisticsDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }
             return new ResponseDto<DailiesStatisticsDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new DailiesStatisticsDto
                {
                    Id = statisticsEntity.Id,
                    Date = statisticsEntity.Date,
                    TotalPassengersDay = statisticsEntity.TotalPassengersDay,
                    RushHour = statisticsEntity.RushHour,
                    OffPeakHour = statisticsEntity.OffPeakHour,
                    MostUsedRoute = statisticsEntity.MostUsedRoute
                }
            };
        }
        public async Task<ResponseDto<DailiesStatisticsActionResponseDto>> CreateAsync(DailiesStatisticsCreateDto dto)
        {
            var dateInRoutes = await _context.Routes
            .FirstOrDefaultAsync(b => b.Date == dto.Date);

            if (dateInRoutes is null)
            {
                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Message = "No existe esa fecha registrada en el registro de uso de rutas",
                    Status = false,
                };
            }

            var dateInDailies = await _context.Dailies
            .FirstOrDefaultAsync(b => b.Date == dto.Date);

            if (dateInDailies is null)
            {
                dto.TotalPassengersDay = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .SumAsync(x => x.TotalPassengers);

                dto.RushHour = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderByDescending(x => x.TotalPassengers)
                .Select(x => x.Hour).FirstOrDefaultAsync();

                dto.OffPeakHour = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderBy(x => x.TotalPassengers)
                .Select(x => x.Hour).FirstOrDefaultAsync();

                var mostUsedRouteID = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderByDescending(x => x.TotalPassengers)
                .Select(x => x.BusId).FirstOrDefaultAsync();

                dto.MostUsedRoute = await _context.Buses.
                Where(x => x.Id == mostUsedRouteID).Select(x => x.RouteNumber).
                FirstOrDefaultAsync();
                TotalDailyStatisticsEntity statisticEntity = DailiesStatisticsMapper.CreateDtoToEntity(dto);

                await _context.SaveChangesAsync();

                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = HttpMessageResponse.REGISTER_CREATED,
                    Status = true,
                    Data = new DailiesStatisticsActionResponseDto
                    {
                        Id = statisticEntity.Id 
                    }
                };
            }
            else
            {
                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Message = "Fecha Existente en tabla de Estadísticas",
                    Status = false,
                };
            }
        }

        public async Task<ResponseDto<DailiesStatisticsActionResponseDto>> EditAsync(string id, DailiesStatisticsEditDto dto)
        {
            var statisticsEntity = await _context.Dailies.FirstOrDefaultAsync(b => b.Id == id);

            if (statisticsEntity is null)
            {
                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }
                dto.Date = await _context.Dailies
                .OrderBy(x => x.Id).
                Select(x => x.Date).FirstOrDefaultAsync(); 

                dto.TotalPassengersDay = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .SumAsync(x => x.TotalPassengers);

                dto.RushHour = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderByDescending(x => x.TotalPassengers)
                .Select(x => x.Hour).FirstOrDefaultAsync();

                dto.OffPeakHour = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderBy(x => x.TotalPassengers)
                .Select(x => x.Hour).FirstOrDefaultAsync();

                var mostUsedRouteID = await _context.Routes
                .Where(x => x.Date == dto.Date)
                .OrderByDescending(x => x.TotalPassengers)
                .Select(x => x.BusId).FirstOrDefaultAsync();

                dto.MostUsedRoute = await _context.Buses.
                Where(x => x.Id == mostUsedRouteID).Select(x => x.RouteNumber).
                FirstOrDefaultAsync();
                DailiesStatisticsMapper.EditDtoToEntity(statisticsEntity, dto);

                await _context.SaveChangesAsync();

                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = HttpMessageResponse.REGISTER_CREATED,
                    Status = true,
                    Data = new DailiesStatisticsActionResponseDto
                    {
                        Id = id
                    }
                };
        }
        public async Task<ResponseDto<DailiesStatisticsActionResponseDto>> DeleteAsync(string id)
        {
            var statisticEntity = await _context.Dailies.FirstOrDefaultAsync(b => b.Id == id);

            if (statisticEntity is null)
            {
                return new ResponseDto<DailiesStatisticsActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                };
            }
                
            _context.Dailies.Remove(statisticEntity);
            
            await _context.SaveChangesAsync();

            return new ResponseDto<DailiesStatisticsActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new DailiesStatisticsActionResponseDto
                {
                    Id = id
                }
            };
        }

    }
}