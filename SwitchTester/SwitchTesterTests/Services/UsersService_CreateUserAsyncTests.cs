using Microsoft.EntityFrameworkCore;
using Moq;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Services;
using SwitchTesterTests.Models;

namespace SwitchTesterUnitTests.Services
{
    [TestFixture]
    public class UsersService_CreateUserAsyncTests
    {
        [Test]
        public async Task ValidUser_CreatesUserInDatabase()
        {
            // Arrange
            var userDTO = new UserCreateDTO
            {
                UserName = "ValidUser",
                Password = "StrongP@ssword123"
            };

            var mockContext = new Mock<ISecurityContext>();
            var mockApplicationUsers = new Mock<DbSet<User>>();

            mockContext.Setup(x => x.ApplicationUsers).Returns(mockApplicationUsers.Object);

            var userService = new UsersService(mockContext.Object);

            // Act
            await userService.CreateUserAsync(userDTO);

            // Assert
            mockContext.Verify(x => x.ApplicationUsers.Add(It.IsAny<User>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void InvalidUserName_ThrowsArgumentException()
        {
            // Arrange
            var userDTO = new UserCreateDTO
            {
                UserName = "Invalid@User",
                Password = "StrongP@ssword123"
            };

            var mockContext = new Mock<ISecurityContext>();
            var userService = new UsersService(mockContext.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () 
                => await userService.CreateUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The username does not comply with established policies"));
        }

        [Test]
        public async Task UserNameUsed_ThrowsArgumentExceptionAsync()
        {
            // Arrange
            var userNameUsed = "UserNameUsed";

            var userDTO = new UserCreateDTO
            {
                UserName = userNameUsed,
                Password = "StrongP@ssword123"
            };

            using var fakeContext = new FakeSecurityContext();
            var userService = new UsersService(fakeContext);

            fakeContext.ApplicationUsers.Add(new() { UserName = userNameUsed, PasswordHash = new byte[32], Salt = new byte[16] });
            await fakeContext.SaveChangesAsync();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async ()
                => await userService.CreateUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The username is already in use"));
        }

        [Test]
        public void InvalidPassword_ThrowsArgumentException()
        {
            // Arrange
            var userDTO = new UserCreateDTO
            {
                UserName = "ValidUser",
                Password = "WeakPassword"
            };

            var mockContext = new Mock<ISecurityContext>();
            var userService = new UsersService(mockContext.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () 
                => await userService.CreateUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The password does not comply with established policies"));
        }
    }
}
