namespace SwitchTesterApi.DTOs
{
    public class DeviceSwitchConnectionsDTO
    {
        public int DeviceId { get; set; }
        public required string HostName { get; set; }
        public List<SwitchConnectedDTO>? Switches { get; set; }
    }
}
