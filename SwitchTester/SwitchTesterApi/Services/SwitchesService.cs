using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;

namespace SwitchTesterApi.Services
{
    public class SwitchesService : ISwitchesService
    {
        private readonly ISwitchTesterContext context;

        public SwitchesService(ISwitchTesterContext context)
        {
            this.context = context;
        }

        public async Task<List<SwitchDeviceConnectionsDTO>> GetSwitchConnectedAsync()
        {
            var response = new List<SwitchDeviceConnectionsDTO>();
            var query = from c in context.DeviceSwitchConnections
                        join s in context.Switches on c.SwitchId equals s.SwitchId
                        join d in context.Devices on c.DeviceId equals d.DeviceId
                        select new
                        {
                            SwitchId = c.SwitchId,
                            SwitchHostName = s.HostName,
                            DeviceId = c.DeviceId,
                            DeviceHostName = d.HostName,
                            Port = c.Port
                        }
                        into selection
                        group selection by new
                        {
                            selection.SwitchId,
                            selection.SwitchHostName
                        };

            var result = await query.ToListAsync();

            foreach (var group in result)
            {
                var newSwitch = new SwitchDeviceConnectionsDTO
                {
                    SwitchId = group.Key.SwitchId,
                    HostName = group.Key.SwitchHostName,
                    Devices = new List<DeviceConnectedDTO>()
                };

                foreach (var s in group)
                {
                    var newDevice = new DeviceConnectedDTO
                    {
                        DeviceId = s.DeviceId,
                        HostName = s.DeviceHostName,
                        Port = s.Port
                    };

                    newSwitch.Devices.Add(newDevice);
                }

                response.Add(newSwitch);
            }

            return response;
        }
    }
}
