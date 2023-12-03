namespace SwitchTesterUnitTests.Helpers
{
    [TestFixture]
    public class UserHelpers_GenerateSaltTests
    {
        [Test]
        public void ValidSaltGenerated()
        {
            // Act
            var salt = UserHelpers.GenerateSalt();

            // Assert
            Assert.That(salt, Is.Not.Null);
            Assert.That(salt, Has.Length.EqualTo(16)); // Ensure the salt is the correct length
        }

        [Test]
        public void MultipleSalts_AreNotEqual()
        {
            // Act
            var salt1 = UserHelpers.GenerateSalt();
            var salt2 = UserHelpers.GenerateSalt();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(salt1, Is.Not.Null);
                Assert.That(salt2, Is.Not.Null);
                Assert.That(salt1, Has.Length.EqualTo(16)); // Ensure the salt is the correct length
                Assert.That(salt2, Has.Length.EqualTo(16)); // Ensure the salt is the correct length
                Assert.That(salt2, Is.Not.EqualTo(salt1));  // Ensure that the generated salts are not equal
            });
            
        }
    }
}