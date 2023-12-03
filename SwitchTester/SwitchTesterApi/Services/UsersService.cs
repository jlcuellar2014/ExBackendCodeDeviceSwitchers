using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterApi.Services
{
    /// <summary>
    /// Service responsible for user-related operations.
    /// </summary>
    public class UsersService(ISecurityContext context) : IUsersService
    {
        /// <summary>
        /// Creates a new user with the provided user data.
        /// </summary>
        /// <param name="userDTO">The data for creating a new user.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the username is already in use.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown for general errors during user creation.
        /// </exception>
        public async Task CreateUserAsync(UserCreateDTO userDTO)
        {
            try
            {
                UserHelpers.CheckUserNameFormat(userDTO.UserName);
                UserHelpers.CheckPasswordFormat(userDTO.Password);

                var salt = UserHelpers.GenerateSalt();
                var passwordHash = UserHelpers.GeneratePasswordHash(userDTO.Password, salt);

                var newAppUser = new User()
                {
                    UserName = userDTO.UserName,
                    PasswordHash = passwordHash,
                    Salt = salt
                };

                context.ApplicationUsers.Add(newAppUser);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("same key value") ||
                        (ex.InnerException?.Message
                                           .Contains("unique", StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    throw new ArgumentException("The username is already in use.", nameof(userDTO.UserName));
                }

                throw;
            }
        }

        /// <summary>
        /// Updates an existing user with the provided user data.
        /// </summary>
        /// <param name="userDTO">The data for updating an existing user.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided username or password is invalid.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown for general errors during user update.
        /// </exception>
        public async Task UpdateUserAsync(UserUpdateDTO userDTO)
        {
            UserHelpers.CheckPasswordFormat(userDTO.NewPassword);

            var userDb = await context.ApplicationUsers
                                      .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName))
                                        ?? throw new ArgumentException("The user with the assigned credentials does not exist.", nameof(userDTO));

            UserHelpers.CheckIfPasswordCorrect(userDTO.Password, userDb.Salt, userDb.PasswordHash);

            var newSalt = UserHelpers.GenerateSalt();
            var newPassHash = UserHelpers.GeneratePasswordHash(userDTO.NewPassword, newSalt);

            userDb.Salt = newSalt;
            userDb.PasswordHash = newPassHash;

            await context.SaveChangesAsync();
        }
    }
}
