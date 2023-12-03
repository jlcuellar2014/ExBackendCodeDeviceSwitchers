namespace SwitchTesterUnitTests.Helpers
{
    [TestFixture]
    public class UserHelpers_CheckIfPasswordCorrectTests
    {
        [Test]
        public void CorrectPassword_NoExceptionThrown()
        {
            // Arrange
            string enteredPassword = "correctPassword";

            byte[] storedSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");
            byte[] storedPasswordHash = Convert.FromBase64String("RBxnZ8dlZw9Axj/5GigoYji3QjD65tz42t9t/v9/H7M=");

            // Act & Assert
            Assert.DoesNotThrow(() 
                => UserHelpers.CheckIfPasswordCorrect(enteredPassword, storedSalt, storedPasswordHash));
        }

        [Test]
        public void IncorrectPassword_ExceptionThrown()
        {
            // Arrange
            string enteredPassword = "incorrectPassword";

            byte[] storedSalt = Convert.FromBase64String("bZTRU/LwQvsMhQMksvhZJA==");
            byte[] storedPasswordHash = Convert.FromBase64String("RBxnZ8dlZw9Axj/5GigoYji3QjD65tz42t9t/v9/H7M=");

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() 
                => UserHelpers.CheckIfPasswordCorrect(enteredPassword, storedSalt, storedPasswordHash));

            Assert.That(ex.Message, 
                Does.Contain("The password assigned to perform the operation is incorrect."));
        }

        [Test]
        public void IncorrectSalt_ExceptionThrown()
        {
            // Arrange
            string enteredPassword = "correctPassword";

            byte[] storedSalt = Convert.FromBase64String("cZTRU/LwQvsMhQMksvhZJA==");
            byte[] storedPasswordHash = Convert.FromBase64String("RBxnZ8dlZw9Axj/5GigoYji3QjD65tz42t9t/v9/H7M=");

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() 
                => UserHelpers.CheckIfPasswordCorrect(enteredPassword, storedSalt, storedPasswordHash));

            Assert.That(ex.Message, 
                Does.Contain("The password assigned to perform the operation is incorrect."));
        }
    }
}