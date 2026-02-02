using System;
using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.SensorData
{
    /// <summary>
    /// Representa la solicitud para registrar una lectura de un sensor específico de una máquina.
    /// </summary>
    public class SensorDataRequestDto
    {
        /// <summary>
        /// Identificador único del sensor que genera la lectura.
        /// Relaciona la lectura con un sensor previamente registrado en la máquina.
        /// </summary>
        [Required(ErrorMessage = "El sensor es obligatorio")]
        public Guid SensorId { get; set; }

        /// <summary>
        /// Valor medido por el sensor en la unidad correspondiente.
        /// Ejemplo: temperatura en °C, vibración en Hz, energía en kWh.
        /// </summary>
        [Required(ErrorMessage = "El valor de la lectura es obligatorio")]
        public double Value { get; set; }

        /// <summary>
        /// Momento exacto en que se tomó la lectura.
        /// Este campo es crítico para trazabilidad y análisis histórico.
        /// </summary>
        [Required(ErrorMessage = "El momento de la lectura es obligatorio")]
        public DateTime Timestamp { get; set; }
    }
}