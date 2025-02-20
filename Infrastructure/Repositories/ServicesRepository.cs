using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ServicesRepository(
    DataContext dbContext,
    IEntityFactory<Services?, ServicesEntity> factory
) : BaseRepository<Services, ServicesEntity>(dbContext, factory), IServicesRepository {
    // If we want to override the methods from the BaseRepository
    // using override keyword, remember that the method needs to be virtual in the BaseRepository
}