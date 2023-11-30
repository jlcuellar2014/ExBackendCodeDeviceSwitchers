using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface IDevicesService
    {
        Task<List<DeviceSwitchConnectionsDTO>> GetDeviceConnectedAsync();
    }
}