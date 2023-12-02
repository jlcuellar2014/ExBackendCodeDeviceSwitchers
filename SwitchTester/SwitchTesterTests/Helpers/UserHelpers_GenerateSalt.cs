namespace Tests.Helpers
{
    [TestFixture]
    public class UserHelpers_GenerateSalt
    {
        [Test]
        public void ValidSaltGenerated()
        {
            // Act
            byte[] salt = UserHelpers.GenerateSalt();

            // Assert
            Assert.IsNotNull(salt);
            Assert.That(salt.Length, Is.EqualTo(16)); // Ensure the salt is the correct length
        }

        [Test]
        public void MultipleSalts_AreNotEqual()
        {
            // Act
            byte[] salt1 = UserHelpers.GenerateSalt();
            byte[] salt2 = UserHelpers.GenerateSalt();

            // Assert
            Assert.IsNotNull(salt1);
            Assert.IsNotNull(salt2);
            Assert.That(salt1.Length, Is.EqualTo(16)); // Ensure the salt is the correct length
            Assert.That(salt2.Length, Is.EqualTo(16)); // Ensure the salt is the correct length
            Assert.That(salt2, Is.Not.EqualTo(salt1)); // Ensure that the generated salts are not equal
        }
    }
}