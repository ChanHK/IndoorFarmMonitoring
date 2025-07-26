


public class SensorService : ISensorService
{
    private readonly ISensorRepository _sensorRepository;

    public SensorService(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
    }

    public async Task<List<SensorData>> GetSensorDataAsync()
    {
        return await _sensorRepository.GetSensorDataAsync();
    }
}