using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.Sensor
{
    /// <summary>
    /// Representa la solicitud para actualizar un Sensor existente.
    /// </summary>
    public class SensorUpdateDto
    {
        /// <summary>
        /// Tipo de medición (Temperatura, Energía, Vibración).
        /// </summary>
        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Unidad de medición (°C, Hz, kWh).
        /// </summary>
        [Required(ErrorMessage = "La unidad es obligatoria")]
        public string Unit { get; set; } = string.Empty;
    }
}