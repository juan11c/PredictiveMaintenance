using Microsoft.AspNetCore.Mvc;
using PredictiveMaintenance.Application.DTOs.Sensor;
using PredictiveMaintenance.Application.Interfaces;

namespace PredictiveMaintenance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Crea un nuevo sensor asociado a una máquina.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SensorResponseDto>> AddSensor([FromBody] SensorRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sensorService.AddSensorAsync(request);
            return CreatedAtAction(nameof(GetSensorById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Obtiene todos los sensores registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorResponseDto>>> GetAllSensors()
        {
            var result = await _sensorService.GetAllSensorAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un sensor específico por su Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorResponseDto>> GetSensorById(Guid id)
        {
            var result = await _sensorService.GetSensorByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Actualiza un sensor existente por su Id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<SensorResponseDto>> UpdateSensor(Guid id, [FromBody] SensorUpdateDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sensorService.UpdateSensorByIdAsync(id, request);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Elimina un sensor existente por su Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SensorResponseDto>> DeleteSensor(Guid id)
        {
            var result = await _sensorService.DeleteSensorByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}