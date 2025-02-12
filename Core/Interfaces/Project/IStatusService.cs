using Core.DTOs.Project;
using Core.DTOs.Project.Status;

namespace Core.Interfaces.Project;

public interface IStatusService
{
    Task<StatusDisplay?> CreateStatusAsync(StatusInsert status);
    Task<StatusDisplay?> GetStatusByIdAsync(int id);
    Task<StatusDisplay> UpdateStatusAsync(StatusUpdate statusUpdateDto);
    Task<StatusDisplay> DeleteStatusAsync(int id);

}