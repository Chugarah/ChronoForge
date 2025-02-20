using API.Helpers;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Service Contracts Controller
/// </summary>
/// <param name="serviceContractsService"></param>
/// <param name="environment"></param>
[ApiController]
[Route("api/[controller]")]
public class ServiceContractsController(
    IServiceContractsService serviceContractsService,
    IWebHostEnvironment environment
) : ControllerBase
{
    /// <summary>
    /// Create a new Service Contract
    /// </summary>
    /// <param name="serviceContractsInsertDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<ServiceContractsShowDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateServiceContractsAsync(
        [FromBody] ServiceContractsInsertDto serviceContractsInsertDto
    )
    {
        try
        {
            // Create the Service Contracts
            var displayDto = await serviceContractsService.CreateServiceContractsAsync(
                serviceContractsInsertDto
            );
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetServiceContractsById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        catch (Exception ex)
        {
            // With proper EF Core exception handling:
            return ex is DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } }
                ? ApiResponseHelper.ConflictDuplicate("Project already exists")
                :
                // Return a problem response
                ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    /// Get a Service Contracts by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetServiceContractsById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetServiceContractsById([FromRoute] int id)
    {
        try
        {
            // Get the Service Contracts from the database
            var serviceContracts = await serviceContractsService.GetServiceContractsByIdAsync(id);
            // Return the Service Contracts
            return serviceContracts != null
                ? ApiResponseHelper.Success(serviceContracts)
                : ApiResponseHelper.NotFound("Service Contracts not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    /// Get all Service Contracts
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("GetAllServiceContracts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetAllServiceContractsAsync()
    {
        try
        {
            // Get the projects from the database
            var serviceContracts = await serviceContractsService.GetAllServiceContractsAsync();
            // Return the projects
            IEnumerable<ServiceContractsShowDto> projectShowDos = serviceContracts!.ToList();
            return projectShowDos.Count() != 0
                ? ApiResponseHelper.Success(projectShowDos)
                : ApiResponseHelper.NotFound("No projects found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }
}
