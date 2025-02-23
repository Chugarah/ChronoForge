using Core.DTOs.Project;
using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.Project;

public class ProjectFactory : EntityFactoryBase<Projects, ProjectsEntity>
{
    /// <summary>
    /// Creating from Domain object to an Entity object
    /// a domain object
    /// Entity -> Domain
    /// </summary>
    /// <param name="projectsEntity"></param>
    /// <returns></returns>
    public override Projects ToDomain(ProjectsEntity projectsEntity) =>
        new()
        {
            Id = projectsEntity.Id,
            StatusId = projectsEntity.StatusId,
            ProjectManager = projectsEntity.ProjectManager,
            Title = projectsEntity.Title,
            Description = projectsEntity.Description,
            StartDate = projectsEntity.StartDate,
            EndDate = projectsEntity.EndDate,
            ServiceContracts =
                projectsEntity
                    .ServiceContractsEntity?.Select(sc => new ServiceContracts
                    {
                        Id = sc.Id,
                        CustomerId = sc.CustomerId,
                        PaymentTypeId = sc.PaymentTypeId,
                        Name = sc.Name,
                        Price = sc.Price,
                    })
                    .ToList() ?? [],
        };

    /// <summary>
    /// Creating from Domain object to Entity object
    /// Domain -> Entity
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public override ProjectsEntity ToEntity(Projects domain)
    {
        var entity = new ProjectsEntity
        {
            Id = domain.Id,
            Title = domain.Title,
            ProjectManager = domain.ProjectManager,
            StartDate = domain.StartDate,
            EndDate = domain.EndDate,
            StatusId = domain.StatusId,
            Description = domain.Description,
            // Map relationships through IDs only
            ServiceContractsEntity = domain.ServiceContracts?
                .Where(sc => sc.Id > 0)
                .Select(sc => new ServiceContractsEntity { Id = sc.Id })
                .ToList() ?? new List<ServiceContractsEntity>()
        };

        return entity;
    }
}
