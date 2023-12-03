using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Service responsible for handling operations related to switches and their connections.
    /// </summary>
    public class SwitchesService(ISwitchTesterContext context) : ISwitchesService
    {
        /// <summary>
        /// Retrieves information about connected devices grouped by switches.
        /// </summary>
        /// <returns>A list of switch device connections.</returns>
        public async Task<List<SwitchDeviceConnectionsDTO>> GetSwitchConnectedAsync()
        {
            var response = new List<SwitchDeviceConnectionsDTO>();

            var query = from c in context.DeviceSwitchConnections
                        group c by c.SwitchId into g
                        select new
                        {
                            SwitchId = g.Key,
                            SwitchHostName = context.Switches.First(x => x.SwitchId.Equals(g.Key)).HostName,
                            Devices = g.Select(x => new { x.DeviceId, x.Port })
                        };

            var result = await query.ToListAsync();
            var deviceNames = new Dictionary<int, string>();

            foreach (var r in await query.ToListAsync())
            {
                var newSwitch = new SwitchDeviceConnectionsDTO
                {
                    SwitchId = r.SwitchId,
                    HostName = r.SwitchHostName,
                    Devices = []
                };

                foreach (var s in r.Devices.GroupBy(x => x.DeviceId))
                {
                    if (!deviceNames.TryGetValue(s.Key, out string? deviceHostName))
                    {

                        var deviceDb = await context.Devices.FirstOrDefaultAsync(x => x.DeviceId.Equals(s.Key));

                        deviceHostName = deviceDb?.HostName ?? string.Empty;
                        deviceNames.Add(s.Key, deviceHostName);
                    }

                    var newDevice = new DeviceConnectedDTO
                    {
                        DeviceId = s.Key,
                        HostName = deviceHostName,
                        Ports = s.Select(x => x.Port).ToList()
                    };

                    newSwitch.Devices.Add(newDevice);
                }

                response.Add(newSwitch);
            }

            return response;
        }

        /// <summary>
        /// Connects a device to a switch with specified ports.
        /// </summary>
        /// <param name="switchId">The ID of the switch.</param>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="portsDTO">The ports DTO containing the ports to be connected.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the switch does not support more than one device connection per port.
        /// </exception>
        public async Task ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO portsDTO)
        {
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

                try
                {
                    context.DeviceSwitchConnections.Add(newConnection);
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(portsDTO), "The switch does not support more than one device connection per port.");
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Disconnects a device from a switch with specified ports.
        /// </summary>
        /// <param name="switchId">The ID of the switch.</param>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="portsDTO">The optional ports DTO containing the ports to be disconnected.</param>
        public async Task DisconnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO? portsDTO)
        {
            List<int> ports = portsDTO?.Ports ?? [];

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
                throw new ArgumentOutOfRangeException(nameof(ports), "Not all ports to be connected can be managed by the device.");
        }
        
        private async Task ValidateSwitchPorts(int switchId, List<int> ports)
        {
            var portsInSwitch = await context.SwitchPorts
                                             .Where(a => a.SwitchId.Equals(switchId))
                                             .Select(a => a.Port).ToListAsync();

            var areAllInSwitch = ports.All(p => portsInSwitch.Contains(p));

            if (!areAllInSwitch)
                throw new ArgumentOutOfRangeException(nameof(ports), "Not all ports to be connected can be managed by the switch.");
        }
    }
}
