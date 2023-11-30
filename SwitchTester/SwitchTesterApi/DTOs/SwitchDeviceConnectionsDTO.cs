namespace SwitchTesterApi.DTOs
{
    public class GetSwitchConnectionsDTO
    {
        public int SwitchId { get; set; }
        public string HostName { get; set; }
        public List<GetDeviceConnectedDTO> Devices { get; set; }
    }
}
