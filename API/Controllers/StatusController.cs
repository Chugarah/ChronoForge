using API.Helpers;
using API.Interfaces;
using Core.DTOs.Project.Status;
using Core.Interfaces.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Status Controller
/// </summary>
/// <param name="statusService"></param>
/// <param name="environment"></param>
/// <param name="commonHelpers"></param>
[ApiController]
[Route("api/[controller]")]
public class StatusController(
    IStatusService statusService,
    IWebHostEnvironment environment,
    ICommonHelpers commonHelpers
) : ControllerBase
{
    /// <summary>
    /// Create a new status
    /// </summary>
    /// <param name="status">Status creation DTO</param>
    /// <returns>Created status</returns>
    [HttpPost]
    [ProducesResponseType<StatusDisplayDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateStatusAsync([FromBody] StatusInsertDto status)
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
        catch (DbUpdateException ex) when (commonHelpers.IsDuplicateKeyError(ex))
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
    /// Updates a status
    /// </summary>
    /// <param name="status">Status creation DTO</param>
    /// <returns>Created status</returns>
    [HttpPut]
    [ProducesResponseType<StatusDisplayDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> UpdateStatus([FromBody] StatusUpdateDto status)
    {
        try
        {
            // Create the status
            var displayDto = await statusService.UpdateStatusAsync(status);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetStatusById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        // Catch if the status is not found
        catch (KeyNotFoundException ex)
        {
            // Return 404 response if the status is not found
            return ApiResponseHelper.NotFound(ex.Message);
        }
        // Catch duplicate key error
        catch (DbUpdateException ex) when (commonHelpers.IsDuplicateKeyError(ex))
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
    /// Remove Status by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}", Name = "DeleteStatusById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteStatusAsync([FromRoute] int id)
    {
        try
        {
            // Delete the status
            var deleteStatus = await statusService.DeleteStatusAsync(id);
            // Return the status
            return ApiResponseHelper.Success(deleteStatus);
        }
        // Catch if the status is not found
        catch (KeyNotFoundException ex)
        {
            // Return 404 response if the status is not found
            return ApiResponseHelper.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }
}
