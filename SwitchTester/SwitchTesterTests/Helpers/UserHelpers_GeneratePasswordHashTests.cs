namespace SwitchTesterUnitTests.Helpers
{
    [TestFixture]
    public class UserHelpers_GeneratePasswordHashTests
    {
        [Test]
        public void ValidPasswordAndSalt_ValidHashGenerated()
        {
            // Arrange
            var validPassword = "correctPassword";
            var validSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA=="); 

            // Act
            var passwordHash = UserHelpers.GeneratePasswordHash(validPassword, validSalt);

            // Assert
            Assert.That(passwordHash, Is.Not.Null);
            Assert.That(passwordHash, Has.Length.EqualTo(32));
        }

        [Test]
        public void EmptyPassword_ExceptionThrown()
        {
            // Arrange
            var emptyPassword = string.Empty;
            var validSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");

            // Act & Assert
            Assert.Throws<ArgumentException>(() 
                => UserHelpers.GeneratePasswordHash(emptyPassword, validSalt));
        }

        [Test]
        public void NullSalt_ExceptionThrown()
        {
            // Arrange
            var validPassword = "correctPassword";
            byte[]? nullSalt = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() 
                => UserHelpers.GeneratePasswordHash(validPassword, nullSalt));

            Assert.That(ex.Message, 
                Does.Contain("Value cannot be null"));
        }
    }
}