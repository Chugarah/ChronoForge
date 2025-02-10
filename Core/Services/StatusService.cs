using System.Data.Common;
using Core.DTOs.Project;
using Core.Interfaces.Data;
using Core.Interfaces.Project;

namespace Core.Services;

public class StatusService(
    IStatusRepository statusRepository,
    IStatusDtoFactory statusDtoFactory,
    IUnitOfWork unitOfWork
) : IStatusService
{
    /// <summary>
    /// Using AI Phind to create Summary
    /// Creates a new status entity in the database using DTO input
    /// </summary>
    /// <param name="statusInsertDto">Data transfer object containing status properties</param>
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
    public async Task CreateStatusAsync(StatusInsert statusInsertDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var status = statusDtoFactory.ToDomain(statusInsertDto);

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the status in the database
            await statusRepository.CreateAsync(status);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Throw an exception with a message
            throw new Exception("Could not created an new Status in the database:", ex);
        }
    }
}
