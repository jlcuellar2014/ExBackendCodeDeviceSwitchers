using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Interface defining operations related to user management.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Creates a new user based on the provided user data.
        /// </summary>
        /// <param name="userDTO">The user creation data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateUserAsync(UserCreateDTO userDTO);

        /// <summary>
        /// Updates a user's information, including the password.
        /// </summary>
        /// <param name="userDTO">The user update data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateUserAsync(UserUpdateDTO userDTO);
    }
}