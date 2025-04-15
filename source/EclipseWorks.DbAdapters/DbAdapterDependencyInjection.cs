using EclipseWorks.DbAdapter.Repositories;
using EclipseWorks.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseWorks.DbAdapter;

public static class DbAdapterDependencyInjection
{
    public static void AddDbAdapter(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseNpgsql("Obter connection string");
            }
        );

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
