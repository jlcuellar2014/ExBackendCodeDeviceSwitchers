using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Service class responsible for handling operations related to devices.
    /// </summary>
    public class DevicesService(ISwitchTesterContext context) : IDevicesService
    {
        /// <summary>
        /// Retrieves a list of device-switch connections with associated information.
        /// </summary>
        /// <returns>A list of <see cref="DeviceSwitchConnectionsDTO"/> representing device-switch connections.</returns>
        public async Task<List<DeviceSwitchConnectionsDTO>> GetDeviceConnectedAsync()
        {
            var response = new List<DeviceSwitchConnectionsDTO>();

            var query = from c in context.DeviceSwitchConnections
                        group c by c.DeviceId into g
                        select new
                        {
                            DeviceId = g.Key,
                            DeviceHostName = context.Devices.First(x => x.DeviceId.Equals(g.Key)).HostName,
                            Switches = g.Select(x => new { x.SwitchId, x.Port })
                        };

            var switchNames = new Dictionary<int, string>();

            foreach (var r in await query.ToListAsync())
            {
                var newDevice = new DeviceSwitchConnectionsDTO
                {
                    DeviceId = r.DeviceId,
                    HostName = r.DeviceHostName,
                    Switches = []
                };

                foreach (var s in r.Switches.GroupBy(x => x.SwitchId))
                {
                    if (!switchNames.TryGetValue(s.Key, out string? switchHostName)) {

                        var switchDb = await context.Switches.FirstOrDefaultAsync(x => x.SwitchId.Equals(s.Key));

                        switchHostName = switchDb?.HostName ?? string.Empty;
                        switchNames.Add(s.Key, switchHostName);
                    }

                    var newSwitch = new SwitchConnectedDTO
                    {
                        SwitchId = s.Key,
                        HostName = switchHostName,
                        Ports = s.Select(x => x.Port).ToList()
                    };

                    newDevice.Switches.Add(newSwitch);
                }

                response.Add(newDevice);
            }

            return response;
        }
    }
}
