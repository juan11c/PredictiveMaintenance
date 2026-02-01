using PredictiveMaintenance.Application.DTOs.Alerts;
using PredictiveMaintenance.Application.DTOs.Telemetry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface ITelemetryService
    {
        Task AddSensorDataAsync(SensorDataDto data);
        Task<List<SensorDataDto>> GetSensorDataByMachineAsync(Guid machineId);
        Task ProcessSensorDataAsync(SensorDataDto dataDto);
        Task<IEnumerable<MaintenanceAlertDto>> GetAlertsByMachineAsync(Guid machineId);

    }
}