using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IProjectService
{
    Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertFormDto projectInsertFormDto);
    Task<ProjectShowDto?> GetProjectByIdAsync(int id);
    Task<ProjectShowDto?> GetProjectByStatusAsync(int id);
    Task<IEnumerable<ProjectShowDto>> GetAllProjectsAsync();

    Task<ProjectUpdateDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
    Task<IEnumerable<ProjectShowDto>> UpdateProjectStatusAsync(int oldStatus);
    Task<ProjectDeleteShowDto> DeleteProjectAsync(int id);
}
