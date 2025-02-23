using System.Net;
using Core.DTOs.Associations;
using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;
using Domain;

namespace Core.Interfaces.DTos;

public interface IAssociationFactory
{
    // This will show the service contracts that are associated with a project
    IEnumerable<AssociationShowProjectServiceContractDto> AssociationShowProjectServiceContractDtos(
        IEnumerable<ServiceContracts?> projectServiceContracts
    );

    // This will show a result after associating a service contract to a project
    AssociationResultDto AssociationResultDto(
        bool success,
        string message,
        HttpStatusCode statusCode
    );

    AssociationFullResultDto AssociationFullResultDto(ProjectShowDto? projectShowDto,
        ServiceContractsShowDto? serviceContract,
        AssociationResultDto associationResult);
}
