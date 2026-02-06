using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.Sensor
{
    /// <summary>
    /// Representa la solicitud para crear un Sensor.
    /// </summary>
    public class SensorRequestDto
    {
        /// <summary>
        /// Id con el que se enlaza el sensor a una máquina.
        /// </summary>
        [Required(ErrorMessage = "El Id de la máquina es obligatorio")]
        public Guid MachineId { get; set; }

        /// <summary>
        /// Tipo de medición (Temperatura, Energía) de la máquina.
        /// </summary>
        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Unidad de medición (°C, Hz) de la máquina.
        /// </summary>
        [Required(ErrorMessage = "La unidad es obligatoria")]
        public string Unit { get; set; } = string.Empty;
    }
}
