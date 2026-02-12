using PredictiveMaintenance.Application.DTOs.SensorData;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface ISensorDataService
    {
        public Task<SensorDataResponseDto> AddSensorDataAsync(SensorDataRequestDto sensorDataRequestDto);
        public Task<IEnumerable<SensorDataResponseDto>> GetSensorsByMachineIdAsync(Guid machineId);
        public Task<SensorDataResponseDto?> GetSensorDataByIdAsync(Guid id);
    }
}
