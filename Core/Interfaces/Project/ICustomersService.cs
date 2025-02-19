using Core.DTOs.Project.Customers;

namespace Core.Interfaces.Project;

public interface ICustomersService
{
    Task<CustomerShowDto?> CreateCustomerAsync(CustomersInsertDto customersInsertDto);
    Task<CustomerShowDto?> GetCustomerByIdAsync(int id);
    Task<CustomerShowDto?> GetCustomerByStatusAsync(int id);
    Task<IEnumerable<CustomerShowDto>> GetAllCustomersAsync();

    // TODO: Implement the following methods if needed
    //  Task<CustomerShowDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
    //  Task<IEnumerable<CustomerShowDto>> UpdateProjectStatusAsync(int oldStatus);
    // Task<ProjectDeleteShowDto> DeleteProjectAsync(int id);
}
