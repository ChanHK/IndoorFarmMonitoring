


public class PlantConfigService : IPlantConfigService
{
    private readonly IPlantConfigRepository _plantConfigRepository;

    public PlantConfigService(IPlantConfigRepository plantConfigRepository)
    {
        _plantConfigRepository = plantConfigRepository;
    }

    public async Task<List<PlantConfig>> GetPlantConfigsAsync()
    {
        return await _plantConfigRepository.GetPlantConfigsAsync();
    }
}