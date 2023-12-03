namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents data transfer object (DTO) for the response of a user login operation.
    /// </summary>
    public class UserLoginResponseDTO : OkResponseDTO
    {
        /// <summary>
        /// Gets or sets the authentication token associated with the user login.
        /// </summary>
        public required string Token { get; set; }
    }

}
