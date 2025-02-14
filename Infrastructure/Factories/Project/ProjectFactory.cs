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
            Status = new Status
            {
                Id = projectsEntity.StatusEntity.Id,
                Name = projectsEntity.StatusEntity.Name
            },
            ProjectManager = projectsEntity.ProjectManager,
            Title = projectsEntity.Title,
            StartDate = projectsEntity.StartDate,
            EndDate = projectsEntity.EndDate,
        };

    /// <summary>
    /// Creating from Domain object to Entity object
    /// Domain -> Entity
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public override ProjectsEntity ToEntity(Projects projects) =>
        new()
        {
            Id = projects.Id,
            StatusId = projects.StatusId,
            ProjectManager = projects.ProjectManager,
            Title = projects.Title,
            StartDate = projects.StartDate,
            EndDate = projects.EndDate,
        };
}

