using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.Project;

/// <summary>
/// Status Factory
/// </summary>
public class StatusFactory : EntityFactoryBase<Status, StatusEntity>
{

    /// <summary>
    /// Create a StatusEntity from a Status Entity to
    /// a domain object
    /// Entity -> Domain
    /// </summary>
    /// <param name="statusEntity"></param>
    /// <returns></returns>
    public override Status ToDomain(StatusEntity statusEntity) => new()
    {
        Id = statusEntity.Id,
        Name = statusEntity.Name
    };

    /// <summary>
    /// Creating from Domain object to Entity object
    /// Domain -> Entity
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public override StatusEntity ToEntity(Status status) => new()
    {
        Id = status.Id,
        Name = status.Name
    };
}