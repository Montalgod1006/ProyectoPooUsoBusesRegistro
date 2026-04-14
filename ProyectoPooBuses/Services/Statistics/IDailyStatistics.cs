using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Dtos.DailiesStatistics;

namespace ProyectoPooBuses.Services.Statistics
{
    public interface IDailyStatistics
    {
        Task<ResponseDto<PageDto<List<DailiesStatisticsDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<DailiesStatisticsDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<DailiesStatisticsActionResponseDto>> CreateAsync(DailiesStatisticsCreateDto dto);
        Task<ResponseDto<DailiesStatisticsActionResponseDto>> EditAsync (string id, DailiesStatisticsEditDto dto);
        Task<ResponseDto<DailiesStatisticsActionResponseDto>> DeleteAsync (string id);
    }
}