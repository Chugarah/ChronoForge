using Core.DTOs.Project;
using Domain;

namespace Core.Interfaces.DTos;

public interface IProjectDtoFactory
{
    Projects? ToDomainProjectInsert(ProjectInsertDto createProjectDomain);
    ProjectShowDto? ToDtoProjectShow(Projects projects);
    IEnumerable<ProjectShowDto> ToDtoProjectShow(IEnumerable<Projects?> projects);
    ProjectDeleteShowDto? ToDtoDeleteShow(Projects projects);
}
