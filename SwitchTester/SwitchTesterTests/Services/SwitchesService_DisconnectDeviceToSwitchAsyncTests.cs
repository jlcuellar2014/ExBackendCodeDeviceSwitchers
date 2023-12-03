using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Services;

namespace Tests.Services
{
    [TestFixture]
    public class SwitchesService_DisconnectDeviceToSwitchAsyncTests
    {
        [Test]
        public async Task NoSpecificPorts_DisconnectsDeviceFromSwitch()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = null };

            fakeContext.DeviceSwitchConnections.AddRange(
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 1 },
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 2 },
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 3 }
             );

            await fakeContext.SaveChangesAsync();

            // Act & Assert
            Assert.DoesNotThrowAsync(async () 
                => await switchesService.DisconnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(fakeContext.DeviceSwitchConnections, Is.Empty);
        }

        [Test]
        public async Task WithSpecificPorts_DisconnectsDeviceFromSwitchWithSpecificPorts()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = [1, 2] };

            fakeContext.DeviceSwitchConnections.AddRange(
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 1 },
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 2 },
                 new() { DeviceId = deviceId, SwitchId = switchId, Port = 3 }
             );

            await fakeContext.SaveChangesAsync();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrowAsync(async ()
                                => await switchesService.DisconnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

                Assert.That(fakeContext.DeviceSwitchConnections, Is.Not.Empty);

                Assert.That(fakeContext.DeviceSwitchConnections.Count(), Is.EqualTo(1));

                Assert.That(fakeContext.DeviceSwitchConnections
                                         .All(c => !portsDTO.Ports.Contains(c.Port)
                                                   && c.DeviceId.Equals(deviceId)
                                                   && c.SwitchId.Equals(switchId)), Is.True);
            });
        }

        [Test]
        public async Task ConnectionsDoNotExist_NothingToRemoveAsync()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            var switchId = 1;
            var deviceId = 1;
            var portsDTO = new PortsDTO { Ports = null };

            fakeContext.DeviceSwitchConnections.AddRange(
                 new DeviceSwitchConnection { DeviceId = 2, SwitchId = switchId, Port = 1 },
                 new DeviceSwitchConnection { DeviceId = 2, SwitchId = switchId, Port = 2 },
                 new DeviceSwitchConnection { DeviceId = 2, SwitchId = switchId, Port = 3 }
             );

            await fakeContext.SaveChangesAsync();

            // Act & Assert
            Assert.DoesNotThrowAsync(async () 
                => await switchesService.DisconnectDeviceToSwitchAsync(switchId, deviceId, portsDTO));

            Assert.That(fakeContext.DeviceSwitchConnections.Count(), Is.EqualTo(3));
        }
    }
}
