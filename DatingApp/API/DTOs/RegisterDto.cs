using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// Defines the structure of the Register DTO object
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// The username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        [Required]
        [StringLength(maximumLength: 8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}