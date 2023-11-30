namespace SwitchTesterApi.DTOs
{
    public class GetDeviceConnectedDTO
    {
        public int DeviceId { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }
}
