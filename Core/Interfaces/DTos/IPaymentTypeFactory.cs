using Core.DTOs.ServicesContracts;
using Domain;

namespace Core.Interfaces.DTos;

/// <summary>
/// This interface is used to convert the PaymentType to PaymentTypeShowDto
/// </summary>
public interface IPaymentTypeFactory
{
    PaymentTypeShowDto? ToDtoStatusDisplay(PaymentType? paymentType);
}