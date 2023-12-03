namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a port associated with a switch in the switch testing system.
    /// </summary>
    public class SwitchPort
    {
        /// <summary>
        /// Gets or sets the unique identifier of the switch to which the port belongs.
        /// </summary>
        public int SwitchId { get; set; }

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the reference to the switch to which the port belongs.
        /// </summary>
        public Switch? Switch { get; set; }
    }
}
