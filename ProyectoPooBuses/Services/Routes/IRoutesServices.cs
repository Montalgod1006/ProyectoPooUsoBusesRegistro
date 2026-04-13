using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Dtos.RoutesUses;

namespace ProyectoPooBuses.Services.Routes
{
    public interface IRoutesServices
    {
        Task<ResponseDto<RoutesUsesActionResponseDto>> CreateAsync(RoutesUsesCreateDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> EditAsync(string id, RoutesUsesEditDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> DeleteAsync(string id);
    }
}