using System.Collections;
using System.Data.Common;
using Core.DTOs.Project;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Domain;
using Domain.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IProjectDtoFactory projectDtoFactory,
    IUnitOfWork unitOfWork
) : IProjectService
{
    public async Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertFormDto projectInsertFormDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var projectInsert = projectDtoFactory.ToDomainProjectInsert(projectInsertFormDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the project in the database
            await projectRepository.CreateAsync(projectInsert);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Get the tracked entity with generated ID
            var createdProject = await projectRepository.GetAsync(
                p => projectInsert != null && p!.Id == projectInsert.Id,
                true
            );

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return show DTO with full details
            return projectDtoFactory.ToDtoProjectShow(createdProject);
        }
        catch (Exception)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    ///  Get a project by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ProjectShowDto?> GetProjectByIdAsync(int id)
    {
        try
        {
            // Get the project from the database
            var project = await projectRepository.GetAsync(p => p != null && p.Id == id, false);

            // Convert the project to a display DTO
            return project != null ? projectDtoFactory.ToDtoProjectShow(project) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the Project from the database:", ex);
        }
    }

    /// <summary>
    /// Get a project by status
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ProjectShowDto?> GetProjectByStatusAsync(int id)
    {
        try
        {
            // Get the project from the database
            var project = await projectRepository.GetAsync(
                p => p != null && p.StatusId == id,
                false
            );

            // Convert the project to a display DTO
            return project != null ? projectDtoFactory.ToDtoProjectShow(project) : null;
        }
        catch (DbException)
        {
            // Throw an exception with a message
            return null;
        }
    }

    public async Task<IEnumerable<ProjectShowDto>> GetAllProjectsAsync()
    {
        try
        {
            // Get all the projects from the database
            var projects = await projectRepository.GetAllAsync(p => p != null);

            // Convert the projects to a display DTO
            return projectDtoFactory.ToDtoProjectShow(projects);
        }
        catch (DbException)
        {
            // Throw an exception with a message
            return Enumerable.Empty<ProjectShowDto>();
        }
    }

    /// <summary>
    /// Update a project in the database
    /// </summary>
    /// <param name="projectUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ProjectUpdateDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
    {
        try
        {
            // Get the project from the database
            var project =
                await projectRepository.GetAsync(p => p!.Id == projectUpdateDto.Id, true)
                ?? throw new Exception("Could not find the project in the database");

            // Update the project with the new values
            project.StatusId = projectUpdateDto.StatusId;
            project.ProjectManager = projectUpdateDto.ProjectManager;
            project.Title = projectUpdateDto.Title;
            project.Description = projectUpdateDto.Description;
            project.StartDate = projectUpdateDto.StartDate;
            project.EndDate = projectUpdateDto.EndDate;

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Update the status in the database
            await projectRepository.UpdateAsync(project);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the updated project as a display DTO
            return projectUpdateDto;
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Throw an exception with a message
            throw new Exception("Could not update the project in the database:", ex);
        }
    }

    /// <summary>
    /// Update the project status
    /// </summary>
    /// <param name="oldStatus"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProjectShowDto>> UpdateProjectStatusAsync(int oldStatus)
    {
        try
        {
            // Get the project from the database
            var getProjects = await projectRepository.GetAllAsync(p =>
                p != null && p.StatusId == oldStatus
            );
            // Convert our Projects to a list
            var projectsEnumerable = getProjects.ToList();

            // Check if the project exists using LINQ, if not return an empty list
            if (!projectsEnumerable.Any())
            {
                // We are using Enumerable.Empty<T> to return an empty list instead
                // IEnumerable<T> because we are an empty returning a list of ProjectShowDto
                return Enumerable.Empty<ProjectShowDto>();
            }

            #region BEGIN TRANSACTION
            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Used Rider to refactor the code to LINQ syntax
            // Iterate through the projects and update the status
            // This updates all the projects with the old status to the default status
            // TODO: Look into Update status on view or background Process
            foreach (var projects in projectsEnumerable.OfType<Projects>())
            {
                // Update the status of the project to the default status
                projects.StatusId = StatusConstants.DefaultStatusId;
                await projectRepository.UpdateAsync(projects);
            }

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();
            #endregion END TRANSACTION

            // Get the updated projects from the database using LINQ
            // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.contains?view=net-9.0
            var updatedProjects = await projectRepository.GetAllAsync(p =>
                p != null && projectsEnumerable.Select(pr => pr!.Id).Contains(p.Id)
            );

            // Return the updated projects as a display DTO
            return projectDtoFactory.ToDtoProjectShow(updatedProjects);
        }
        catch (DbException)
        {
            // Rollback the transaction if error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ProjectDeleteShowDto> DeleteProjectAsync(int id)
    {
        try
        {
            // Get the status from the database
            var projects =
                await projectRepository.GetAsync(s => s!.Id == id, true)
                ?? throw new KeyNotFoundException($"Project with ID {id} not found");

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Delete the status in the database
            await projectRepository.DeleteAsync(projects);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the deleted status as a display DTO
            return projectDtoFactory.ToDtoDeleteShow(projects)!;
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Throw an exception with a message
            throw new Exception("Could not delete the status in the database:", ex);
        }
    }
}
