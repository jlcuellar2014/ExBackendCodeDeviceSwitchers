namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a switch in the switch testing system.
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Gets or sets the unique identifier for the switch.
        /// </summary>
        public int SwitchId { get; set; }

        /// <summary>
        /// Gets or sets the host name associated with the switch.
        /// </summary>
        public required string HostName { get; set; }

        /// <summary>
        /// Gets or sets the list of ports associated with the switch.
        /// </summary>
        public List<SwitchPort> Ports { get; set; } = new List<SwitchPort>();

        /// <summary>
        /// Gets or sets the list of device-switch connections associated with the switch.
        /// </summary>
        public List<DeviceSwitchConnection> Connections { get; set; } = new List<DeviceSwitchConnection>();
    }
}
