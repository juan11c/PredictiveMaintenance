using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Machine;
using PredictiveMaintenance.Infrastructure.Persistence;
using PredictiveMaintenance.Infrastructure.Services;
using Xunit;

namespace PredictiveMaintenance.Tests.Machine;
public class MachineServiceTests
{
    private readonly AppDbContext _context;
    private readonly MachineService _service;

    public MachineServiceTests()
    {
        // Base en memoria (no toca tu base real)
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MachineTestDb")
            .Options;

        _context = new AppDbContext(options);
        _service = new MachineService(_context);
    }

    [Fact]
    public async Task AddMachineAsync_ShouldCreateMachine()
    {
        var request = new MachineRequestDto
        {
            SerialNumber = "SN-123",
            Model = "Model-X",
            InstallationDate = DateTime.UtcNow
        };

        var result = await _service.AddMachineAsync(request);

        Assert.NotNull(result);
        Assert.Equal("SN-123", result.SerialNumber);
        Assert.Equal("Model-X", result.Model);

        var machineInDb = await _context.Machines.FindAsync(result.Id);
        Assert.NotNull(machineInDb);
    }

    [Fact]
    public async Task GetAllMachinesAsync_ShouldReturnMachines()
    {
        _context.Machines.Add(new PredictiveMaintenance.Domain.Entities.Machine
        {
            Id = Guid.NewGuid(),
            SerialNumber = "SN-001",
            Model = "Model-A",
            InstallationDate = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();

        var result = await _service.GetAllMachinesAsync();

        Assert.NotEmpty(result);
        Assert.Contains(result, m => m.SerialNumber == "SN-001");
    }

    [Fact]
    public async Task GetMachineByIdAsync_ShouldReturnMachine_WhenExists()
    {
        var machine = new PredictiveMaintenance.Domain.Entities.Machine
        {
            Id = Guid.NewGuid(),
            SerialNumber = "SN-002",
            Model = "Model-B",
            InstallationDate = DateTime.UtcNow
        };
        _context.Machines.Add(machine);
        await _context.SaveChangesAsync();

        var result = await _service.GetMachineByIdAsync(machine.Id);

        Assert.NotNull(result);
        Assert.Equal("SN-002", result.SerialNumber);
    }

    [Fact]
    public async Task GetMachineByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _service.GetMachineByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateMachineByIdAsync_ShouldUpdateMachine_WhenExists()
    {
        var machine = new PredictiveMaintenance.Domain.Entities.Machine
        {
            Id = Guid.NewGuid(),
            SerialNumber = "SN-003",
            Model = "Model-C",
            InstallationDate = DateTime.UtcNow
        };
        _context.Machines.Add(machine);
        await _context.SaveChangesAsync();

        var updateDto = new MachineUpdateDto
        {
            SerialNumber = "SN-003-Updated",
            Model = "Model-C-Updated",
            InstallationDate = DateTime.UtcNow.AddDays(1)
        };

        var result = await _service.UpdateMachineByIdAsync(machine.Id, updateDto);

        Assert.NotNull(result);
        Assert.Equal("SN-003-Updated", result.SerialNumber);
        Assert.Equal("Model-C-Updated", result.Model);
    }

    [Fact]
    public async Task UpdateMachineByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var updateDto = new MachineUpdateDto
        {
            SerialNumber = "SN-999",
            Model = "Model-Z",
            InstallationDate = DateTime.UtcNow
        };

        var result = await _service.UpdateMachineByIdAsync(Guid.NewGuid(), updateDto);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteMachineByIdAsync_ShouldDeleteMachine_WhenExists()
    {
        var machine = new PredictiveMaintenance.Domain.Entities.Machine
        {
            Id = Guid.NewGuid(),
            SerialNumber = "SN-004",
            Model = "Model-D",
            InstallationDate = DateTime.UtcNow
        };
        _context.Machines.Add(machine);
        await _context.SaveChangesAsync();

        var result = await _service.DeleteMachineByIdAsync(machine.Id);

        Assert.NotNull(result);
        Assert.Equal("SN-004", result.SerialNumber);

        var machineInDb = await _context.Machines.FindAsync(machine.Id);
        Assert.Null(machineInDb); // ya fue eliminado
    }

    [Fact]
    public async Task DeleteMachineByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _service.DeleteMachineByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }
}