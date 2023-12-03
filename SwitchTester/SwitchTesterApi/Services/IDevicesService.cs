using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Interface defining operations related to devices.
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Retrieves a list of device-switch connections with associated information.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of <see cref="DeviceSwitchConnectionsDTO"/>.</returns>
        Task<List<DeviceSwitchConnectionsDTO>> GetDeviceConnectedAsync();
    }
}