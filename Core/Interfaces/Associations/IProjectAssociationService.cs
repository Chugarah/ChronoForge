using Core.DTOs.Associations;
using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;
using Domain;

namespace Core.Interfaces.Associations;

public interface IProjectAssociationService
{
    Task<AssociationFullResultDto> AttachServiceContractToProjectAsync(
        ProjectInsertFormDto projectInsertFormDto,
        ServiceContractsInsertDto serviceContractsInsertDto);
    Task<AssociationResultDto> DetachServiceContractFromProjectAsync(
        int projectId,
        int serviceContractId
    );
    Task<IEnumerable<ServiceContracts?>> GetServiceContractsForProjectAsync(int projectId);
}
