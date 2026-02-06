using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.Sensor
{
    /// <summary>
    /// Representa la respuesta de un Sensor.
    /// </summary>
    public class SensorResponseDto
    {
        /// <summary>
        /// Identificador único del sensor.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id con el que se enlaza el sensor a una máquina.
        /// </summary>
        public Guid MachineId { get; set; }

        /// <summary>
        /// Tipo de medición (Temperatura, Energía) de la máquina.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Unidad de medición (°C, Hz) de la máquina.
        /// </summary>
        public string Unit { get; set; } = string.Empty;
    }
}
