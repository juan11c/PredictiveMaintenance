using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Sensor;
using PredictiveMaintenance.Infrastructure.Persistence;
using PredictiveMaintenance.Infrastructure.Services;
using Xunit;

namespace PredictiveMaintenance.Tests;
public class SensorServiceTests
{
    private readonly AppDbContext _context;
    private readonly SensorService _service;

    public SensorServiceTests()
    {
        // Configuramos EF Core con base en memoria
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _service = new SensorService(_context);
    }

    [Fact]
    public async Task AddSensorAsync_ShouldCreateSensor()
    {
        // Arrange: se prepara el DTO de entrada
        var request = new SensorRequestDto
        {
            MachineId = Guid.NewGuid(),
            Type = "Temperatura",
            Unit = "°C"
        };

        // Act: llamamos al servicio
        var result = await _service.AddSensorAsync(request);

        // Assert: verificación que el resultado sea correcto
        Assert.NotNull(result);
        Assert.Equal("Temperatura", result.Type);
        Assert.Equal("°C", result.Unit);

        // Además, verificación que realmente se guardó en la base en memoria
        var sensorInDb = await _context.Sensors.FindAsync(result.Id);
        Assert.NotNull(sensorInDb);
        Assert.Equal(request.Type, sensorInDb.Type);
    }

    [Fact]
    public async Task GetSensorByIdAsync_ShouldReturnSensor_WhenExists()
    {
        var sensor = new PredictiveMaintenance.Domain.Entities.Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = Guid.NewGuid(),
            Type = "Vibration",
            Unit = "Hz"
        };
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();

        var result = await _service.GetSensorByIdAsync(sensor.Id);

        Assert.NotNull(result);
        Assert.Equal("Vibration", result.Type);
        Assert.Equal("Hz", result.Unit);
    }

    [Fact]
    public async Task GetSensorByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _service.GetSensorByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateSensorByIdAsync_ShouldUpdateSensor_WhenExists()
    {
        var sensor = new PredictiveMaintenance.Domain.Entities.Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = Guid.NewGuid(),
            Type = "Energy",
            Unit = "kWh"
        };
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();

        var updateDto = new SensorUpdateDto
        {
            Type = "Temperature",
            Unit = "°C"
        };

        var result = await _service.UpdateSensorByIdAsync(sensor.Id, updateDto);

        Assert.NotNull(result);
        Assert.Equal("Temperature", result.Type);
        Assert.Equal("°C", result.Unit);
    }

    [Fact]
    public async Task UpdateSensorByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var updateDto = new SensorUpdateDto
        {
            Type = "Temperature",
            Unit = "°C"
        };

        var result = await _service.UpdateSensorByIdAsync(Guid.NewGuid(), updateDto);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteSensorByIdAsync_ShouldDeleteSensor_WhenExists()
    {
        var sensor = new PredictiveMaintenance.Domain.Entities.Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = Guid.NewGuid(),
            Type = "Energy",
            Unit = "kWh"
        };
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();

        var result = await _service.DeleteSensorByIdAsync(sensor.Id);

        Assert.NotNull(result);
        Assert.Equal("Energy", result.Type);

        var sensorInDb = await _context.Sensors.FindAsync(sensor.Id);
        Assert.Null(sensorInDb); // ya fue eliminado
    }

    [Fact]
    public async Task DeleteSensorByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _service.DeleteSensorByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }
}