using SwitchTesterApi.DTOs;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Interface defining security-related operations.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Performs user login and returns an authentication token.
        /// </summary>
        /// <param name="userDTO">The user login data.</param>
        /// <returns>A task representing the asynchronous operation that returns the authentication token as a string.</returns>
        Task<string> LoginUserAsync(UserLoginDTO userDTO);
    }
}