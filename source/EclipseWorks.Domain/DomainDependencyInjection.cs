using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseWorks.Domain;

public static class DomainDependencyInjection
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}
