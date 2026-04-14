using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.Common;

namespace ProyectoPooBuses.Services.Buses
{
    public interface IBusesServices
    {
        Task<ResponseDto<PageDto<List<BusDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<BusDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<BusesActionResponseDto>> CreateAsync(BusCreateDto dto);
        Task<ResponseDto<BusesActionResponseDto>> EditAsync (string id, BusEditDto dto);
        Task<ResponseDto<BusesActionResponseDto>> DeleteAsync (string id);
    }
}