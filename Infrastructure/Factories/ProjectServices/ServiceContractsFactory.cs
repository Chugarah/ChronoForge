using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.ProjectServices;

/// <summary>
///
/// </summary>
public class ServiceContractsFactory : EntityFactoryBase<ServiceContracts, ServiceContractsEntity>
{
    /// <summary>
    /// Creating from Domain object to an Entity object
    /// </summary>
    /// <param name="serviceContractsEntity"></param>
    /// <returns></returns>
    public override ServiceContracts ToDomain(ServiceContractsEntity serviceContractsEntity)
    {
        var services = new ServiceContracts
        {
            Id = serviceContractsEntity.Id,
            CustomerId = serviceContractsEntity.CustomerId,
            PaymentTypeId = serviceContractsEntity.PaymentTypeId,
            Name = serviceContractsEntity.Name,
            Price = serviceContractsEntity.Price,
        };
        return services;
    }

    /// <summary>
    /// Creating from Domain object to an Entity object
    /// </summary>
    /// <param name="serviceContracts"></param>
    /// <returns></returns>
    public override ServiceContractsEntity ToEntity(ServiceContracts serviceContracts)
    {
        var serviceEntity = new ServiceContractsEntity
        {
            Id = serviceContracts.Id,
            CustomerId = serviceContracts.CustomerId,
            PaymentTypeId = serviceContracts.PaymentTypeId,
            Name = serviceContracts.Name,
            Price = serviceContracts.Price,
        };
        return serviceEntity;
    }
}
