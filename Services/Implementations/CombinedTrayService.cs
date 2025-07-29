


public class CombinedTrayService : ICombinedTrayService
{
    private readonly ISensorService _sensorService;
    private readonly IPlantConfigService _plantConfigService;

    public CombinedTrayService(ISensorService sensorService, IPlantConfigService plantConfigService)
    {
        _sensorService = sensorService;
        _plantConfigService = plantConfigService;
    }

    public async Task<List<CombinedTray>> GetCombinedTrayAsync()
    {
        var sensorData = await _sensorService.GetSensorDataAsync();
        var plantConfigs = await _plantConfigService.GetPlantConfigsAsync();
        Console.WriteLine($"Sensor Data Count: {sensorData.Count}, Plant Configs Count: {plantConfigs.Count}");

        var combinedData = from sensor in sensorData
                          join config in plantConfigs on sensor.tray_id equals config.tray_id
                          select new CombinedTray
                          {
                            tray_id = sensor.tray_id,
                            temperature = sensor.temperature,
                            humidity = sensor.humidity,
                            light = sensor.light,
                            plant_type = config.plant_type,
                            target_temperature = config.target_temperature,
                            target_humidity = config.target_humidity,
                            target_light = config.target_light,
                            is_out_of_range = sensor.temperature < config.target_temperature
                                           || sensor.humidity < config.target_humidity
                                           || sensor.light < config.target_light
                          };
        
        return combinedData.ToList();
    }
}