using Microsoft.AspNetCore.Mvc;
using ProyectoPooBuses.Dtos.DailiesStatistics;
using ProyectoPooBuses.Services.Statistics;


namespace ProyectoPooBuses.Controllers
{
    [ApiController]
    [Route("api/statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly IDailyStatistics _statisticService;
        public StatisticController(IDailyStatistics statisticsServices)
        {
            _statisticService = statisticsServices;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm ="", int page =1, int pageSize = 10)
        {
            var response = await _statisticService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _statisticService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DailiesStatisticsCreateDto dto)
        {
            var result = await _statisticService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, DailiesStatisticsEditDto dto)
        {
            var result = await _statisticService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _statisticService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}