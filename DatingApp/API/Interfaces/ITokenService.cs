using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Defines a set of Token related methods
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns a string token</returns>
        string CreateToken(AppUser user);
    }
}