using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence;

namespace AlertService;
public class AlertService
{
    private readonly AppDbContext _context;

    public AlertService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAlertAsync(Guid machineId, string message)
    {
        var alert = new MaintenanceAlert
        {
            Id = Guid.NewGuid(),
            MachineId = machineId,
            Message = message,
            CreatedAt = DateTime.UtcNow
        };

        _context.MaintenanceAlerts.Add(alert);
        await _context.SaveChangesAsync();
    }
}