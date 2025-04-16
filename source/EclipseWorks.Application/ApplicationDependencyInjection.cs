using EclipseWorks.Application.ApplicationServices;
using EclipseWorks.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EclipseWorks.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserAppService, UserAppService>();
    }

}
