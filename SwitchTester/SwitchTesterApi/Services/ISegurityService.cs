using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface ISegurityService
    {
        Task CreateUserAsync(ApplicationUserCreateDTO userDTO);
        Task UpdateUserAsync(ApplicationUserUpdateDTO userDTO);
        Task<string> LoginUserAsync(ApplicationUserLoginDTO userDTO);
        
    }
}