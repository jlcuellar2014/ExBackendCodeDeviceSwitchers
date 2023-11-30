namespace SwitchTesterApi.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string HostName { get; set; }
        public List<DevicePort> Ports { get; set; }
        public List<DeviceSwitchConnection> Connections { get; set; }
    }
}
