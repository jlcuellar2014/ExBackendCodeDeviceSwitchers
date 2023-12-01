namespace SwitchTesterApi.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public required string HostName { get; set; }
        public List<DevicePort> Ports { get; set; } = new List<DevicePort>();
        public List<DeviceSwitchConnection> Connections { get; set; } = new List<DeviceSwitchConnection>();
    }
}
