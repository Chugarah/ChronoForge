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
            StartDate = createProjectDomain.StartDate,
            EndDate = createProjectDomain.EndDate,
        };

    public ProjectShowDto? ToDToProjectShow(Projects projects) =>
        new()
        {
            Id = projects.Id,
            Status = new StatusDisplayDto
            {
                Id = projects.Status!.Id,
                Name = projects.Status.Name,
            },
            Title = projects.Title,
        };
}
