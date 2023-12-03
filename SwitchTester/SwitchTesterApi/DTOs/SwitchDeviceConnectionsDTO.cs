namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for information about a switch's connections to devices.
    /// </summary>
    public class SwitchDeviceConnectionsDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the connected switch.
        /// </summary>
        public int SwitchId { get; set; }

        /// <summary>
        /// Gets or sets the host name associated with the connected switch.
        /// </summary>
        public required string HostName { get; set; }

        /// <summary>
        /// Gets or sets the list of devices connected to the switch. Can be null.
        /// </summary>
        public List<DeviceConnectedDTO>? Devices { get; set; }
    }
}
