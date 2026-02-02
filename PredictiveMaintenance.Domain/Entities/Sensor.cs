namespace PredictiveMaintenance.Domain.Entities
{
    public class Sensor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MachineId { get; set; }
        public string Type { get; set; } = string.Empty; // Temperature, Vibration, Energy
        public string Unit { get; set; } = string.Empty; // °C, Hz, kWh
    }
}
