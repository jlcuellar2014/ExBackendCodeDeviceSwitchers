namespace Tests.Helpers
{
    [TestFixture]
    public class UserHelpers_CheckUserNameFormat
    {
        [Test]
        public void ValidUserName_NoExceptionThrown()
        {
            // Arrange
            string validUserName = "Valid_User123";

            // Act & Assert
            Assert.DoesNotThrow(() => UserHelpers.CheckUserNameFormat(validUserName));
        }

        // Caso de prueba: Nombre de usuario demasiado corto
        [Test]
        public void TooShort_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "Shor"; // Menos de 5 caracteres

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckUserNameFormat(invalidUserName));
            StringAssert.Contains("The username does not comply with established policies", ex.Message);
        }

        // Caso de prueba: Nombre de usuario demasiado largo
        [Test]
        public void TooLong_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "TooLongUserName123456789012345678901234567890"; // Más de 30 caracteres

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckUserNameFormat(invalidUserName));
            StringAssert.Contains("The username does not comply with established policies", ex.Message);
        }

        // Caso de prueba: Nombre de usuario con caracteres no permitidos
        [Test]
        public void InvalidCharacters_ExceptionThrown()
        {
            // Arrange
            string invalidUserName = "Invalid@User";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => UserHelpers.CheckUserNameFormat(invalidUserName));
            StringAssert.Contains("The username does not comply with established policies", ex.Message);
        }

    }
}
