using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace TiendaApi.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("IdentityConnection"));
            });

            return services;
        }
    }
}
