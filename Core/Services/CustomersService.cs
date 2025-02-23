using System.Data.Common;
using Core.DTOs.Project.Customers;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;

namespace Core.Services;

public class CustomersService(
    ICustomerRepository customerRepository,
    ICustomersDtoFactory customersDtoFactory,
    IUnitOfWork unitOfWork
) : ICustomersService
{
    /// <summary>
    /// This method is used to create a new customer
    /// </summary>
    /// <param name="customersInsertDto"></param>
    /// <returns></returns>
    public async Task<CustomerShowDto?> CreateCustomerAsync(CustomersInsertDto customersInsertDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var customerInsert = customersDtoFactory.ToDomainCustomerInsert(customersInsertDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the customer in the database
            await customerRepository.CreateAsync(customerInsert);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync<object>();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the created customer as a display DTO
           return customersDtoFactory.ToCustomerShow(customerInsert);
        }
        catch (Exception)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<CustomerShowDto?> GetCustomerByIdAsync(int id)
    {
        try
        {
            // Get the status from the database
            var customer = await customerRepository.GetAsync(c => c!.Id == id, false);
            // Convert the status to a display DTO
            return customer != null ? customersDtoFactory.ToCustomerShow(customer) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the status from the database:", ex);
        }
    }

    public Task<CustomerShowDto?> GetCustomerByStatusAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerShowDto>> GetAllCustomersAsync()
    {
        throw new NotImplementedException();
    }
}
