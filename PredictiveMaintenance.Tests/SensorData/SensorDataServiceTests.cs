using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.SensorData;
using PredictiveMaintenance.Infrastructure.Persistence;
using PredictiveMaintenance.Infrastructure.services;
using PredictiveMaintenance.Domain.Entities;
using Xunit;

namespace PredictiveMaintenance.Tests.SensorData;
public class SensorDataServiceTests
{
    private readonly AppDbContext _context;
    private readonly SensorDataService _service;

    public SensorDataServiceTests()
    {
        // Base en memoria (no toca tu base real)
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SensorDataTestDb")
            .Options;

        _context = new AppDbContext(options);
        _service = new SensorDataService(_context);
    }

    [Fact]
    public async Task AddSensorDataAsync_ShouldCreateSensorData()
    {
        // Arrange: primero creamos un sensor para que exista la FK
        var sensor = new Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = Guid.NewGuid(),
            Type = "Temperature",
            Unit = "°C"
        };
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();

        var request = new SensorDataRequestDto
        {
            SensorId = sensor.Id,
            Value = 25.5,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var result = await _service.AddSensorDataAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sensor.Id, result.SensorId);
        Assert.Equal(25.5, result.Value);

        var sensorDataInDb = await _context.SensorData.FindAsync(result.Id);
        Assert.NotNull(sensorDataInDb);
    }

    [Fact]
    public async Task GetSensorDataByIdAsync_ShouldReturnSensorData_WhenExists()
    {
        var sensor = new Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = Guid.NewGuid(),
            Type = "Vibration",
            Unit = "Hz"
        };
        _context.Sensors.Add(sensor);

        var sensorData = new PredictiveMaintenance.Domain.Entities.SensorData
        {
            Id = Guid.NewGuid(),
            SensorId = sensor.Id,
            Value = 100.0,
            Timestamp = DateTime.UtcNow
        };
        _context.SensorData.Add(sensorData);
        await _context.SaveChangesAsync();

        var result = await _service.GetSensorDataByIdAsync(sensorData.Id);

        Assert.NotNull(result);
        Assert.Equal(100.0, result.Value);
        Assert.Equal(sensor.Id, result.SensorId);
    }

    [Fact]
    public async Task GetSensorDataByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _service.GetSensorDataByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task GetSensorsByMachineIdAsync_ShouldReturnSensorDataForMachine()
    {
        var machineId = Guid.NewGuid();

        var sensor = new Sensor
        {
            Id = Guid.NewGuid(),
            MachineId = machineId,
            Type = "Energy",
            Unit = "kWh"
        };
        _context.Sensors.Add(sensor);

        var sensorData = new PredictiveMaintenance.Domain.Entities.SensorData
        {
            Id = Guid.NewGuid(),
            SensorId = sensor.Id,
            Value = 500.0,
            Timestamp = DateTime.UtcNow
        };
        _context.SensorData.Add(sensorData);
        await _context.SaveChangesAsync();

        var result = await _service.GetSensorsByMachineIdAsync(machineId);

        Assert.NotEmpty(result);
        Assert.Contains(result, d => d.SensorId == sensor.Id && d.Value == 500.0);
    }

    [Fact]
    public async Task GetSensorsByMachineIdAsync_ShouldReturnEmpty_WhenNoData()
    {
        var result = await _service.GetSensorsByMachineIdAsync(Guid.NewGuid());
        Assert.Empty(result);
    }
}