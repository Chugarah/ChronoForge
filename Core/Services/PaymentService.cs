using System.Data.Common;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.ServiceContractsI;

namespace Core.Services;

public class PaymentService(
    IPaymentTypeRepository paymentTypeRepository,
    IPaymentTypeFactory paymentTypeFactory
) : IPaymentService
{
    public async Task<PaymentTypeShowDto?> GetPaymentTypeByIdAsync(int id)
    {
        try
        {
            // Get the paymentType from the database
            var paymentType = await paymentTypeRepository.GetAsync(p => p != null && p.Id == id);

            // Convert the paymentType to a display DTO
            return paymentType != null ? paymentTypeFactory.ToDtoStatusDisplay(paymentType) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the User from the database:", ex);
        }
    }
}
