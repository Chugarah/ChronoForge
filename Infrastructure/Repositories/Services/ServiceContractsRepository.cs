using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Services;

public class ServiceContractsRepository(
    DataContext dbContext,
    IEntityFactory<ServiceContracts, ServiceContractsEntity> factory
) : BaseRepository<ServiceContracts, ServiceContractsEntity>(dbContext, factory), IServiceContractsRepository {
    // Explicit implementation to handle non-null returns
    /// <summary>
    /// Attaching a service contract to the database
    /// </summary>
    /// <param name="serviceContract"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public new async Task<ServiceContracts?> AttachAsync(ServiceContracts? serviceContract)
    {
        // Attach the service contract to the database
        var attached = await base.AttachAsync(serviceContract);
        // If the service contract is not attached, throw an exception
        return attached ?? throw new InvalidOperationException("Attach operation failed");
    }
}