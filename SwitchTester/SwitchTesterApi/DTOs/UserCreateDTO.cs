namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for creating a new user.
    /// </summary>
    public class UserCreateDTO
    {
        /// <summary>
        /// Gets or sets the desired username for the new user.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the desired password for the new user.
        /// </summary>
        public required string Password { get; set; }
    }
}
