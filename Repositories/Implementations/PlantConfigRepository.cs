using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;

public class PlantConfigRepository : IPlantConfigRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PlantConfigRepository> _logger;

    public PlantConfigRepository(HttpClient httpClient, ILogger<PlantConfigRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<PlantConfig>> GetPlantConfigsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching plant configurations from API.");
            var response = await _httpClient.GetFromJsonAsync<List<PlantConfig>>("http://3.0.148.231:8020/plant-configurations");
            _logger.LogInformation("Successfully fetched plant configurations.");
            return response ?? new List<PlantConfig>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching plant configurations.");
            return new List<PlantConfig>();
        }
    }
}