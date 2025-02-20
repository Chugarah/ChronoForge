using API.Helpers;
using API.Interfaces;
using Core.DTOs.Project;
using Core.DTOs.Project.User;
using Core.Interfaces.Project;
using Core.Interfaces.ServiceContractsI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// User Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    IWebHostEnvironment environment,
    ICommonHelpers commonHelpers
) : ControllerBase
{
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userInsertDto">User creation DTO</param>
    /// <returns>Created user</returns>
    [HttpPost]
    [ProducesResponseType<UserShowDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateUserAsync([FromBody] UserInsertDto userInsertDto)
    {
        try
        {
            // Create the status
            var displayDto = await userService.CreateUserAsync(userInsertDto);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetUserById",
                routeValues: new { id = displayDto!.Id, displayDto },
                value: displayDto
            );
        }
        catch (Exception ex)
        {
            // With proper EF Core exception handling:
            return ex is DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } }
                ? ApiResponseHelper.ConflictDuplicate("User already exists")
                :
                // Return a problem response
                ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }

    /// <summary>
    /// Get a User by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            // Get the status from the database
            var user = await userService.GetUserByIdAsync(id);
            // Return the status
            return user != null
                ? ApiResponseHelper.Success(user)
                : ApiResponseHelper.NotFound("User not found");
        }
        catch (Exception ex)
        {
            // Return a problem response
            return ApiResponseHelper.Problem(ex, environment.IsDevelopment());
        }
    }


    /// <summary>
    /// Update a user
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType<UserShowDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<UserShowDto>(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
    {
        try
        {
            // Create the status
            var displayDto = await userService.UpdateUserAsync(userUpdateDto);
            // Return a created response
            return Results.CreatedAtRoute(
                routeName: "GetUserById",
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

}
