using PredictiveMaintenance.Application.DTOs.Sensor;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface ISensorService
    {
        public Task<SensorResponseDto> AddSensorAsync(SensorRequestDto sensorRequestDto);
        public Task<IEnumerable<SensorResponseDto>> GetAllSensorAsync();
        public Task<SensorResponseDto?> GetSensorByIdAsync(Guid id);
        public Task<SensorResponseDto?> UpdateSensorByIdAsync(Guid id, SensorUpdateDto sensorUpdateDto);
        public Task<SensorResponseDto?> DeleteSensorByIdAsync(Guid id);
    }
}