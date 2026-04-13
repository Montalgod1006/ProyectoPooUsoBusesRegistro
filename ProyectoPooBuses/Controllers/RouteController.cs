using Microsoft.AspNetCore.Mvc;
using ProyectoPooBuses.Dtos.RoutesUses;
using ProyectoPooBuses.Services.Routes;

namespace ProyectoPooBuses.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRoutesServices _routeService;

        public RouteController(IRoutesServices routesServices)
        {
            _routeService = routesServices;
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoutesUsesCreateDto dto)
        {
            var result = await _routeService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, RoutesUsesEditDto dto)
        {
            var result = await _routeService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _routeService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}