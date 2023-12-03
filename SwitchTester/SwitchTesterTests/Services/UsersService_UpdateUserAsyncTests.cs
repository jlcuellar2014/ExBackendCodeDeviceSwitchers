using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Services;
using SwitchTesterTests.Models;

namespace SwitchTesterUnitTests.Services
{
    [TestFixture]
    public class UsersService_UpdateUserAsyncTests
    {
        [Test]
        public async Task ValidUser_UpdateUserInDatabase()
        {
            // Arrange
            using var context = new FakeSecurityContext();

            var userDTO = new UserUpdateDTO
            {
                UserName = "ExistingUser",
                Password = "correctPassword",
                NewPassword = "NewP@ssword456"
            };

            var userDb = new User
            {
                UserName = userDTO.UserName,
                Salt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA=="),
                PasswordHash = Convert.FromBase64String("RBxnZ8dlZw9Axj/5GigoYji3QjD65tz42t9t/v9/H7M=")
            };

            var userService = new UsersService(context);

            context.ApplicationUsers.Add(userDb);
            await context.SaveChangesAsync();

            // Act & Assert
            Assert.DoesNotThrowAsync(async () 
                => await userService.UpdateUserAsync(userDTO));
        }

        [Test]
        public void NonExistingUser_ThrowsArgumentException()
        {
            // Arrange
            using var context = new FakeSecurityContext();

            var userDTO = new UserUpdateDTO
            {
                UserName = "NonExistingUser",
                Password = "OldP@ssword123",
                NewPassword = "NewP@ssword456"
            };

            var userService = new UsersService(context);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async ()
                    => await userService.UpdateUserAsync(userDTO));

            Assert.That(ex.Message,
                Does.Contain("The user with the assigned credentials does not exist."));
        }

        [Test]
        public async Task IncorrectOldPassword_ThrowsArgumentException()
        {
            // Arrange
            using var context = new FakeSecurityContext();

            var userDTO = new UserUpdateDTO
            {
                UserName = "ExistingUser",
                Password = "IncorrectOldPassword",
                NewPassword = "NewP@ssword456"
            };

            var userDb = new User
            {
                UserName = userDTO.UserName,
                Salt = UserHelpers.GenerateSalt(),
                PasswordHash = UserHelpers.GeneratePasswordHash("OldP@ssword123", new byte[16])
            };

            var userService = new UsersService(context);

            context.ApplicationUsers.Add(userDb);
            await context.SaveChangesAsync();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () 
                => await userService.UpdateUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The password assigned to perform the operation is incorrect."));
        }
    }
}
