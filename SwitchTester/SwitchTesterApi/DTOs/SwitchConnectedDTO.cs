namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for information about a connected switch.
    /// </summary>
    public class SwitchConnectedDTO
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
        /// Gets or sets the list of ports associated with the connected switch. Can be null.
        /// </summary>
        public List<int>? Ports { get; set; }
    }
}
