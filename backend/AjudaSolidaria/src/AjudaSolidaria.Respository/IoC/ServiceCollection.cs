using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AjudaSolidaria.Respository.IoC
{
    public static class ServiceCollection
    {
        public static void AddServicesRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AjudaSolidariaContext>(
                p =>p.UseNpgsql(configuration.GetConnectionString("Producao"), 
                c => c.EnableRetryOnFailure().MigrationsAssembly(typeof(AjudaSolidariaContext).Assembly.FullName)));
        }
    }
}
