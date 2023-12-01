using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface ISecurityService
    {
        Task<string> LoginUserAsync(UserLoginDTO userDTO);
        
    }
}