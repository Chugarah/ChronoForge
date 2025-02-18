using System.Data.Common;
using Core.DTOs.Project.Status;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Domain.Constants;
using Microsoft.Data.SqlClient;

namespace Core.Services;

public class StatusService(
    IStatusRepository statusRepository,
    IStatusDtoFactory statusDtoFactory,
    IUnitOfWork unitOfWork,
    IProjectService projectService
) : IStatusService
{
    /// <summary>
    /// Using AI Phind to create Summary
    /// Creates a new status entity in the database using DTO input
    /// </summary>
    /// <param name="statusInsertDtoDto">Data transfer object containing status properties</param>
    /// <exception cref="Exception">Thrown when database operations fail</exception>
    /// <remarks>
    /// Implements Unit of a Work pattern to ensure atomic operations:
    /// <para>- Converts DTO to domain entity using <see cref="IStatusDtoFactory"/></para>
    /// <para>- Manages transaction scope for database operations</para>
    /// <para>- Handles rollback on any database exceptions</para>
    /// </remarks>
    /// <seealso cref="https://medium.com/@josiahmahachi/how-to-use-iunitofwork-single-responsibility-principle-2821398addee"/>
    /// <seealso cref="https://stackoverflow.com/questions/28133801/entity-framework-6-async-operations-and-transcationscope"/>
    /// <seealso cref="https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.infrastructure.dbupdateexception?view=entity-framework-6.2.0"/>
    public async Task<StatusDisplayDto?> CreateStatusAsync(StatusInsertDto statusInsertDtoDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var statusInsert = statusDtoFactory.ToDomainStatusInsert(statusInsertDtoDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the status in the database
            await statusRepository.CreateAsync(statusInsert);

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Get the created status from the database, this is required to follow
            // restful API response 201 best practices
            var deletedStatus =
                await statusRepository.GetAsync(s => s!.Name == statusInsertDtoDto.Name)
                ?? throw new Exception("Could not find the created status in the database");

            // Return the created status as a display DTO
            return statusDtoFactory.ToDtoStatusDisplay(deletedStatus);
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Used Rider to Refactor the code
            // Check if the exception is a duplicate key error
            if (ex is SqlException { Number: 2601 or 2627 })
            {
                throw new Exception($"Status name '{statusInsertDtoDto.Name}' already exists", ex);
            }

            // Throw an exception with a message
            throw new Exception("Could not created an new Status in the database:", ex);
        }
    }

    /// <summary>
    /// Get a status by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<StatusDisplayDto?> GetStatusByIdAsync(int id)
    {
        try
        {
            // Get the status from the database
            var status = await statusRepository.GetAsync(s => s!.Id == id);
            // Convert the status to a display DTO
            return status != null ? statusDtoFactory.ToDtoStatusDisplay(status) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the status from the database:", ex);
        }
    }

    /// <summary>
    /// Update a status in the database
    /// </summary>
    /// <param name="statusUpdateDtoDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<StatusDisplayDto> UpdateStatusAsync(StatusUpdateDto statusUpdateDtoDto)
    {
        try
        {
            // Get the status from the database
            // https://stackoverflow.com/questions/606636/best-way-to-handle-a-keynotfoundexception
            var status =
                await statusRepository.GetAsync(s => s!.Id == statusUpdateDtoDto.Id)
                ?? throw new KeyNotFoundException(
                    $"Status with ID {statusUpdateDtoDto.Id} not found"
                );

            // Update the status properties
            status.Name = statusUpdateDtoDto.Name;

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Update the status in the database
            await statusRepository.UpdateAsync(status);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the updated status as a display DTO
            return statusDtoFactory.ToDtoStatusDisplay(status)!;
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Used Rider to Refactor the code
            // Check if the exception is a duplicate key error
            if (ex is SqlException { Number: 2601 or 2627 })
            {
                throw new Exception($"Status name '{statusUpdateDtoDto.Name}' already exists", ex);
            }

            // Throw an exception with a message
            throw new Exception("Could not update the status in the database:", ex);
        }
    }

    public async Task<StatusDisplayDto> DeleteStatusAsync(int id)
    {
        try
        {
            // Get the status from the database
            var status =
                await statusRepository.GetAsync(s => s!.Id == id)
                ?? throw new KeyNotFoundException($"Status with ID {id} not found");

            // Check if the status is the default status, we cannot delete the default status
            if (id == 1)
            {
                // Throw an exception if the status is the default status
                throw new Exception("Cannot delete the default status");
            }

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Get the project by status
            var projectStatus = await projectService.GetProjectByStatusAsync(id);
            // Check if the project status is not null
            if (projectStatus != null)
            {
                // Update the project status to the default status if you have a project with the status
                // that we want to delete
                var updateProjectStatus = await projectService.UpdateProjectStatusAsync(
                    id
                );
            }

            // Delete the status in the database
            await statusRepository.DeleteAsync(status);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the deleted status as a display DTO
            return statusDtoFactory.ToDtoStatusDisplay(status)!;
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
