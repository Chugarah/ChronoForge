using API.Helpers;
using Core.DTOs.Project;
using Core.DTOs.Project.Customers;
using Core.Interfaces.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Customers Controller
/// </summary>
/// <param name="customersService"></param>
/// <param name="environment"></param>
[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    ICustomersService customersService,
    IWebHostEnvironment environment
) : ControllerBase
{

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="customersInsertDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<CustomerShowDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateCustomerAsync([FromBody] CustomersInsertDto customersInsertDto)
    {
        try
        {
            // Create the customer
            var displayDto = await customersService.CreateCustomerAsync(customersInsertDto);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetCustomerById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        catch (Exception ex)
        {
            // With proper EF Core exception handling:
            return ex is DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } }
                ? ApiResponseHelper.ConflictDuplicate("Customer already exists")
                :
                // Return a problem response
                ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetCustomerById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetCustomerByIdAsync([FromRoute] int id)
    {
        try
        {
            // Get the status from the database
            var customer = await customersService.GetCustomerByIdAsync(id);
            // Return the status
            return customer != null
                ? ApiResponseHelper.Success(customer)
                : ApiResponseHelper.NotFound("Customer not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }
}
