using Core.DTOs.ServicesContracts;

namespace Core.Interfaces.ServiceContractsI;

public interface IPaymentService
{
    Task<PaymentTypeShowDto?> GetPaymentTypeByIdAsync(int id);
}