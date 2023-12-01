namespace SwitchTesterApi.Models
{
    public class DeviceSwitchConnection
    {
        public int SwitchId { get; set; }
        public int DeviceId { get; set; }
        public int Port { get; set; }
        public Switch? Switch { get; set; }
        public Device? Device { get; set; }
    }
}
