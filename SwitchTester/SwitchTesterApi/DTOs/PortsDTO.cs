namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for a collection of ports.
    /// </summary>
    public class PortsDTO
    {
        /// <summary>
        /// Gets or sets the list of ports associated with the DTO.
        /// </summary>
        public List<int> Ports { get; set; } = [];
    }
}
