using Microsoft.Extensions.Options;
using Moq;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Services;
using SwitchTesterApi.Settings;
using SwitchTesterTests.Models;

namespace SwitchTesterUnitTests.Services
{
    [TestFixture]
    public class SecurityService_LoginUserAsyncTests
    {
        [Test]
        public async Task ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userDTO = new UserLoginDTO { UserName = "UserName", Password = "Password" };
            var userSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");
            var userPasswordHash = UserHelpers.GeneratePasswordHash(userDTO.Password, userSalt);

            var mockJwt = new Mock<IOptions<JwtConfiguration>>();
            mockJwt.Setup(o => o.Value.Issuer).Returns("Issuer");
            mockJwt.Setup(o => o.Value.Audience).Returns("Audience");
            mockJwt.Setup(o => o.Value.SecretKey).Returns("YviRyMQdf9YsC1r9KpcbAQTOJiiOKzrHl6rQtiqAZ7U=");
            mockJwt.Setup(o => o.Value.HoursLife).Returns(1);

            using var fakeContext = new FakeSecurityContext();

            fakeContext.ApplicationUsers.Add(new User { 
                UserName = userDTO.UserName,
                PasswordHash = userPasswordHash,
                Salt = userSalt
            });

            await fakeContext.SaveChangesAsync();

            var securityService = new SecurityService(fakeContext, mockJwt.Object);
            
            // Act
            var result = await securityService.LoginUserAsync(userDTO);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task InvalidUser_ThrowsArgumentExceptionAsync()
        {
            // Arrange
            var userDTO = new UserLoginDTO { UserName = "UserName", Password = string.Empty };
            var userSalt = new byte[16];
            var userPasswordHash = new byte[32];

            var mockJwt = new Mock<IOptions<JwtConfiguration>>();

            using var fakeContext = new FakeSecurityContext();

            fakeContext.ApplicationUsers.Add(new User
            {
                UserName = "WrongUserName",
                PasswordHash = userPasswordHash,
                Salt = userSalt
            });

            await fakeContext.SaveChangesAsync();

            var securityService = new SecurityService(fakeContext, mockJwt.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () 
                => await securityService.LoginUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The user does not exist in the system."));
        }

        [Test]
        public async Task IncorrectPassword_ThrowsArgumentExceptionAsync()
        {
            // Arrange
            var userDTO = new UserLoginDTO { UserName = "UserName", Password = "WrongPassword" };
            var userSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");
            var userPasswordHash = UserHelpers.GeneratePasswordHash("Password", userSalt);

            var mockJwt = new Mock<IOptions<JwtConfiguration>>();

            using var fakeContext = new FakeSecurityContext();

            fakeContext.ApplicationUsers.Add(new User
            {
                UserName = "UserName",
                PasswordHash = userPasswordHash,
                Salt = userSalt
            });

            await fakeContext.SaveChangesAsync();

            var securityService = new SecurityService(fakeContext, mockJwt.Object);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () 
                => await securityService.LoginUserAsync(userDTO));

            Assert.That(ex.Message, Does.Contain("The password assigned to perform the operation is incorrect."));
        }
    }
}
