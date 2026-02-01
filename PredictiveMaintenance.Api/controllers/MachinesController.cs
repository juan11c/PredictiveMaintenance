using Microsoft.AspNetCore.Mvc;
using PredictiveMaintenance.Application.DTOs.Common;
using PredictiveMaintenance.Application.DTOs.Machine;
using PredictiveMaintenance.Application.Interfaces;

namespace PredictiveMaintenance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachinesController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        /// <summary>
        /// Crea una nueva máquina en el sistema.
        /// </summary>
        /// <param name="machine">Datos de la máquina a registrar.</param>
        /// <returns>La máquina creada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MachineResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMachine([FromBody] MachineRequestDto machine)
        {
            if (string.IsNullOrWhiteSpace(machine.SerialNumber))
                return BadRequest(new ErrorResponseDto
                {
                    ErrorCode = "MACHINE_VALIDATION_ERROR",
                    Message = "El número de serie es requerido",
                    Details = null // Se puede incluir más información
                });

            var createdMachine = await _machineService.AddMachineAsync(machine);

            return CreatedAtAction(nameof(GetMachineById), new { id = createdMachine.Id }, createdMachine);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachines()
        {
            var machines = await _machineService.GetAllMachinesAsync();
            return Ok(machines); // Se devuelve List<MachineResponseDto>
        }

        /// <summary>
        /// Obtiene la información de una máquina por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la máquina.</param>
        /// <returns>La máquina encontrada o un error si no existe.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MachineResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMachineById(Guid id)
        {
            var machine = await _machineService.GetMachineByIdAsync(id);

            if (machine == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    ErrorCode = "MACHINE_NOT_FOUND",
                    Message = $"No se encontró la máquina con id {id}",
                    Details = null // opcional, se puede incluir más info técnica
                });
            }

            return Ok(machine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineById(Guid id)
        {
            var deleted = await _machineService.DeleteMachineByIdAsync(id);
            if (deleted == null)
                return NotFound(new { Message = $"No hay ninguna maquina con el Id {id} para eliminar" });
            return NoContent(); // 204 sin body
        }

        /// <summary>
        /// Actualiza los datos de una máquina existente.
        /// </summary>
        /// <param name="id">Identificador único de la máquina.</param>
        /// <param name="machineUpdateDto">Datos a actualizar de la máquina.</param>
        /// <returns>La máquina actualizada o un error si no existe.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MachineResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMachineById(Guid id, [FromBody] MachineUpdateDto machineUpdateDto)
        {
            if (machineUpdateDto == null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    ErrorCode = "INVALID_REQUEST",
                    Message = "El cuerpo de la solicitud no puede estar vacío"
                });
            }

            var updatedMachine = await _machineService.UpdateMachineByIdAsync(id, machineUpdateDto);

            if (updatedMachine == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    ErrorCode = "MACHINE_NOT_FOUND",
                    Message = $"No se encontró la máquina con id {id}"
                });
            }

            return Ok(updatedMachine);
        }
    }
}