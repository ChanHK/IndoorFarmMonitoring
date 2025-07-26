using Microsoft.AspNetCore.Mvc;

namespace IndoorFarmMonitoring.Controllers
{
    [ApiController]
    [Route("plant-sensor-data")]
    public class PlantSensorDataController : ControllerBase
    {
        private readonly ICombinedTrayService _combinedTrayService;

        public PlantSensorDataController(ICombinedTrayService combinedTrayService)
        {
            _combinedTrayService = combinedTrayService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CombinedTray>>> GetCombinedTrayData()
        {
            try
            {
                var combinedTrayData = await _combinedTrayService.GetCombinedTrayAsync();
                return Ok(combinedTrayData);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}