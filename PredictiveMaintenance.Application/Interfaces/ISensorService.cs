using PredictiveMaintenance.Application.DTOs.Sensor;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface ISensorService
    {
        public Task<SensorResponseDto> AddSensorAsync();
    }
}