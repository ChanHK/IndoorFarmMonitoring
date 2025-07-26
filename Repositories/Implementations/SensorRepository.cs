using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

public class SensorRepository : ISensorRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SensorRepository> _logger;

    public SensorRepository(HttpClient httpClient, ILogger<SensorRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<SensorData>> GetSensorDataAsync()
    {
        try
        {
            _logger.LogInformation("Fetching sensor data from API.");
            var response = await _httpClient.GetFromJsonAsync<List<SensorData>>("http://3.0.148.231:8010/sensor-readings");
            _logger.LogInformation("Successfully fetched sensor data.");
            return response ?? new List<SensorData>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sensor data.");
            return new List<SensorData>();
        }
    }
}