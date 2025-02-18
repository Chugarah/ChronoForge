using API.Helpers;
using API.Interfaces;
using Core.DTOs.Project;
using Core.DTOs.Project.Status;
using Core.Interfaces.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Project Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectController(
    IProjectService projectService,
    IWebHostEnvironment environment,
    ICommonHelpers commonHelpers
) : ControllerBase
{
    /// <summary>
    /// Create a new status
    /// </summary>
    /// <param name="projectInsert">Status creation DTO</param>
    /// <returns>Created status</returns>
    [HttpPost]
    [ProducesResponseType<ProjectShowDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateProjectAsync([FromBody] ProjectInsertDto projectInsert)
    {
        try
        {
            // Create the status
            var displayDto = await projectService.CreateProjectAsync(projectInsert);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetProjectById",
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
    /// Get a Project by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetProjectById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetProjectByIdAsync([FromRoute] int id)
    {
        try
        {
            // Get the status from the database
            var status = await projectService.GetProjectByIdAsync(id);
            // Return the status
            return status != null
                ? ApiResponseHelper.Success(status)
                : ApiResponseHelper.NotFound("Project not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }


    /// <summary>
    /// Update a project
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType<StatusDisplayDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<StatusDisplayDto>(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateProject([FromBody] ProjectUpdateDto projectUpdateDto)
    {
        try
        {
            // Create the status
            var displayDto = await projectService.UpdateProjectAsync(projectUpdateDto);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetProjectById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        catch (DbUpdateException ex) when (commonHelpers.IsForeignKeyError(ex))
        {
            // Return a conflict response
            return ApiResponseHelper.BadRequestWithMessage(
                "Invalid Entity Reference",
                "Referenced entity does not exist"
            );
        }
        // Catch if the project is not found
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

    /// <summary>
    /// Delete a project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}", Name = "DeleteProjectById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteProjectAsync([FromRoute] int id)
    {
        try
        {
            // Delete the status
            var deleteStatus = await projectService.DeleteProjectAsync(id);
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
