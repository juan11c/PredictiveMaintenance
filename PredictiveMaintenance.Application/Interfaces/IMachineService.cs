using PredictiveMaintenance.Application.DTOs.Machine;

namespace PredictiveMaintenance.Application.Interfaces
{
    public interface IMachineService
    {
        Task<MachineResponseDto> AddMachineAsync(MachineRequestDto machineRequestDto);
        Task<IEnumerable<MachineResponseDto>> GetAllMachinesAsync();
        Task<MachineResponseDto?> GetMachineByIdAsync(Guid id);
        Task<MachineResponseDto?> UpdateMachineByIdAsync(Guid id, MachineUpdateDto machineUpdateDto);
        Task<MachineResponseDto?> DeleteMachineByIdAsync(Guid id);
    }
}
