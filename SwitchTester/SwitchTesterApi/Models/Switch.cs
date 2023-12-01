namespace SwitchTesterApi.Models
{
    public class Switch
    {
        public int SwitchId { get; set; }
        public required string HostName { get; set; }
        public List<SwitchPort> Ports { get; set; } = new List<SwitchPort>();
        public List<DeviceSwitchConnection> Connections { get; set; } = new List<DeviceSwitchConnection>();
    }
}
