using API.Helpers;
using Core.Interfaces.ServiceContractsI;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Payment Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentTypeController(IWebHostEnvironment environment, IPaymentService paymentService)
    : ControllerBase
{
    /// <summary>
    /// Get a Payment Type by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetPaymentTypeById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetPaymentTypeByIdAsync([FromRoute] int id)
    {
        try
        {
            // Get the Payment Type from the database
            var paymentType = await paymentService.GetPaymentTypeByIdAsync(id);
            // Return the Payment Type
            return paymentType != null
                ? ApiResponseHelper.Success(paymentType)
                : ApiResponseHelper.NotFound("User not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }
}
