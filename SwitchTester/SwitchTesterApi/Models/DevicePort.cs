namespace SwitchTesterApi.Models
{
    public class DevicePort
    {
        public int DeviceId { get; set; }
        public int Port { get; set; }
        public Device Device { get; set; }
    }
}
