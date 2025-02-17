using Core.DTOs.Project;

namespace Core.Interfaces.Project;

public interface IProjectService
{
    Task<ProjectShowDto?> CreateProjectAsync(ProjectInsertDto projectInsertDto);
    Task<ProjectShowDto?> GetProjectById(int id);

    Task<ProjectShowDto> UpdateProject(ProjectUpdateDto projectUpdateDto);
    //   Task<ProjectShow> DeleteStatusAsync(int id);
}
