using Core.DTOs.Project.Customers;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class CustomersDtoFactory : ICustomersDtoFactory
{
    public Customers? ToDomainCustomerInsert(CustomersInsertDto customersInsertDto)
    {
        // Creating from Domain object to Create a DTO object
        return new Customers
        {
            Name = customersInsertDto.Name,
            ContactPersonId = customersInsertDto.ContactPersonId,
        };
    }

    /// <summary>
    /// This method is used to convert the Customers to CustomerShowDto
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    public CustomersInsertDto? ToEntityProjectInsert(Customers customers)
    {
        // Creating from Domain object to Display a DTO object
        return new CustomersInsertDto
        {
            Name = customers.Name,
            ContactPersonId = customers.ContactPersonId,
        };
    }

    /// <summary>
    /// This method is used to convert the CustomersInsertDto to Customers
    /// </summary>
    /// <param name="customerShowDto"></param>
    /// <returns></returns>
    public Customers ToDomainShowCustomer(CustomerShowDto customerShowDto)
    {
        return new Customers
        {
            Id = customerShowDto.Id,
            Name = customerShowDto.Name,
            ContactPersonId = customerShowDto.ContactPersonId,
        };
    }

    /// <summary>
    /// This method is used to convert the Customers to CustomerShowDto
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    public CustomerShowDto ToCustomerShow(Customers? customers)
    {
        return new CustomerShowDto
        {
            Id = customers!.Id,
            Name = customers.Name,
            ContactPersonId = customers.ContactPersonId,
        };
    }

    /// <summary>
    /// This method is used to convert the Customers to CustomerShowDto
    /// This returns a collection of CustomerShowDto
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    public IEnumerable<CustomerShowDto> ToCustomerShow(IEnumerable<Customers?> customers)
    {
        return customers.Select(c => new CustomerShowDto
        {
            Id = c!.Id,
            Name = c.Name,
            ContactPersonId = c.ContactPersonId,
        });
    }
}
