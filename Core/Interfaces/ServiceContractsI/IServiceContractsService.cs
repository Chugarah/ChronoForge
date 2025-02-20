using Core.DTOs.ServicesContracts;

namespace Core.Interfaces.ServiceContractsI;

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
