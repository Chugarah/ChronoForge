using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.ProjectServices;

/// <summary>
/// ServiceContractsFactory
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
    public override ServiceContractsEntity ToEntity(ServiceContracts domain)
    {
        return new ServiceContractsEntity
        {
            Id = domain.Id,
            CustomerId = domain.CustomerId,
            PaymentTypeId = domain.PaymentTypeId,
            Name = domain.Name,
            Price = domain.Price,
            // Map relationships through IDs only
            ProjectsEntity = domain.Projects?
                .Where(p => p?.Id > 0)
                .Select(p => new ProjectsEntity { Id = p.Id })
                .ToList() ?? new List<ProjectsEntity>()
        };
    }
}
