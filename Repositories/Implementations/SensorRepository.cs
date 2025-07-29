using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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

            using var request = new HttpRequestMessage(HttpMethod.Get, "http://3.0.148.231:8010/sensor-readings");
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Sensor API responded with non-success status code: {StatusCode}", response.StatusCode);
                return new List<SensorData>();
            }

            var stream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<List<SensorData>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null || !data.Any())
            {
                _logger.LogWarning("Sensor data is empty or malformed.");
                return new List<SensorData>();
            }

            _logger.LogInformation("Successfully fetched sensor data.");
            return data;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Sensor API request timed out.");
            return new List<SensorData>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching sensor data.");
            return new List<SensorData>();
        }
    }
}