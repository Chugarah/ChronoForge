using System.Data.Common;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Core.Interfaces.ServiceContractsI;

namespace Core.Services;

public class ServiceContractsService(
    IServiceContractsRepository serviceContractsRepository,
    IServiceContractsServiceDtoFactory serviceContractsServiceDtoFactory,
    IUnitOfWork unitOfWork
) : IServiceContractsService
{
    /// <summary>
    /// Create a new Service Contract
    /// </summary>
    /// <param name="serviceContractsInsertDto"></param>
    /// <returns></returns>
    public async Task<ServiceContractsShowDto?> CreateServiceContractsAsync(
        ServiceContractsInsertDto serviceContractsInsertDto
    )
    {
        try
        {
            // Convert the DTO to a domain object
            var serviceContractInsert =
                serviceContractsServiceDtoFactory.ToDomainServiceContractsInsert(
                    serviceContractsInsertDto
                );

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the project in the database
            await serviceContractsRepository.CreateAsync(serviceContractInsert);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the created project as a display DTO
            return serviceContractsServiceDtoFactory.ToDtoServiceContractDisplay(
                serviceContractInsert
            );
        }
        catch (Exception)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    /// Get a Service Contract by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceContractsShowDto?> GetServiceContractsByIdAsync(int id)
    {
        try
        {
            // Get the ServiceContract from the database
            var serviceContracts = await serviceContractsRepository.GetAsync(s =>
                s != null && s.Id == id,false
            );

            // Convert the ServiceContract to a display DTO
            return serviceContracts != null
                ? serviceContractsServiceDtoFactory.ToDtoServiceContractDisplay(serviceContracts)
                : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the Service Contract from the database:", ex);
        }
    }

    /// <summary>
    /// Get all the Service Contracts
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ServiceContractsShowDto>?> GetAllServiceContractsAsync()
    {
        try
        {
            // Get all the ServiceContract from the database
            var serviceContracts = await serviceContractsRepository.GetAllAsync(s => s != null);

            // Convert the ServiceContract to a display DTO
            return serviceContractsServiceDtoFactory.ToDtoServiceContractDisplay(serviceContracts);
        }
        catch (DbException)
        {
            // Throw an exception with a message
            return Enumerable.Empty<ServiceContractsShowDto>();
        }
    }

    /// <summary>
    /// Update a Service Contract
    /// </summary>
    /// <param name="serviceContractsUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceContractsShowDto> UpdateServiceContractsAsync(
        ServiceContractsUpdateDto serviceContractsUpdateDto
    )
    {
        try
        {
            // Get the ServiceContract from the database
            var serviceContracts =
                await serviceContractsRepository.GetAsync(s =>
                    s!.Id == serviceContractsUpdateDto.Id,true
                ) ?? throw new Exception("Could not find the ServiceContract in the database");

            // Update the ServiceContract with the new values
            serviceContracts.CustomerId = serviceContractsUpdateDto.CustomerId;
            serviceContracts.PaymentTypeId = serviceContractsUpdateDto.PaymentTypeId;
            serviceContracts.Name = serviceContractsUpdateDto.Name;
            serviceContracts.Price = serviceContractsUpdateDto.Price;

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Update the ServiceContract in the database
            await serviceContractsRepository.UpdateAsync(serviceContracts);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the updated project as a display DTO
            return serviceContractsServiceDtoFactory.ToDtoServiceContractDisplay(serviceContracts)!;
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
