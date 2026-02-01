using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Alerts;
using PredictiveMaintenance.Application.DTOs.Telemetry;
using PredictiveMaintenance.Application.Interfaces;
using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence;


namespace PredictiveMaintenance.Application.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly AppDbContext _context;

        public TelemetryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSensorDataAsync(SensorDataDto data)
        {
            var entity = new SensorData
            {
                MachineId = data.MachineId,
                Temperature = data.Temperature,
                Vibration = data.Vibration,
                EnergyConsumption = data.EnergyConsumption,
                Timestamp = data.Timestamp
            };

            _context.SensorData.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SensorDataDto>> GetSensorDataByMachineAsync(Guid machineId)
        {
            return await _context.SensorData
                .Where(s => s.MachineId == machineId)
                .OrderByDescending(s => s.Timestamp)
                .Select(s => new SensorDataDto
                {
                    MachineId = s.MachineId,
                    Temperature = s.Temperature,
                    Vibration = s.Vibration,
                    EnergyConsumption = s.EnergyConsumption,
                    Timestamp = s.Timestamp
                })
                .ToListAsync();
        }

        public async Task ProcessSensorDataAsync(SensorDataDto dataDto)
        {
            // Convertir DTO a entidad
            var entity = new SensorData
            {
                Id = Guid.NewGuid(),
                MachineId = dataDto.MachineId,
                Temperature = dataDto.Temperature,
                Vibration = dataDto.Vibration,
                EnergyConsumption = dataDto.EnergyConsumption,
                Timestamp = dataDto.Timestamp
            };

            // Guardar la lectura
            _context.SensorData.Add(entity);
            await _context.SaveChangesAsync();

            // Obtener últimas 10 lecturas de esa máquina
            var recentData = await _context.SensorData
                .Where(s => s.MachineId == dataDto.MachineId)
                .OrderByDescending(s => s.Timestamp)
                .Take(10)
                .ToListAsync();

            // Calcular promedio de temperatura
            double avgTemp = recentData.Average(s => s.Temperature);

            // Regla simple: si la lectura supera 20% del promedio → alerta
            if (dataDto.Temperature > avgTemp * 1.2)
            {
                var alert = new MaintenanceAlert
                {
                    Id = Guid.NewGuid(),
                    MachineId = dataDto.MachineId,
                    Message = $"Temperatura anómala detectada: {dataDto.Temperature} °C",
                    CreatedAt = DateTime.UtcNow
                };

                _context.MaintenanceAlerts.Add(alert);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MaintenanceAlertDto>> GetAlertsByMachineAsync(Guid machineId)
        {
            var alerts = await _context.MaintenanceAlerts
                .Where(a => a.MachineId == machineId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            // Mapear entidad a DTO
            return alerts.Select(a => new MaintenanceAlertDto
            {
                Id = a.Id,
                MachineId = a.MachineId,
                Message = a.Message,
                CreatedAt = a.CreatedAt
            });
        }
    }
}