using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController(ISwitchesService service) : ControllerBase
    {
        private readonly ISwitchesService service = service;

        [HttpGet]
        public async Task<ActionResult<SwitchDeviceConnectionsDTO>> GetAsync()
        {
            try
            {
                var results = await service.GetSwitchConnectedAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("{switchId}/connect-device/{deviceId}")]
        public async Task<IActionResult> ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO ports)
        {
            try
            {
                await service.ConnectDeviceToSwitchAsync(switchId, deviceId, ports);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{switchId}/connect-device/{deviceId}")]
        public async Task<IActionResult> DisconnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO? ports)
        {
            try
            {
                await service.DisconnectDeviceToSwitchAsync(switchId, deviceId, ports);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
