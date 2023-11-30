using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models
{
    public interface ISwitchTesterContext
    {
        DbSet<DevicePort> DevicePorts { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }
        DbSet<Switch> Switches { get; set; }
        DbSet<SwitchPort> SwitchPorts { get; set; }
    }
}