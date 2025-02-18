using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.Project;
/// <summary>
/// User Factory
/// </summary>
public class UserFactory : EntityFactoryBase<Users, UsersEntity>
{
    /// <summary>
    /// Create a UserEntity from a User Entity to
    /// a domain object
    /// Entity -> Domain
    /// </summary>
    /// <param name="usersEntity"></param>
    /// <returns></returns>
    public override Users ToDomain(UsersEntity usersEntity) => new()
    {
        Id = usersEntity.Id,
        FirstName = usersEntity.FirstName,
        LastName = usersEntity.LastName,
    };

    /// <summary>
    /// Creating from Domain object to Entity object
    /// Domain -> Entity
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    public override UsersEntity ToEntity(Users users) => new()
    {
        Id = users.Id,
        FirstName = users.FirstName,
        LastName = users.LastName,
    };
}