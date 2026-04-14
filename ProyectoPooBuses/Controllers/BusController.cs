using Microsoft.AspNetCore.Mvc;
using ProyectoPooBuses.Dtos.Buses;
using ProyectoPooBuses.Services.Buses;

namespace ProyectoPooBuses.Controllers
{
    [Route("api/bus")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusesServices _busService;

        public BusController(IBusesServices busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var result = await _busService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _busService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync (BusCreateDto dto)
        {
            var result = await _busService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAsync (string id, BusEditDto dto)
        {
            var result = await _busService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _busService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}