using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService service;

        public DevicesController(IDevicesService service)
        {
            this.service = service;
        }

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
