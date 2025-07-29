using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndoorFarmMonitoring.Controllers
{
    [ApiController]
    [Route("plant-sensor-data")]
    public class PlantSensorDataController : ControllerBase
    {
        private readonly ICombinedTrayService _combinedTrayService;
        private readonly ILogger<PlantSensorDataController> _logger;

        public PlantSensorDataController(ICombinedTrayService combinedTrayService, ILogger<PlantSensorDataController> logger)
        {
            _combinedTrayService = combinedTrayService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCombinedTrayData()
        {
            try
            {
                _logger.LogInformation("Fetching combined tray data...");

                var combinedTrayData = await _combinedTrayService.GetCombinedTrayAsync();

                if (combinedTrayData == null || !combinedTrayData.Any())
                {
                    _logger.LogWarning("No combined tray data found.");
                    return NotFound("No data available.");
                }

                _logger.LogInformation("Successfully fetched combined tray data.");
                return Ok(combinedTrayData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching combined tray data.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
