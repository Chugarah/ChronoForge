using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;

namespace Core.Interfaces.Project;

/// <summary>
///  Interface for the ServiceContractsService
/// </summary>
public interface IServiceContractsService
{
    Task<ServiceContractsShowDto?> CreateServiceContractsAsync(
        ServiceContractsInsertDto serviceContractsInsertDto
    );
    Task<ServiceContractsShowDto?> GetServiceContractsByIdAsync(int id);
    Task<IEnumerable<ServiceContractsShowDto>?> GetAllServiceContractsAsync();

    Task<ServiceContractsShowDto> UpdateServiceContractsAsync(
        ServiceContractsUpdateDto serviceContractsUpdateDto
    );
}
