using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Defines the API routes for User related methods
    /// </summary>
    [AllowAnonymous]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController{T}"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UsersController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>Returns all the users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Gets the specified user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the user that matches the provided id</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}