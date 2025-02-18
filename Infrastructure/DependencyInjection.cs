using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories.Project;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// This is Composition Root pattern
/// https://medium.com/@cfryerdev/dependency-injection-composition-root-418a1bb19130
/// API -> Core
/// Infrastructure -> Core
/// API -> Infrastructure (only for DI registration)
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add services to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString
    )
    {
        // We are taking the connection string from the API layer.
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
            )
        );

        // Registering the repositories in DI container
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Registering the factories in DI container
        services.AddScoped<IEntityFactory<Status, StatusEntity>, StatusFactory>();
        services.AddScoped<IEntityFactory<Projects, ProjectsEntity>, ProjectFactory>();
        services.AddScoped<IEntityFactory<Users, UsersEntity>, UserFactory>();
        // Got help from Phind AI to add this line regarding the UnitOfWork
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(
            provider.GetRequiredService<DataContext>()
        ));
        return services;
    }
}
