namespace SwitchTesterUnitTests.Helpers
{
    [TestFixture]
    public class UserHelpers_CheckUserNameFormatTests
    {
        [Test]
        public void ValidUserName_NoExceptionThrown()
        {
            // Arrange
            string validUserName = "Valid_User123";

            // Act & Assert
            Assert.DoesNotThrow(() 
                => UserHelpers.CheckUserNameFormat(validUserName));
        }

        [Test]
        public void TooShort_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "Shor";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() 
                => UserHelpers.CheckUserNameFormat(invalidUserName));

            Assert.That(ex.Message, 
                Does.Contain("The username does not comply with established policies"));
        }

        [Test]
        public void TooLong_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "TooLongUserName123456789012345678901234567890";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() 
                => UserHelpers.CheckUserNameFormat(invalidUserName));

            Assert.That(ex.Message, 
                Does.Contain("The username does not comply with established policies"));
        }

        [Test]
        public void InvalidCharacters_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "Invalid@User";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(()
                => UserHelpers.CheckUserNameFormat(invalidUserName));

            Assert.That(ex.Message,
                Does.Contain("The username does not comply with established policies"));
        }
    }
}
