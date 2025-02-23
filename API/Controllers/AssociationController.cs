using System.Net;
using API.Helpers;
using Core.DTOs.Associations;
using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.Associations;
using Core.Interfaces.ServiceContractsI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// Association Controller
/// </summary>
/// <param name="associationService"></param>
[ApiController]
[Route("api/[controller]")]
public class AssociationController(
    IProjectAssociationService associationService
) : ControllerBase
{
    /// <summary>
    /// This will create an association between a project and a service contract
    /// </summary>
    /// <param name="associationFullInsertDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> CreateAssociationProjectService(
        [FromBody] AssociationFullInsertDto associationFullInsertDto)
    {
        // Displaying the result of the association from the service
        var displayResult = await associationService.AttachServiceContractToProjectAsync(
            associationFullInsertDto.ProjectInsertFormDto,
            associationFullInsertDto.ServiceContractsInsertDto
        );

        // Return the result of the association using the status code from the service
        return displayResult.AssociationResultDto.StatusCode switch
        {
            // If the association was successful, return the created status
            HttpStatusCode.Created => Results.Created(
                $"/api/association/{displayResult.ProjectShowDto?.Id}/{displayResult.ServiceContract?.Id}",
                displayResult
            ),
            // If the association was not successful, return a problem response
            _ => ApiResponseHelper.Problem(
                displayResult.AssociationResultDto.Message,
                displayResult.AssociationResultDto.StatusCode)
        };
    }
}