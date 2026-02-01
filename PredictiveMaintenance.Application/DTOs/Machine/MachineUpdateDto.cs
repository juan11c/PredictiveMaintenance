namespace PredictiveMaintenance.Application.DTOs.Machine;

/// <summary>
/// Representa la solicitud para actualizar una máquina existente.
/// </summary>
public class MachineUpdateDto
{
    /// <summary>
    /// Número de serie de la máquina (opcional si no se actualiza).
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Modelo de la máquina (opcional si no se actualiza).
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Fecha de instalación de la máquina (opcional si no se actualiza).
    /// </summary>
    public DateTime? InstallationDate { get; set; }
}
