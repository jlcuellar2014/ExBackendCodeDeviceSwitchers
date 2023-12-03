namespace SwitchTesterApi.DTOs
{
    /// <summary>
    /// Represents a response DTO for indicating an OK (HTTP 200) status.
    /// </summary>
    public class OkResponseDTO
    {
        /// <summary>
        /// Gets or sets the HTTP status code for a successful operation (default is 200).
        /// </summary>
        public virtual int StatusCode { get; set; } = StatusCodes.Status200OK;

        /// <summary>
        /// Gets or sets the status description for a successful operation (default is "Success").
        /// </summary>
        public virtual string Status { get; set; } = "Success";

        /// <summary>
        /// Gets or sets the message indicating that the operation has been completed successfully
        /// (default is "The operation has been completed successfully.").
        /// </summary>
        public virtual string Message { get; set; } = "The operation has been completed successfully.";
    }

    /// <summary>
    /// Represents a typed response DTO for indicating an OK (HTTP 200) status with additional data.
    /// </summary>
    /// <typeparam name="T">The type of additional data.</typeparam>
    public class OkResponseDTO<T> : OkResponseDTO
    {
        /// <summary>
        /// Gets or sets the additional data associated with the successful operation.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OkResponseDTO{T}"/> class.
        /// </summary>
        public OkResponseDTO() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OkResponseDTO{T}"/> class with the specified data.
        /// </summary>
        /// <param name="data">The additional data to include in the response.</param>
        public OkResponseDTO(T data) : this() => Data = data;
    }

}
