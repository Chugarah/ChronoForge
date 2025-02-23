using System.Collections;
using Core.DTOs.Project;
using Core.DTOs.Project.Status;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class ProjectDtoFactory : IProjectDtoFactory
{
    /// <summary>
    /// This method is used to convert the ProjectInsertDto to Projects
    /// </summary>
    /// <param name="formDto"></param>
    /// <returns></returns>
    public Projects ToDomainProjectInsert(ProjectInsertFormDto formDto)
    {
        return new Projects
        {
            Title = formDto.Title,
            ProjectManager = formDto.ProjectManager,
            StartDate = formDto.StartDate,
            EndDate = formDto.EndDate,
            StatusId = formDto.StatusId,
            Description = formDto.Description,
            // Safe collection initialization
            ServiceContracts = new List<ServiceContracts?>(),
        };
    }

    /// <summary>
    /// This method is used to convert the ProjectUpdateDto to Projects
    /// This updates the project
    /// </summary>
    /// <param name="updateProjectDomain"></param>
    /// <returns></returns>
    public Projects ToDomainProjectUpdate(ProjectUpdateDto updateProjectDomain)
    {
        return new Projects
        {
            Id = updateProjectDomain.Id,
            StatusId = updateProjectDomain.StatusId,
            ProjectManager = updateProjectDomain.ProjectManager,
            Title = updateProjectDomain.Title,
            Description = updateProjectDomain.Description,
            StartDate = updateProjectDomain.StartDate,
            EndDate = updateProjectDomain.EndDate,
        };
    }

    /// <summary>
    /// This method is used to convert the ProjectShowDto to Projects
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public ProjectShowDto ToDomainProjectShow(Projects project)
    {
        return new ProjectShowDto
        {
            Id = project.Id,
            ServiceContracts = project.ServiceContracts.Select(sc => new ServiceContractsShowDto
            {
                Id = sc!.Id,
                CustomerId = sc.CustomerId,
                PaymentTypeId = sc.PaymentTypeId,
                Name = sc.Name,
                Price = sc.Price,
            }),
            Title = project.Title,
            StatusId = project.StatusId,
            ProjectManager = project.ProjectManager,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
        };
    }

    /// <summary>
    /// This method is used to convert the ProjectUpdateDto to Projects
    /// Different from the above method, this one returns a collection of Projects
    /// </summary>
    /// <param name="updateProjectDomain"></param>
    /// <returns></returns>
    public IEnumerable<Projects> ToDomainProjectUpdateList(
        IEnumerable<ProjectUpdateDto> updateProjectDomain
    )
    {
        // Using LINQ to convert the ProjectUpdateDto to Projects
        return updateProjectDomain.Select(p => new Projects
        {
            Id = p.Id,
            StatusId = p.StatusId,
            ProjectManager = p.ProjectManager,
            Title = p.Title,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
        });
    }

    /// <summary>
    /// This method is used to convert the Projects to ProjectShowDto
    /// and returns a single ProjectShowDto
    /// </summary>
    /// <param name="projects"></param>
    /// <returns></returns>
    public ProjectShowDto? ToDtoProjectShow(Projects? projects) =>
        new()
        {
            Id = projects!.Id,
            Title = projects!.Title,
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
