using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.DTOs.Telemetry;
using PredictiveMaintenance.Application.Interfaces;

namespace PredictiveMaintenance.Api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;

        public TelemetryController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        // POST: api/telemetry/add
        [HttpPost("add")]
        public async Task<ActionResult> AddSensorData([FromBody] SensorDataDto data)
        {
            if (data == null)
                return BadRequest("Sensor data is required.");

            try
            {
                await _telemetryService.ProcessSensorDataAsync(data);
                return Ok(new { Message = "Sensor data added successfully" });
            }
            catch (Exception ex)
            {
                // Log error (usar ILogger en producción)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/telemetry/machine/{machineId}
        [HttpGet("machine/{machineId}")]
        public async Task<ActionResult<IEnumerable<SensorDataDto>>> GetSensorDataByMachine(Guid machineId)
        {
            var data = await _telemetryService.GetSensorDataByMachineAsync(machineId);

            if (data == null || !data.Any())
                return NotFound($"No sensor data found for machine {machineId}");

            return Ok(data);
        }

        [HttpGet("alerts/{machineId}")]
        public async Task<IActionResult> GetAlertsByMachine(Guid machineId)
        {
            var alerts = await _telemetryService.GetAlertsByMachineAsync(machineId);

            if (alerts == null || !alerts.Any())
                return NotFound($"No alerts found for machine {machineId}");

            return Ok(alerts);
        }
    }
}