using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Defines routes and logic for the Account Controller
    /// </summary>
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// The data context
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Defines the constructor for the Account Controller
        /// </summary>
        /// <param name="context"></param>
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user in the database 
        /// </summary>
        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registration)
        {
            if (await UserExists(registration.Username))
            {
                return BadRequest("Error: User already exists");
            }

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registration.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registration.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user) };
        }

        /// <summary>
        /// Validates the user credentials and logs in the user
        /// </summary>
        /// <param name="login"></param>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password) || !UserExists(login.Username).Result)
            {
                return Unauthorized("Invalid username");
            };

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.Equals(login.Username.ToLower()));
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user) };
        }

        /// <summary>
        /// Checks wether the username already exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns a boolean result</returns>
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
        }
    }
}