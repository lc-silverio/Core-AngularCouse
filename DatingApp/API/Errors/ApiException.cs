namespace API.Errors
{
    public class ApiException
    {
        /// <summary>
        /// The status code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// The exception message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The exception details
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code</param>
        /// <param name="message">The exception message</param>
        /// <param name="details">The exception details</param>
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

    }
}