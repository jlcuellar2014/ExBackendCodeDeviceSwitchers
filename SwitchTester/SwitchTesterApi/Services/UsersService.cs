using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterApi.Services
{
    public class UsersService(ISecurityContext context) : IUsersService
    {
        public async Task CreateUserAsync(UserCreateDTO userDTO)
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

        public async Task UpdateUserAsync(UserUpdateDTO userDTO)
        {
            UserHelpers.CheckPasswordFormat(userDTO.NewPassword);

            var userDb = await context.ApplicationUsers
                                      .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName))
                                        ?? throw new ArgumentException("The user with the assigned credentials does not exist.", nameof(userDTO));

            UserHelpers.CheckIfPasswordCorrect(userDTO.OldPassword, userDb.Salt, userDb.PasswordHash);

            var newSalt = UserHelpers.GenerateSalt();
            var newPassHash = UserHelpers.GeneratePasswordHash(userDTO.NewPassword, newSalt);

            userDb.Salt = newSalt;
            userDb.PasswordHash = newPassHash;

            await context.SaveChangesAsync();
        }
    }
}
