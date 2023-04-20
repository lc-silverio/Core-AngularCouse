using System.Text;
using API.Controllers.Services;
using API.Data;
using API.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    /// <summary>
    /// Defines service extensions
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>Returns a Service Collection</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Adds Cors
            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();
            // Transient => Objects are always different; A new instance is provided to every controller and every service.
            // Scoped => Objects are the same within a request, but different across different requests.
            // Singleton => Objects are the same for every object and every request. (App lifetime)

            // Adds the database context
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}