using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.Project;

public class CustomerFactory : EntityFactoryBase<Customers, CustomersEntity>
{
    /// <summary>
    /// Creating from Domain object to an Entity object
    /// </summary>
    /// <param name="customersEntity"></param>
    /// <returns></returns>
    public override Customers ToDomain(CustomersEntity customersEntity)
    {
        var customers = new Customers
        {
            Id = customersEntity.Id,
            Name = customersEntity.Name,
            ContactPersonId = customersEntity.ContactPersonId,
        };
        return customers!;
    }

    /// <summary>
    /// Creating from Domain object to Entity object
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    public override CustomersEntity ToEntity(Customers customers) =>
        new()
        {
            Id = customers.Id,
            Name = customers.Name,
            ContactPersonId = customers.ContactPersonId,
        };
}