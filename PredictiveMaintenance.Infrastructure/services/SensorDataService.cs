using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.SensorData;
using PredictiveMaintenance.Application.Interfaces;
using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence;

namespace PredictiveMaintenance.Infrastructure.services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly AppDbContext _context;

        public SensorDataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SensorDataResponseDto> AddSensorDataAsync(SensorDataRequestDto sensorDataRequestDto)
        {
            var sensorsData = new SensorData
            {
                Id = Guid.NewGuid(),
                SensorId = sensorDataRequestDto.SensorId,
                Value = sensorDataRequestDto.Value,
                Timestamp = sensorDataRequestDto.Timestamp
            };

            _context.SensorData.Add(sensorsData);
            await _context.SaveChangesAsync();

            return new SensorDataResponseDto
            {
                Id = sensorsData.Id,
                SensorId = sensorsData.SensorId,
                Value = sensorsData.Value,
                Timestamp = sensorsData.Timestamp
            };
        }

        public async Task<SensorDataResponseDto?> GetSensorDataByIdAsync(Guid id)
        {
            var sensorData = await _context.SensorData.FindAsync(id);

            if (sensorData == null) return null;

            return new SensorDataResponseDto
            {
                Id = sensorData.Id,
                SensorId = sensorData.SensorId,
                Value = sensorData.Value,
                Timestamp = sensorData.Timestamp
            };
        }

        public async Task<IEnumerable<SensorDataResponseDto>> GetSensorsByMachineIdAsync(Guid machineId)
        {
            return await _context.SensorData
                .Join(_context.Sensors,
                      data => data.SensorId,
                      sensor => sensor.Id,
                      (data, sensor) => new { data, sensor })
                .Where(x => x.sensor.MachineId == machineId)
                .Select(x => new SensorDataResponseDto

                {
                    Id = x.data.Id,
                    SensorId = x.data.SensorId,
                    Value = x.data.Value,
                    Timestamp = x.data.Timestamp,
                    // opcional: incluir info del sensor
                    Type = x.sensor.Type,
                    Unit = x.sensor.Unit
                })
                .ToListAsync();
        }
    }
}
