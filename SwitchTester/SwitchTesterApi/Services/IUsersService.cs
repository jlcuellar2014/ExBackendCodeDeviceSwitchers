using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    public interface IUsersService
    {
        Task CreateUserAsync(UserCreateDTO userDTO);
        Task UpdateUserAsync(UserUpdateDTO userDTO);
    }
}