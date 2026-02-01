using System;

namespace PredictiveMaintenance.Application.DTOs.Telemetry
{
    public class SensorDataDto
    {
        public Guid MachineId { get; set; }            // Relación con la máquina
        public double Temperature { get; set; }        // °C
        public double Vibration { get; set; }          // Hz
        public double EnergyConsumption { get; set; }  // kWh
        public DateTime Timestamp { get; set; }        // Momento de la lectura
    }
}