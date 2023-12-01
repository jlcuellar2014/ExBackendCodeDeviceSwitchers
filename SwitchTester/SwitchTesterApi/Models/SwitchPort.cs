namespace SwitchTesterApi.Models
{
    public class SwitchPort
    {
        public int SwitchId { get; set; }
        public int Port { get; set; }
        public Switch? Switch { get; set; }
    }
}
