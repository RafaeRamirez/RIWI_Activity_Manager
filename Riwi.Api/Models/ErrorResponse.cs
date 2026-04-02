namespace Riwi.Api.Models
{
    /// <summary>
    /// Standardized error response model for API errors
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Detailed error information (optional)
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Timestamp of the error
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
