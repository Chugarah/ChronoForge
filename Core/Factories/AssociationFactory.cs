using System.Net;
using Core.DTOs.Associations;
using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class AssociationFactory : IAssociationFactory
{
    /// <summary>
    /// This will show the service contracts that are associated with a project
    /// </summary>
    /// <param name="projectServiceContracts"></param>
    /// <returns></returns>
    public IEnumerable<AssociationShowProjectServiceContractDto> AssociationShowProjectServiceContractDtos(
        IEnumerable<ServiceContracts?> projectServiceContracts
    ) =>
        projectServiceContracts.Select(s => new AssociationShowProjectServiceContractDto
        {
            Id = s!.Id,
            PaymentTypeId = s.PaymentTypeId,
            CustomerId = s.CustomerId,
            Name = s.Name,
            Price = s.Price,
        });

    /// <summary>
    /// This will show a result after associating a service contract to a project
    /// attaching or detaching a service contract to a project
    /// </summary>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public AssociationResultDto AssociationResultDto(bool success, string message, HttpStatusCode statusCode)
    {
        return new AssociationResultDto { Success = success, Message = message, StatusCode = statusCode };
    }

    /// <summary>
    /// This will show the result of the association between a project and a service contract
    /// </summary>
    /// <param name="projectShowDto"></param>
    /// <param name="serviceContractsShowDto"></param>
    /// <param name="associationResultDto"></param>
    /// <returns></returns>
    public AssociationFullResultDto AssociationFullResultDto(
        ProjectShowDto? projectShowDto,
        ServiceContractsShowDto? serviceContractsShowDto,
        AssociationResultDto associationResultDto
    )
    {
        return new AssociationFullResultDto(projectShowDto, serviceContractsShowDto, associationResultDto)
        {
            ProjectShowDto = projectShowDto,
            ServiceContract = serviceContractsShowDto,
            AssociationResultDto = associationResultDto
        };
    }
}
