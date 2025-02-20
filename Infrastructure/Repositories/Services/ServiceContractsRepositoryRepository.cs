using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Services;

public class ServiceContractsRepositoryRepository(
    DataContext dbContext,
    IEntityFactory<ServiceContracts?, ServiceContractsEntity> factory
) : BaseRepository<ServiceContracts, ServiceContractsEntity>(dbContext, factory), IServiceContractsRepository {
    // If we want to override the methods from the BaseRepository
    // using override keyword, remember that the method needs to be virtual in the BaseRepository
}