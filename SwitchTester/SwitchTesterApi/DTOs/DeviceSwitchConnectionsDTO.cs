namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for information about a device's connections to switches.
    /// </summary>
    public class DeviceSwitchConnectionsDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the connected device.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the host name associated with the connected device.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the list of switches to which the device is connected. Can be null.
        /// </summary>
        public List<SwitchConnectedDTO>? Switches { get; set; }
    }

}
