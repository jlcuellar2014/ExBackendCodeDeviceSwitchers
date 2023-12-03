namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for updating user information.
    /// </summary>
    public class UserUpdateDTO
    {
        /// <summary>
        /// Gets or sets the username of the user to be updated.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the current password of the user for verification.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the new password to be set for the user.
        /// </summary>
        public required string NewPassword { get; set; }
    }
}
