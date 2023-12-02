using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterApi.Services
{
    public class DevicesService(ISwitchTesterContext context) : IDevicesService
    {
        public async Task<List<DeviceSwitchConnectionsDTO>> GetDeviceConnectedAsync()
        {
            var response = new List<DeviceSwitchConnectionsDTO>();
            var query = from c in context.DeviceSwitchConnections
                        join d in context.Devices on c.DeviceId equals d.DeviceId
                        join s in context.Switches on c.SwitchId equals s.SwitchId
                        select new
                        {
                            DeviceId = c.DeviceId,
                            DeviceHostName = d.HostName,
                            SwitchId = c.SwitchId,
                            SwitchHostName = s.HostName,
                            Port = c.Port
                        }
                        into selection
                        group selection by new
                        {
                            selection.DeviceId,
                            selection.DeviceHostName
                        };

            var result = await query.ToListAsync();

            foreach (var group in result)
            {
                var newDevice = new DeviceSwitchConnectionsDTO
                {
                    DeviceId = group.Key.DeviceId,
                    HostName = group.Key.DeviceHostName,
                    Switches = []
                };

                foreach (var s in group)
                {
                    var newSwitch = new SwitchConnectedDTO
                    {
                        SwitchId = s.SwitchId,
                        HostName = s.SwitchHostName,
                        Port = s.Port
                    };

                    newDevice.Switches.Add(newSwitch);
                }

                response.Add(newDevice);
            }

            return response;
        }
    }
}
