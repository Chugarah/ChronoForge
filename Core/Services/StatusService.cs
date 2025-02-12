﻿using System.Data.Common;
using Core.DTOs.Project;
using Core.Interfaces.Data;
using Core.Interfaces.Project;
using Domain;
using Microsoft.Data.SqlClient;

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
    public async Task<StatusDisplay?> CreateStatusAsync(StatusInsert statusInsertDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var status = statusDtoFactory.ToDomain(statusInsertDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the status in the database
            await statusRepository.CreateAsync(status);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Get the created status from the database, this is required to follow
            // restful API response 201 best practices
            var createdStatus =
                await statusRepository.GetAsync(s => s!.Name == statusInsertDto.Name)
                ?? throw new Exception("Could not find the created status in the database");

            // Return the created status as a display DTO
            return statusDtoFactory.ToDisplayDto(createdStatus);
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Used Rider to Refactor the code
            // Check if the exception is a duplicate key error
            if (ex is SqlException { Number: 2601 or 2627 })
            {
                throw new Exception($"Status name '{statusInsertDto.Name}' already exists", ex);
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
    public async Task<StatusDisplay?> GetStatusByIdAsync(int id)
    {
        try
        {
            // Get the status from the database
            var status = await statusRepository.GetAsync(s => s!.Id == id);
            // Convert the status to a display DTO
            return status != null ? statusDtoFactory.ToDisplayDto(status) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the status from the database:", ex);
        }
    }
}
