namespace PredictiveMaintenance.Application.DTOs.Machine
{
    /// <summary>
    /// Representa la respuesta con los datos de una máquina.
    /// </summary>
    public class MachineResponseDto
    {
        /// <summary>
        /// Identificador único de la máquina.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Número de serie de la máquina.
        /// </summary>
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Modelo de la máquina.
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de instalación de la máquina.
        /// </summary>
        public DateTime InstallationDate { get; set; }
    }
}