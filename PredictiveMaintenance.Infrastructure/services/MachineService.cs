using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Machine;
using PredictiveMaintenance.Application.Interfaces;
using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence;

namespace PredictiveMaintenance.Infrastructure.Services
{
    public class MachineService : IMachineService
    {
        private readonly AppDbContext _context;

        public MachineService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MachineResponseDto> AddMachineAsync(MachineRequestDto machineRequestDto)
        {
            var machine = new Machine
            {
                Id = Guid.NewGuid(),
                SerialNumber = machineRequestDto.SerialNumber,
                Model = machineRequestDto.Model,
                InstallationDate = machineRequestDto.InstallationDate
            };

            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            // Creo el objeto de respuesta
            return new MachineResponseDto
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Model = machine.Model,
                InstallationDate = machine.InstallationDate
            };
        }
        public async Task<IEnumerable<MachineResponseDto>> GetAllMachinesAsync()
        {
            return await _context.Machines
                .Select(m => new MachineResponseDto
                {
                    Id = m.Id,
                    SerialNumber = m.SerialNumber,
                    Model = m.Model,
                    InstallationDate = m.InstallationDate
                })
                .ToListAsync();
        }
        public async Task<MachineResponseDto?> GetMachineByIdAsync(Guid id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null) return null;

            return new MachineResponseDto
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Model = machine.Model,
                InstallationDate = machine.InstallationDate
            };
        }
        public async Task<MachineResponseDto?> DeleteMachineByIdAsync(Guid id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null) return null;

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return new MachineResponseDto
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Model = machine.Model,
                InstallationDate = machine.InstallationDate
            };
        }

        public async Task<MachineResponseDto?> UpdateMachineByIdAsync(Guid id, MachineUpdateDto machineUpdateDto)
        {
            var machine = await _context.Machines.FindAsync(id);

            if (machine == null) return null;

            // Aplicar cambios solo si se enviaron
            if (!string.IsNullOrWhiteSpace(machineUpdateDto.SerialNumber))
                machine.SerialNumber = machineUpdateDto.SerialNumber;

            if (!string.IsNullOrWhiteSpace(machineUpdateDto.Model))
                machine.Model = machineUpdateDto.Model;

            if (machineUpdateDto.InstallationDate.HasValue)
                machine.InstallationDate = machineUpdateDto.InstallationDate.Value;

            // Guardar cambios
            await _context.SaveChangesAsync();

            // Devolver DTO actualizado
            return new MachineResponseDto
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Model = machine.Model,
                InstallationDate = machine.InstallationDate
            };
        }
    }
}