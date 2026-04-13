using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Dtos.RoutesUses;

namespace ProyectoPooBuses.Services.Routes
{
    public interface IRoutesServices
    {
        Task<ResponseDto<PageDto<List<RoutesUsesDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<RoutesUsesDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<RoutesUsesActionResponseDto>> CreateAsync(RoutesUsesCreateDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> EditAsync(string id, RoutesUsesEditDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> DeleteAsync(string id);
    }
}