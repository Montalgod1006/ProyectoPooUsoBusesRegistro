using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.Common;

namespace Buses.Services
{
    public interface IServicesBuses
    {
        Task<ResponseDto<BusesActionResponseDto>> CreateAsync(BusCreateDto dto);
        Task<ResponseDto<BusesActionResponseDto>> EditAsync (string id, BusCreateDto dto);
        Task<ResponseDto<BusesActionResponseDto>> DeleteAsync (string id);
    }
}