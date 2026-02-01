
using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.SensorData
{
    /// <summary>
    /// Representa la solicitud para agregar un sensor de una maquina relacionada.
    /// </summary>
    public class SensorDataRequestDto
    {
        /// <summary>
        /// Id de relación con la maquina.
        /// </summary>
        [Required(ErrorMessage = "La relación es obligatoria")]
        public Guid MachineId { get; set; } // Relación con la máquina

        /// <summary>
        /// Campo que muestra la temperatura de la maquina.
        /// </summary>
        [Required(ErrorMessage = "La temperatura es obligatoria")]
        public double Temperature { get; set; }        // °C

        /// <summary>
        /// Campo que muestra la vibración de la maquina en Hz.
        /// </summary>
        public double Vibration { get; set; }          // Hz

        /// <summary>
        /// Campo que muestra la energía usada por la maquina en kwh.
        /// </summary>
        public double EnergyConsumption { get; set; }  // kWh

        /// <summary>
        /// Campo que muestra el momento de la lectura.
        /// </summary>
        public DateTime Timestamp { get; set; }        // Momento de la lectura
    }
}
