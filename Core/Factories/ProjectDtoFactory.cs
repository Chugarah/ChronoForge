using Core.DTOs.Project;
using Core.DTOs.Project.Status;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class ProjectDtoFactory : IProjectDtoFactory
{
    public Projects? ToDomainProjectInsert(ProjectInsertDto createProjectDomain) =>
        new()
        {
            StatusId = createProjectDomain.StatusId,
            ProjectManager = createProjectDomain.ProjectManager,
            Title = createProjectDomain.Title,
            Description = createProjectDomain.Description,
            StartDate = createProjectDomain.StartDate,
            EndDate = createProjectDomain.EndDate,
        };

    public ProjectShowDto? ToDToProjectShow(Projects projects) =>
        new()
        {
            Id = projects.Id,
            Title = projects.Title,
            StartDate = projects.StartDate,
            EndDate = projects.EndDate,
        };
}
