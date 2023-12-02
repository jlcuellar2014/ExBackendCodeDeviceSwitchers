using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Services;

namespace Tests.Services
{
    [TestFixture]
    public class SwitchesService_ConnectDeviceToSwitchAsync
    {
        [Test]
        public async Task ValidData_ConnectsDeviceToSwitch()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [1, 2, 3] };

            fakeContext.Devices.Add(new Device
            {
                DeviceId = deviceId,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = deviceId, Port = 1 },
                    new() { DeviceId = deviceId, Port = 2 },
                    new() { DeviceId = deviceId, Port = 3 }
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = switchId,
                HostName = "Switch Host Name",
                Ports = [
                    new() { SwitchId = switchId, Port = 1 },
                    new() { SwitchId = switchId, Port = 2 },
                    new() { SwitchId = switchId, Port = 3 }
                ]
            });

            await fakeContext.SaveChangesAsync();

            // Act & Asset
            Assert.DoesNotThrowAsync(async() 
                => await switchesService.ConnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(fakeContext.DeviceSwitchConnections
                                   .All(c => portsDTO.Ports.Contains(c.Port) 
                                             && c.DeviceId.Equals(deviceId) 
                                             && c.SwitchId.Equals(switchId)),
                        Is.True);
        }

        [Test]
        public async Task ValidData_ConnectsDeviceToSwitchWithNewPorts()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [2, 3] };

            fakeContext.Devices.Add(new Device
            {
                DeviceId = deviceId,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = deviceId, Port = 1 },
                    new() { DeviceId = deviceId, Port = 2 },
                    new() { DeviceId = deviceId, Port = 3 },
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = switchId,
                HostName = "Switch Host Name",
                Ports = [
                    new() { SwitchId = switchId, Port = 1 },
                    new() { SwitchId = switchId, Port = 2 },
                    new() { SwitchId = switchId, Port = 3 },
                ]
            });

            fakeContext.DeviceSwitchConnections.AddRange(
                new DeviceSwitchConnection { DeviceId = deviceId, SwitchId = switchId, Port = 1 }
            );

            await fakeContext.SaveChangesAsync();

            // Act & Asset
            Assert.DoesNotThrowAsync(async () 
                => await switchesService.ConnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(fakeContext.DeviceSwitchConnections
                                   .Where(c => portsDTO.Ports.Contains(c.Port)
                                                    && c.DeviceId.Equals(deviceId)
                                                    && c.SwitchId.Equals(switchId)).Count(),
                        Is.EqualTo(portsDTO.Ports.Count));
        }

        [Test]
        public async Task DuplicateConnections_ArgumentOutOfRangeException()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [1, 2, 3] };

            fakeContext.Devices.Add(new Device
            {
                DeviceId = deviceId,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = deviceId, Port = 1 },
                    new() { DeviceId = deviceId, Port = 2 },
                    new() { DeviceId = deviceId, Port = 3 }
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = switchId,
                HostName = "Switch Host Name",
                Ports = [
                    new() {  SwitchId = switchId, Port = 1},
                    new() {  SwitchId = switchId, Port = 2},
                    new() {  SwitchId = switchId, Port = 3}
                ]
            });

            fakeContext.DeviceSwitchConnections.AddRange(
                new() { DeviceId = deviceId, SwitchId = switchId, Port = 1 },
                new() { DeviceId = deviceId, SwitchId = switchId, Port = 2 },
                new() { DeviceId = deviceId, SwitchId = switchId, Port = 3 }
            );

            await fakeContext.SaveChangesAsync();

            // Act & Asset
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () 
                => await switchesService.ConnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));
            
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message,
                    Does.Contain("he switch does not support more than one device connection per port."));

                Assert.That(fakeContext.DeviceSwitchConnections
                                         .Any(c => portsDTO.Ports.Contains(c.Port)
                                                   && c.DeviceId.Equals(deviceId)
                                                   && c.SwitchId.Equals(switchId)),
                            Is.True);
            });
        }

        [Test]
        public async Task InvalidPorts_ThrowsArgumentException()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [-1, 0, 42] }; // Invalid ports

            fakeContext.Devices.Add(new Device
            {
                DeviceId = deviceId,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = deviceId, Port = 1 },
                    new() { DeviceId = deviceId, Port = 2 },
                    new() { DeviceId = deviceId, Port = 3 }
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = switchId,
                HostName = "Switch Host Name",
                Ports = [
                    new() { SwitchId = switchId, Port = 1 },
                    new() { SwitchId = switchId, Port = 2 },
                    new() { SwitchId = switchId, Port = 3 }
                ]
            });

            await fakeContext.SaveChangesAsync();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () 
                => await switchesService.ConnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(ex.Message, Does.Contain("Not all ports to be connected can be managed by the device."));
        }

        [Test]
        public async Task InsufficientSwitchPorts_ThrowsArgumentExceptionAsync()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [1, 2, 3] };

            fakeContext.Devices.Add(new Device
            {
                DeviceId = deviceId,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = deviceId, Port = 1 },
                    new() { DeviceId = deviceId, Port = 2 },
                    new() { DeviceId = deviceId, Port = 3 },
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = switchId,
                HostName = "Switch Host Name",
                Ports = [ new() { SwitchId = switchId, Port = 1}]
            });

            await fakeContext.SaveChangesAsync();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () 
                => await switchesService.ConnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(ex.Message, Does.Contain("Not all ports to be connected can be managed by the switch."));
        }
    }
}
