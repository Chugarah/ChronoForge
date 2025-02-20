using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories.Project;
using Infrastructure.Factories.ProjectServices;
using Infrastructure.Repositories.Projects;
using Infrastructure.Repositories.Services;
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
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IServiceContractsRepository, ServiceContractsRepositoryRepository>();

        // Registering the factories in DI container
        services.AddScoped<IEntityFactory<Status, StatusEntity>, StatusFactory>();
        services.AddScoped<IEntityFactory<Projects, ProjectsEntity>, ProjectFactory>();
        services.AddScoped<IEntityFactory<Users, UsersEntity>, UserFactory>();
        services.AddScoped<IEntityFactory<Customers, CustomersEntity>, CustomerFactory>();
        services.AddScoped<IEntityFactory<ServiceContracts, ServiceContractsEntity>, ServiceContractsFactory>();

        // Got help from Phind AI to add this line regarding the UnitOfWork
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(
            provider.GetRequiredService<DataContext>()
        ));
        return services;
    }
}
