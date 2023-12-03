using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Services;

namespace SwitchTesterUnitTests.Services
{
    [TestFixture]
    public class SwitchesService_GetSwitchConnectedAsyncTests
    {
        [Test]
        public async Task ReturnsSwitchDeviceConnectionsDTOList()
        {
            // Arrange
            using var fakeContext = new FakeSwitchTesterContext();
            var switchesService = new SwitchesService(fakeContext);

            fakeContext.Devices.Add(new Device
            {
                DeviceId = 1,
                HostName = "Device Host Name",
                Ports = [
                    new() { DeviceId = 1, Port = 1 },
                    new() { DeviceId = 1, Port = 2 },
                    new() { DeviceId = 1, Port = 3 }
                ]
            });

            fakeContext.Switches.Add(new Switch
            {
                SwitchId = 1,
                HostName = "Switch Host Name",
                Ports = [
                    new() { SwitchId = 1, Port = 1 },
                    new() { SwitchId = 1, Port = 2 },
                    new() { SwitchId = 1, Port = 3 }
                ]
            });

            fakeContext.DeviceSwitchConnections.AddRange(
                new() { DeviceId = 1, SwitchId = 1, Port = 1 },
                new() { DeviceId = 1, SwitchId = 1, Port = 2 },
                new() { DeviceId = 1, SwitchId = 1, Port = 3 }
            );

            await fakeContext.SaveChangesAsync();

            // Act
            var result = await switchesService.GetSwitchConnectedAsync();

            // Assert
            Assert.Multiple(() => {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(fakeContext.Switches.Count()));
            });
            
        }
    }
}
