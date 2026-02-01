using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Application.DTOs.Machine
{
    /// <summary>
    /// Representa la solicitud para crear una máquina.
    /// </summary>
    public class MachineRequestDto
    {
        /// <summary>
        /// Número de serie único de la máquina.
        /// </summary>
        [Required(ErrorMessage = "El número de serie es obligatorio")]
        [StringLength(50, ErrorMessage = "El número de serie no puede superar los 50 caracteres")]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Modelo de la máquina.
        /// </summary>
        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100, ErrorMessage = "El modelo no puede superar los 100 caracteres")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de instalación de la máquina.
        /// </summary>
        [Required(ErrorMessage = "La fecha de instalación es obligatoria")]
        public DateTime InstallationDate { get; set; }
    }
}