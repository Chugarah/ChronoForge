using Core.DTOs.Project;
using Core.DTOs.Project.Customers;
using Domain;

namespace Core.Interfaces.DTos;

public interface ICustomersDtoFactory
{
    Customers? ToDomainCustomerInsert(CustomersInsertDto customersInsertDto);
    CustomersInsertDto? ToEntityProjectInsert(Customers customers);

    Customers ToDomainShowCustomer(CustomerShowDto customerShowDto);
    CustomerShowDto ToCustomerShow(Customers? customers);
    IEnumerable<CustomerShowDto> ToCustomerShow(IEnumerable<Customers?> customers);

}