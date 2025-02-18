using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IProjectService
{
    Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertDto projectInsertDto);
    Task<ProjectShowDto?> GetProjectByIdAsync(int id);
    Task<ProjectShowDto?> GetProjectByStatusAsync(int id);

    Task<ProjectShowDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
    Task<IEnumerable<ProjectShowDto>> UpdateProjectStatusAsync(int oldStatus);
    Task<ProjectDeleteShowDto> DeleteProjectAsync(int id);
}
