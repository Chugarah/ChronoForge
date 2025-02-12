using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IStatusService
{
    Task<StatusDisplay?> CreateStatusAsync(StatusInsert status);
    Task<StatusDisplay?> GetStatusByIdAsync(int id);
    
}