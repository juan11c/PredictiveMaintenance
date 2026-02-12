using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Sensor;
using PredictiveMaintenance.Application.Interfaces;
using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence;

namespace PredictiveMaintenance.Infrastructure.Services
{
    public class SensorService : ISensorService
    {
        private readonly AppDbContext _context;

        public SensorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SensorResponseDto> AddSensorAsync(SensorRequestDto sensorRequestDto)
        {
            var sensor = new Sensor
            {
                Id = Guid.NewGuid(),
                MachineId = sensorRequestDto.MachineId,
                Type = sensorRequestDto.Type,
                Unit = sensorRequestDto.Unit
            };

            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return new SensorResponseDto
            {
                Id = sensor.Id,
                MachineId = sensor.MachineId,
                Type = sensor.Type,
                Unit = sensor.Unit
            };
        }

        public async Task<IEnumerable<SensorResponseDto>> GetAllSensorAsync()
        {
            return await _context.Sensors
                .Select(s => new SensorResponseDto
                {
                    Id = s.Id,
                    MachineId = s.MachineId,
                    Type = s.Type,
                    Unit = s.Unit
                })
                .ToListAsync();
        }

        public async Task<SensorResponseDto?> GetSensorByIdAsync(Guid id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null) return null;

            return new SensorResponseDto
            {
                Id = sensor.Id,
                MachineId = sensor.MachineId,
                Type = sensor.Type,
                Unit = sensor.Unit
            };
        }

        public async Task<SensorResponseDto?> UpdateSensorByIdAsync(Guid id, SensorUpdateDto sensorUpdateDto)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null) return null;

            sensor.Type = sensorUpdateDto.Type;
            sensor.Unit = sensorUpdateDto.Unit;

            await _context.SaveChangesAsync();

            return new SensorResponseDto
            {
                Id = sensor.Id,
                MachineId = sensor.MachineId,
                Type = sensor.Type,
                Unit = sensor.Unit
            };
        }

        public async Task<SensorResponseDto?> DeleteSensorByIdAsync(Guid id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null) return null;

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return new SensorResponseDto
            {
                Id = sensor.Id,
                MachineId = sensor.MachineId,
                Type = sensor.Type,
                Unit = sensor.Unit
            };
        }
    }
}