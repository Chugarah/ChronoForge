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
    public override Projects ToDomain(ProjectsEntity projectsEntity)
    {
        // Inspired by Mikael :) This is for easier debugging
        var projects = new Projects
        {
            Id = projectsEntity.Id,
            StatusId = projectsEntity.StatusId,
            ProjectManager = projectsEntity.ProjectManager,
            Title = projectsEntity.Title,
            Description = projectsEntity.Description,
            StartDate = projectsEntity.StartDate,
            EndDate = projectsEntity.EndDate,
        };
        return projects!;
    }

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
            Description = projects.Description,
            StartDate = projects.StartDate,
            EndDate = projects.EndDate,
        };
}
