using PredictiveMaintenance.Application.DTOs.SensorData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface ISensorDataService
    {
        public Task<SensorDataRequestDto> AddSensorDataAsync(SensorDataRequestDto sensorDataRequestDto);
    }
}
