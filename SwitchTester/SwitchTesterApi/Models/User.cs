namespace SwitchTesterApi.Models
{
    /// <summary>
    /// Represents a user in the security system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique username of the user.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public required byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt used in password hashing for the user.
        /// </summary>
        public required byte[] Salt { get; set; }
    }
}
