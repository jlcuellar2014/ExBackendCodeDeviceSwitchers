using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController : ControllerBase
    {
        private readonly ISwitchesService service;

        public SwitchesController(ISwitchesService service)
        {
            this.service = service;
        }

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


    }
}
