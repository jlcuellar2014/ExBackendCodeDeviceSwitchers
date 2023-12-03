namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a device in the switch testing system.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the unique identifier for the device.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the host name associated with the device.
        /// </summary>
        public required string HostName { get; set; }

        /// <summary>
        /// Gets or sets the list of ports associated with the device.
        /// </summary>
        public List<DevicePort> Ports { get; set; } = new List<DevicePort>();

        /// <summary>
        /// Gets or sets the list of device-switch connections associated with the device.
        /// </summary>
        public List<DeviceSwitchConnection> Connections { get; set; } = new List<DeviceSwitchConnection>();
    }

}
