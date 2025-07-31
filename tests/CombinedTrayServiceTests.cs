using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions; 

public class CombinedTrayServiceTests
{
    [Fact]
    public async Task GetCombinedTrayAsync_ShouldReturnCombinedData_WithCorrectRangeStatus()
    {
        // Arrange
        var mockSensorService = new Mock<ISensorService>();
        var mockPlantConfigService = new Mock<IPlantConfigService>();

        mockSensorService.Setup(s => s.GetSensorDataAsync()).ReturnsAsync(new List<SensorData>
        {
            new SensorData { tray_id = 1, temperature = 25.5, humidity = 60, light = 1000 }
        });

        mockPlantConfigService.Setup(p => p.GetPlantConfigsAsync()).ReturnsAsync(new List<PlantConfig>
        {
            new PlantConfig { tray_id = 1, plant_type = "Lettuce", target_temperature = 24, target_humidity = 65, target_light = 1200 }
        });

        var service = new CombinedTrayService(mockSensorService.Object, mockPlantConfigService.Object);

        // Act
        var result = await service.GetCombinedTrayAsync();

        // Assert
        result.Should().HaveCount(1);
        result[0].tray_id.Should().Be(1);
        result[0].plant_type.Should().Be("Lettuce");
        result[0].is_out_of_range.Should().BeTrue();
    }

    [Fact]
    public async Task GetCombinedTrayAsync_ShouldReturnEmpty_WhenTrayIdsDoNotMatch()
    {
        // Arrange
        var mockSensorService = new Mock<ISensorService>();
        var mockPlantConfigService = new Mock<IPlantConfigService>();

        mockSensorService.Setup(s => s.GetSensorDataAsync()).ReturnsAsync(new List<SensorData>
        {
            new SensorData { tray_id = 2, temperature = 25.5, humidity = 60, light = 1000 }
        });

        mockPlantConfigService.Setup(p => p.GetPlantConfigsAsync()).ReturnsAsync(new List<PlantConfig>
        {
            new PlantConfig { tray_id = 1, plant_type = "Lettuce", target_temperature = 24, target_humidity = 65, target_light = 1200 }
        });

        var service = new CombinedTrayService(mockSensorService.Object, mockPlantConfigService.Object);

        // Act
        var result = await service.GetCombinedTrayAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetCombinedTrayAsync_ShouldHandleEmptySensorData()
    {
        // Arrange
        var mockSensorService = new Mock<ISensorService>();
        var mockPlantConfigService = new Mock<IPlantConfigService>();

        mockSensorService.Setup(s => s.GetSensorDataAsync()).ReturnsAsync(new List<SensorData>());
        mockPlantConfigService.Setup(p => p.GetPlantConfigsAsync()).ReturnsAsync(new List<PlantConfig>());

        var service = new CombinedTrayService(mockSensorService.Object, mockPlantConfigService.Object);

        // Act
        var result = await service.GetCombinedTrayAsync();

        // Assert
        result.Should().BeEmpty();
    }
}
