using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;

namespace Core.DTOs.Associations;

/// <summary>
/// This will show a result after associating a service contract to a project
/// a Composite DTO
/// </summary>
/// <param name="ProjectShowDto"></param>
/// <param name="ServiceContract"></param>
public record AssociationFullResultDto(
       ProjectShowDto? ProjectShowDto,
       ServiceContractsShowDto? ServiceContract,
       AssociationResultDto AssociationResultDto
   );