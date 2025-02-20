using Core.DTOs.ServicesContracts;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class PaymentTypeFactory : IPaymentTypeFactory
{
    /// <summary>
    /// This method is used to convert the PaymentType to PaymentTypeShowDto
    /// </summary>
    /// <param name="paymentType"></param>
    /// <returns></returns>
    public PaymentTypeShowDto? ToDtoStatusDisplay(PaymentType? paymentType)
    {
        // Another way of writing, instead of declaring a variable
        // we can return the object directly
        return new PaymentTypeShowDto
        {
            Id = paymentType!.Id,
            Name = paymentType.Name,
            Currency = paymentType.Currency,
        };
    }
}
