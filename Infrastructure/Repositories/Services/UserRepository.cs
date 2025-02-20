using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Services;

public class UserRepository(DataContext dbContext, IEntityFactory<Users?, UsersEntity> factory)
    : BaseRepository<Users, UsersEntity>(dbContext, factory),
        IUserRepository
{
    // If we want to override the methods from the BaseRepository
    // using override keyword, remember that the method needs to be virtual in the BaseRepository
}
