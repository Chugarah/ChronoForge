using Core.DTOs.Project;
using Core.DTOs.Project.Status;

namespace Core.Interfaces.Project;

public interface IStatusService
{
    Task<StatusDisplayDto?> CreateStatusAsync(StatusInsertDto status);
    Task<StatusDisplayDto?> GetStatusByIdAsync(int id);
    Task<StatusDisplayDto> UpdateStatusAsync(StatusUpdateDto statusUpdateDtoDto);
    Task<StatusDisplayDto> DeleteStatusAsync(int id);

}