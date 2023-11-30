using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface ISwitchesService
    {
        Task<List<SwitchDeviceConnectionsDTO>> GetSwitchConnectedAsync();
        Task ConnectDeviceToSwitchAsync(int switchId, int deviceId, PortsDTO ports);
    }
}