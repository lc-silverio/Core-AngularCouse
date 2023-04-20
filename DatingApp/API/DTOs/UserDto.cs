namespace API.DTOs
{
    /// <summary>
    /// Defines the structure of the user dto object
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The login token
        /// </summary>
        public string Token { get; set; }
    }
}