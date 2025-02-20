using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Projects;

/// <summary>
/// Status Repository
/// </summary>
/// <param name="dbContext"></param>
/// <param name="factory"></param>
public class StatusRepository(DataContext dbContext, IEntityFactory<Status?, StatusEntity> factory)
    : BaseRepository<Status, StatusEntity>(dbContext, factory),
        IStatusRepository
{
    // If we want to override the methods from the BaseRepository
    // using override keyword, remember that the method needs to be virtual in the BaseRepository
}


