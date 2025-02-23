using Core.DTOs.ServicesContracts;
using Domain;

namespace Core.Interfaces.DTos;

public interface IServiceContractsServiceDtoFactory
{
    ServiceContracts? ToDomainServiceContractsInsert(
        ServiceContractsInsertDto serviceContractsInsertDto
    );
    ServiceContracts? ToDomainServiceContractsUpdate(
        ServiceContractsUpdateDto serviceContractsUpdateDto
    );
    ServiceContractsShowDto? ToDtoServiceContractDisplay(ServiceContracts? serviceContracts);
    IEnumerable<ServiceContractsShowDto>? ToDtoServiceContractDisplay(IEnumerable<ServiceContracts?> serviceContracts);
}
