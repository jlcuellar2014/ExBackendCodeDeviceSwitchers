namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents a response DTO for indicating a bad request (HTTP 400) status.
    /// </summary>
    public class BadRequestResponseDTO : OkResponseDTO
    {
        /// <inheritdoc/>
        public override int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        /// <inheritdoc/>
        public override string Status { get; set; } = "Fails";
        /// <inheritdoc/>
        public override string Message { get; set; } 
            = "An error occurred while processing the request. Please contact technical support.";
    }
}
