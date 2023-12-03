namespace SwitchTesterApi.DTOs
{
    public class SwitchDeviceConnectionsDTO
    {
        public int SwitchId { get; set; }
        public required string HostName { get; set; }
        public List<DeviceConnectedDTO>? Devices { get; set; }
    }
}
