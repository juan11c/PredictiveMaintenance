using System;

namespace PredictiveMaintenance.Domain.Entities
{
    public class Machine
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Identificador único global
        public string SerialNumber { get; set; } = string.Empty; // Número de serie único
        public string Model { get; set; } = string.Empty; // Modelo de la máquina
        public DateTime InstallationDate { get; set; } // Fecha de instalación
    }
}
