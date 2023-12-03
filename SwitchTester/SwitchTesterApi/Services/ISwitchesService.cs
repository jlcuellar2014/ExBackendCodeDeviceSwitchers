using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Interface defining operations related to switches.
    /// </summary>
    public interface ISwitchesService
    {
        /// <summary>
        /// Retrieves a list of switch-device connections with associated information.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of <see cref="SwitchDeviceConnectionsDTO"/>.</returns>
        Task<List<SwitchDeviceConnectionsDTO>> GetSwitchConnectedAsync();

        /// <summary>
        /// Connects a device to a switch with specified ports.
        /// </summary>
        /// <param name="switchId">The ID of the switch.</param>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="ports">The ports to connect.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO ports);

        /// <summary>
        /// Disconnects a device from a switch with specified ports (optional).
        /// </summary>
        /// <param name="switchId">The ID of the switch.</param>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="portsDTO">Optional ports to disconnect.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DisconnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO? portsDTO);
    }
}