using Microsoft.AspNetCore.Mvc;
using PredictiveMaintenance.Application.DTOs.SensorData;
using PredictiveMaintenance.Infrastructure.services;

namespace PredictiveMaintenance.Api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly SensorDataService _sensorDataService;

        public SensorDataController (SensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        /// <summary>
        /// Registra una nueva lectura de un sensor.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SensorDataResponseDto>> AddSensorDataAsync([FromBody] SensorDataRequestDto sensorDataRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sensorDataService.AddSensorDataAsync(sensorDataRequestDto);
            return CreatedAtAction(nameof(GetSensorDataById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Obtiene una lectura específica por su Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDataResponseDto>> GetSensorDataById(Guid id)
        {
            var result = await _sensorDataService.GetSensorsByMachineIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las lecturas de sensores asociadas a una máquina.
        /// </summary>
        [HttpGet("machine/{machineId}")]
        public async Task<ActionResult<IEnumerable<SensorDataResponseDto>>> GetSensorsByMachineId(Guid machineId)
        {
            var result = await _sensorDataService.GetSensorsByMachineIdAsync(machineId);
            return Ok(result);
        }
    }
}
