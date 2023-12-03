namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for user login credentials.
    /// </summary>
    public class UserLoginDTO
    {
        /// <summary>
        /// Gets or sets the username for user login.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for user login.
        /// </summary>
        public required string Password { get; set; }
    }
}
