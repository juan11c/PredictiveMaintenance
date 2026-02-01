using System;

namespace PredictiveMaintenance.Domain.Entities
{
    public class MaintenanceAlert
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Identificador único
        public Guid MachineId { get; set; }            // Relación con la máquina
        public string Message { get; set; } = string.Empty; // Texto de la alerta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Fecha de creación
        public bool IsResolved { get; set; } = false;  // Estado de la alerta
    }
}