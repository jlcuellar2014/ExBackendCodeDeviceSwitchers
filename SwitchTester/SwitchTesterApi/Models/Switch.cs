namespace SwitchTesterApi.Models
{
    public class Switch
    {
        public int SwitchId { get; set; }
        public string HostName { get; set; }
        public List<SwitchPort> Ports { get; set;}
        public List<DeviceSwitchConnection> Connections { get; set; }
    }
}
