using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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

            using var request = new HttpRequestMessage(HttpMethod.Get, "http://3.0.148.231:8020/plant-configurations");
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Plant Config API responded with status code: {StatusCode}", response.StatusCode);
                return new List<PlantConfig>();
            }

            var stream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<List<PlantConfig>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null || !data.Any())
            {
                _logger.LogWarning("Plant configuration data is empty or malformed.");
                return new List<PlantConfig>();
            }

            _logger.LogInformation("Successfully fetched plant configurations.");
            return data;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Plant Config API request timed out.");
            return new List<PlantConfig>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching plant configurations.");
            return new List<PlantConfig>();
        }
    }
}