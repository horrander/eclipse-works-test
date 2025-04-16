using EclipseWorks.DbAdapter.Repositories;
using EclipseWorks.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseWorks.DbAdapter;

public static class DbAdapterDependencyInjection
{
    public static void AddDbAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            }
        );

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
