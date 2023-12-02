namespace Tests.Helpers
{
    [TestFixture]
    public class UserHelpers_CheckPasswordFormatTests
    {
        [Test]
        public void ValidPassword_NoExceptionThrown()
        {
            // Arrange
            string validPassword = "StrongP@ssword123";

            // Act & Assert
            Assert.DoesNotThrow(() => UserHelpers.CheckPasswordFormat(validPassword));
        }

        [Test]
        public void NoLowercaseLetter_ExceptionThrown()
        {
            // Arrange
            string invalidPassword = "UPPERCASE123!";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckPasswordFormat(invalidPassword));
            StringAssert.Contains("The password does not comply with established policies", ex.Message);
        }

        [Test]
        public void NoUppercaseLetter_ExceptionThrown()
        {
            // Arrange
            string invalidPassword = "lowercase123!";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckPasswordFormat(invalidPassword));
            StringAssert.Contains("The password does not comply with established policies", ex.Message);
        }

        [Test]
        public void NoNumber_ExceptionThrown()
        {
            // Arrange
            string invalidPassword = "NoNumber@UpperCase";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckPasswordFormat(invalidPassword));
            StringAssert.Contains("The password does not comply with established policies", ex.Message);
        }

        [Test]
        public void NoSpecialCharacter_ExceptionThrown()
        {
            // Arrange
            string invalidPassword = "SpecialCharacter123";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckPasswordFormat(invalidPassword));
            StringAssert.Contains("The password does not comply with established policies", ex.Message);
        }

        [Test]
        public void TooShort_ExceptionThrown()
        {
            // Arrange
            string invalidPassword = "ShrP@1";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckPasswordFormat(invalidPassword));
            StringAssert.Contains("The password does not comply with established policies", ex.Message);
        }
    }
}
