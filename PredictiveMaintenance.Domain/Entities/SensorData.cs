using System;

namespace PredictiveMaintenance.Domain.Entities
{
    public class SensorData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SensorId { get; set; } // Relación con el sensor
        public double Value { get; set; }  // Valor medido
        public DateTime Timestamp { get; set; }
    }
}