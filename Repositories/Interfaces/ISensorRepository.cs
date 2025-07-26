
public interface ISensorRepository
{
    Task<List<SensorData>> GetSensorDataAsync();
}