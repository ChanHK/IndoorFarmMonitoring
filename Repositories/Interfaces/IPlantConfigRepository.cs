
public interface IPlantConfigRepository
{
    Task<List<PlantConfig>> GetPlantConfigsAsync();
}