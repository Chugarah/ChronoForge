using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IStatusService
{
    public Task CreateStatusAsync(StatusInsert status);
    
}