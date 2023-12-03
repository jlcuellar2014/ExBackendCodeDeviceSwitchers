namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a connection between a device and a switch in the switch testing system.
    /// </summary>
    public class DeviceSwitchConnection
    {
        /// <summary>
        /// Gets or sets the unique identifier of the switch to which the device is connected.
        /// </summary>
        public int SwitchId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the device in the connection.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the port number associated with the connection.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the reference to the switch in the connection.
        /// </summary>
        public Switch? Switch { get; set; }

        /// <summary>
        /// Gets or sets the reference to the device in the connection.
        /// </summary>
        public Device? Device { get; set; }
    }
}
