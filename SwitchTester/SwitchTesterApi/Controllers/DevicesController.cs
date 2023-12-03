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
        /// <summary>
        /// Retrieves a list of device switch connections, providing details about the devices connected to switches.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         GET /api/devices
        /// </remarks>
        /// <returns>
        /// A response containing the list of device switch connections on successful retrieval.
        /// </returns>
        /// <response code="200">Returns the list of device switch connections.</response>
        /// <response code="400">If an error occurs during the retrieval process.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OkResponseDTO<List<DeviceSwitchConnectionsDTO>>), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<OkResponseDTO<List<DeviceSwitchConnectionsDTO>>>> GetAsync() {
            try
            {
                var results = await service.GetDeviceConnectedAsync();
                return Ok(new OkResponseDTO<List<DeviceSwitchConnectionsDTO>> { Data = results });
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestResponseDTO());
            }
        }
    }
}
