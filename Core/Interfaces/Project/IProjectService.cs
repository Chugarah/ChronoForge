using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IProjectService
{
    Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertDto projectInsertDto);
    Task<ProjectShowDto?> GetProjectById(int id);
    //   Task<ProjectShow> UpdateStatusAsync(StatusUpdate statusUpdateDto);
    //   Task<ProjectShow> DeleteStatusAsync(int id);
}
