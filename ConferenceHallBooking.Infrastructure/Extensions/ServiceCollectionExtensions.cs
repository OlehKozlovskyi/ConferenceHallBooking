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
            services.AddEntityFrameworkPgSql();

            return services;
        }

        private static IServiceCollection AddEntityFrameworkPgSql(this IServiceCollection services)
        {
            string databaseConnectionString = "Host = localhost; Database = hall-booking_db; Username = root; Password = 1111";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(databaseConnectionString));

            return services;
        }
    }
}
