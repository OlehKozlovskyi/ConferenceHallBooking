using ConferenceHallBooking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceHallBooking.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = "Host = localhost; Database = hall_booking; Username = root; Password = 1111";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(databaseConnectionString)));
            // Add infrastructure services here
            return services;
        }
    }
}
