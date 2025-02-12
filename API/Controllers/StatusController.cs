using System.Data.Common;
using API.Helpers;
using Core.DTOs.Project;
using Core.Interfaces.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
///
/// </summary>
/// <param name="statusService"></param>
/// <param name="environment"></param>
[ApiController]
[Route("api/[controller]")]
public class StatusController(IStatusService statusService, IWebHostEnvironment environment)
    : ControllerBase
{
    /// <summary>
    /// Create a new status
    /// </summary>
    /// <param name="status">Status creation DTO</param>
    /// <returns>Created status</returns>
    [HttpPost]
    [ProducesResponseType<StatusDisplay>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateStatusAsync([FromBody] StatusInsert status)
    {
        try
        {
            // Create the status
            var displayDto = await statusService.CreateStatusAsync(status);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetStatusById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        // Catch duplicate key error
        catch (DbUpdateException ex) when (IsDuplicateKeyError(ex))
        {
            // Return a conflict response
            return ApiResponseHelper.ConflictDuplicate("Status already exists");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    /// Get a status by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetStatusById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetStatusByIdAsync([FromRoute] int id)
    {
        try
        {
            // Get the status from the database
            var status = await statusService.GetStatusByIdAsync(id);
            // Return the status
            return status != null
                ? ApiResponseHelper.Success(status)
                : ApiResponseHelper.NotFound("Status not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    ///  Check if the error is a duplicate key error
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    private static bool IsDuplicateKeyError(DbUpdateException ex)
    {
        // Check if the exception is a SqlException and if the error number is 2601 or 2627
        // Why this numbers? Because they are the error numbers for duplicate key errors
        // in Entity Framework Core
        return ex.InnerException is SqlException sqlEx
            && (sqlEx.Number == 2601 || sqlEx.Number == 2627);
    }
}
