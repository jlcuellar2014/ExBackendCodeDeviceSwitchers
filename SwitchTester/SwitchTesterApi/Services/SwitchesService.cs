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

        public async Task ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO portsDTO) {

            var ports = portsDTO.Ports;

            await ValidateDevicePorts(deviceId, ports);
            await ValidateSwitchPorts(switchId, ports);

            foreach (var port in ports)
            {
                var newConnection = new DeviceSwitchConnection { 
                    DeviceId = deviceId,
                    SwitchId = switchId,
                    Port = port
                };

                context.DeviceSwitchConnections.Add(newConnection);
            }

            await context.SaveChangesAsync();
        }

        public async Task DisconnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO? portsDTO)
        {
            List<int> ports = portsDTO?.Ports ?? new List<int>();

            var queryConnections = context.DeviceSwitchConnections.Where(c => c.SwitchId.Equals(switchId) && c.DeviceId.Equals(deviceId));

            if (ports.Any()){
                queryConnections = queryConnections.Where(c => ports.Contains(c.Port));
            }

            context.DeviceSwitchConnections.RemoveRange(queryConnections);
            await context.SaveChangesAsync();
        }

        private async Task ValidateDevicePorts(int deviceId, List<int> ports)
        {
            var portsInDevice = await context.DevicePorts
                                             .Where(d => d.DeviceId.Equals(deviceId))
                                             .Select(d => d.Port).ToListAsync();

            var areAllInDevice = ports.All(p => portsInDevice.Contains(p));

            if (!areAllInDevice)
                throw new ArgumentOutOfRangeException("Not all ports to be connected can be managed by the device.");
        }
        
        private async Task ValidateSwitchPorts(int switchId, List<int> ports)
        {
            var portsInSwitch = await context.SwitchPorts
                                             .Where(a => a.SwitchId.Equals(switchId))
                                             .Select(a => a.Port).ToListAsync();

            var areAllInSwitch = ports.All(p => portsInSwitch.Contains(p));

            if (!areAllInSwitch)
                throw new ArgumentOutOfRangeException("Not all ports to be connected can be managed by the switch.");
        }
    }
}
