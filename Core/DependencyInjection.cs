using Core.Factories;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Core;

/// <summary>
/// This is Composition Root pattern
/// https://medium.com/@cfryerdev/dependency-injection-composition-root-418a1bb19130
/// API -> Core
/// Infrastructure -> Core
/// API -> Infrastructure (only for DI registration)
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // Register domain services
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IUserService, UserService>();
        // Register factories
        services.AddScoped<IStatusDtoFactory, StatusDtoFactory>();
        services.AddScoped<IProjectDtoFactory, ProjectDtoFactory>();
        services.AddScoped<IUserDtoFactory, UserDtoFactory>();
        return services;
    }
}