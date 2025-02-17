using System.Data.Common;
using Core.DTOs.Project;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IProjectDtoFactory projectDtoFactory,
    IUnitOfWork unitOfWork
) : IProjectService
{
    public async Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertDto projectInsertDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var projectInsert = projectDtoFactory.ToDomainProjectInsert(projectInsertDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the project in the database
            await projectRepository.CreateAsync(projectInsert);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Get the created status from the database, this is required to follow
            // restful API response 201 best practices
            var createdProject =
                await projectRepository.GetAsync(p =>
                    p!.Title == projectInsertDto!.Title
                    && p.ProjectManager == projectInsertDto.ProjectManager
                ) ?? null!;

            // Return the created project as a display DTO
            return projectDtoFactory.ToDToProjectShow(createdProject);
        }
        catch (Exception)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ProjectShowDto?> GetProjectById(int id)
    {
        try
        {
            // Get the project from the database
            var project = await projectRepository.GetAsync(
                p => p != null && p.Id == id
            );

            // Convert the project to a display DTO
            return project != null ? projectDtoFactory.ToDToProjectShow(project) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the Project from the database:", ex);
        }
    }

    public async Task<ProjectShowDto> UpdateProject(ProjectUpdateDto projectUpdateDto)
    {
        try
        {
            // Get the project from the database
            var project = await projectRepository.GetAsync(p => p!.Id == projectUpdateDto.Id)
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
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the updated project as a display DTO
            return projectDtoFactory.ToDToProjectShow(project)!;

        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Throw an exception with a message
            throw new Exception("Could not update the project in the database:", ex);
        }

    }

}
