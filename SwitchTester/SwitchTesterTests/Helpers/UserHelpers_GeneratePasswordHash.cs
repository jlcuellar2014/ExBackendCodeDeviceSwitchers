namespace Tests.Helpers
{
    [TestFixture]
    public class UserHelpers_GeneratePasswordHash
    {
        [Test]
        public void ValidPasswordAndSalt_ValidHashGenerated()
        {
            // Arrange
            string validPassword = "correctPassword";
            byte[] validSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA=="); 

            // Act
            byte[] hashResult = UserHelpers.GeneratePasswordHash(validPassword, validSalt);

            // Assert
            Assert.IsNotNull(hashResult);
            Assert.That(hashResult.Length, Is.EqualTo(32)); // Asegurar que el hash tiene la longitud correcta
        }

        [Test]
        public void EmptyPassword_ExceptionThrown()
        {
            // Arrange
            string emptyPassword = string.Empty;
            byte[] validSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => UserHelpers.GeneratePasswordHash(emptyPassword, validSalt));
        }

        [Test]
        public void NullSalt_ExceptionThrown()
        {
            // Arrange
            string validPassword = "correctPassword";
            byte[] nullSalt = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => UserHelpers.GeneratePasswordHash(validPassword, nullSalt));
            StringAssert.Contains("Value cannot be null", ex.Message);
        }
    }
}