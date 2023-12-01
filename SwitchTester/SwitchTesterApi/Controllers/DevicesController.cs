using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController(IDevicesService service) : ControllerBase
    {
        private readonly IDevicesService service = service;

        [HttpGet]
        public async Task<ActionResult<DeviceSwitchConnectionsDTO>> GetAsync() {
            try
            {
                var results = await service.GetDeviceConnectedAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
