namespace SwitchTesterApi.DTOs
{
    public class SwitchDeviceConnectionsDTO
    {
        public int SwitchId { get; set; }
        public string HostName { get; set; }
        public List<DeviceConnectedDTO> Devices { get; set; }
    }
}
