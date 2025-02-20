using Domain;
using Infrastructure.Entities;

namespace Infrastructure.Factories.ProjectServices;

/// <summary>
/// Mapping from Entity to Domain and vice versa
/// </summary>
public class PaymentTypeFactory : EntityFactoryBase<PaymentType, PaymentTypeEntity>
{
    /// <summary>
    /// Creating from Entity object to a Domain object
    /// </summary>
    /// <param name="paymentTypeEntity"></param>
    /// <returns></returns>
    public override PaymentType ToDomain(PaymentTypeEntity paymentTypeEntity)
    {
        var paymentType = new PaymentType
        {
            Id = paymentTypeEntity.Id,
            Name = paymentTypeEntity.Name,
            Currency = paymentTypeEntity.Currency,
        };
        return paymentType;
    }

    /// <summary>
    /// Creating from Domain object to an Entity object
    /// </summary>
    /// <param name="paymentType"></param>
    /// <returns></returns>
    public override PaymentTypeEntity ToEntity(PaymentType paymentType)
    {
        var paymentTypeEntity = new PaymentTypeEntity
        {
            Id = paymentType.Id,
            Name = paymentType.Name,
            Currency = paymentType.Currency,
        };
        return paymentTypeEntity;
    }
}
