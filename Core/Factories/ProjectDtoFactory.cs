using System.Collections;
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


    /// <summary>
    /// This method is used to convert the Projects to ProjectShowDto
    /// and returns a single ProjectShowDto
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public ProjectShowDto? ToDtoProjectShow(Projects projects) =>
        new()
        {
            Id = projects.Id,
            Title = projects.Title,
            ProjectManager = projects.ProjectManager,
            StatusId = projects.StatusId,
            Description = projects.Description,
            StartDate = projects.StartDate,
            EndDate = projects.EndDate,
        };

    /// <summary>
    /// This multiple ProjectShowDto (collection)
    /// Need extra help from AI to refactor this method to use IEnumerable so we
    /// can use LINQ to convert the Projects to ProjectShowDto
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public IEnumerable<ProjectShowDto> ToDtoProjectShow(IEnumerable<Projects?> projects) =>
        // Using LINQ to convert the Projects to ProjectShowDto
        projects.Select(p => new ProjectShowDto
        {
            Id = p!.Id,
            Title = p.Title,
            StatusId = p.StatusId,
            ProjectManager = p.ProjectManager,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
        });

    /// <summary>
    /// This method is used to convert the Projects to ProjectDeleteShow
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public ProjectDeleteShowDto? ToDtoDeleteShow(Projects projects) =>
        new() { Id = projects.Id, Title = projects.Title };
}
