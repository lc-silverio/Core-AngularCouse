using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.Services
{
    /// <summary>
    /// Defines methods related with Token management
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <summary>
        /// The token key
        /// </summary>
        private readonly SymmetricSecurityKey _key;

        /// <summary>
        ///  Initializes a new instance of the <see cref="TokenService"/> class
        /// </summary>
        /// <param name="config"></param>
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns a string token</returns>
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}