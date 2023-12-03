using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SwitchesController"/> class.
    /// </summary>
    /// <param name="service">The switches service to handle operations.</param>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController(ISwitchesService service) : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of switch device connections, providing details about the devices connected to switches.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         GET /api/switches
        /// </remarks>
        /// <returns>
        /// A response containing the list of switch device connections on successful retrieval.
        /// </returns>
        /// <response code="200">Returns the list of switch device connections.</response>
        /// <response code="400">If an error occurs during the retrieval process.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OkResponseDTO<List<SwitchDeviceConnectionsDTO>>), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<OkResponseDTO<List<SwitchDeviceConnectionsDTO>>>> GetAsync()
        {
            try
            {
                var results = await service.GetSwitchConnectedAsync();
                return Ok(new OkResponseDTO<List<SwitchDeviceConnectionsDTO>> { Data = results });
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestResponseDTO());
            }
        }

        /// <summary>
        /// Connects a device to a switch, specifying the switch ID, device ID, and ports to be connected.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         POST /api/security/1/connect-device/1
        ///         Payload:
        ///         {
        ///             "ports": [
        ///                 5101, 5102
        ///             ]
        ///         }
        /// </remarks>
        /// <param name="switchId">The ID of the switch to which the device will be connected.</param>
        /// <param name="deviceId">The ID of the device to be connected to the switch.</param>
        /// <param name="ports">The data containing the ports to be connected.</param>
        /// <returns>
        /// A response indicating the success of the device-to-switch connection.
        /// </returns>
        /// <response code="200">Returns a success response on successful device-to-switch connection.</response>
        /// <response code="400">If the provided data is invalid or if an error occurs during the connection process.</response>
        [HttpPost("{switchId}/connect-device/{deviceId}")]
        [ProducesResponseType(typeof(OkResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<OkResponseDTO>> ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO ports)
        {
            try
            {
                await service.ConnectDeviceToSwitchAsync(switchId, deviceId, ports);
                return Ok(new OkResponseDTO());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new BadRequestResponseDTO { Message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestResponseDTO());
            }
        }

        /// <summary>
        /// Disconnects a device from a switch, specifying the switch ID, device ID, and optional ports to be disconnected.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         DELETE /api/security/1/connect-device/1
        ///         Payload:
        ///         {
        ///             "ports": [
        ///                 5101, 5102
        ///             ]
        ///         }
        /// </remarks>
        /// <param name="switchId">The ID of the switch from which the device will be disconnected.</param>
        /// <param name="deviceId">The ID of the device to be disconnected from the switch.</param>
        /// <param name="ports">Optional data containing specific ports to be disconnected. If null, all ports will be disconnected.</param>
        /// <returns>
        /// A response indicating the success of the device-to-switch disconnection.
        /// </returns>
        /// <response code="200">Returns a success response on successful device-to-switch disconnection.</response>
        /// <response code="400">If the provided data is invalid or if an error occurs during the disconnection process.</response>
        [HttpDelete("{switchId}/connect-device/{deviceId}")]
        [ProducesResponseType(typeof(OkResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<OkResponseDTO>> DisconnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO? ports)
        {
            try
            {
                await service.DisconnectDeviceToSwitchAsync(switchId, deviceId, ports);
                return Ok(new OkResponseDTO());
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestResponseDTO());
            }
        }
    }
}
