namespace SwitchTesterApi.DTOs
{
    public class GetDeviceConnectionsDTO
    {
        public int DeviceId { get; set; }
        public string HostName { get; set; }
        public List<GetSwitchConnectedDTO> Switches { get; set; }
    }
}
