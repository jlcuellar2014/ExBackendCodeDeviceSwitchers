using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface ISwitchesService
    {
        Task<List<SwitchDeviceConnectionsDTO>> GetSwitchConnectedAsync();
    }
}