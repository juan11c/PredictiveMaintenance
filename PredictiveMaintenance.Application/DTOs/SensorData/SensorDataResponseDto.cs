using System;

namespace PredictiveMaintenance.Application.DTOs.SensorData
{
    /// <summary>
    /// Representa la respuesta al consultar una lectura de un sensor.
    /// Incluye información del sensor asociado y el valor registrado.
    /// </summary>
    public class SensorDataResponseDto
    {
        /// <summary>
        /// Identificador único de la lectura registrada.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador único del sensor que generó la lectura.
        /// Permite relacionar la lectura con un sensor específico de una máquina.
        /// </summary>
        public Guid SensorId { get; set; }

        /// <summary>
        /// Valor medido por el sensor en la unidad correspondiente.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Momento exacto en que se tomó la lectura.
        /// Este campo es crítico para trazabilidad y análisis histórico.
        /// </summary>
        public DateTime Timestamp { get; set; }

        // Información del sensor asociado
        /// <summary>
        /// Tipo de valor del sensor (grados, temperatura).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Unidad del tipo (30 C°).
        /// </summary>
        public string Unit { get; set; } = string.Empty;

    }
}