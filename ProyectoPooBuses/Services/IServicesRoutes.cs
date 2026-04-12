using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Dtos.Common;
using ProyectoPooBuses.Dtos.RoutesUses;
using ProyectoPooBuses.Entities;

namespace Buses.Services
{
    public interface IServicesRoutes
    {
        Task<ResponseDto<RoutesUsesActionResponseDto>> CreateAsync(RoutesUsesCreateDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> EditAsync(string id, RoutesUsesEditDto dto);
        Task<ResponseDto<RoutesUsesActionResponseDto>> DeleteAsync(string id);
    }
}