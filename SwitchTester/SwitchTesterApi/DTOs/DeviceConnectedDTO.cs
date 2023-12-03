namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for information about a connected device.
    /// </summary>
    public class DeviceConnectedDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the connected device.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the host name associated with the connected device.
        /// </summary>
        public required string HostName { get; set; }

        /// <summary>
        /// Gets or sets the list of ports associated with the connected device. Can be null.
        /// </summary>
        public List<int>? Ports { get; set; }
    }

}
