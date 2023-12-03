namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a port associated with a device in the switch testing system.
    /// </summary>
    public class DevicePort
    {
        /// <summary>
        /// Gets or sets the unique identifier of the device to which the port belongs.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the reference to the device to which the port belongs.
        /// </summary>
        public Device? Device { get; set; }
    }
}
