using Core.DTOs.Project;
using Domain;

namespace Core.Interfaces.DTos;

public interface IProjectDtoFactory
{
    Projects? ToDomainProjectInsert(ProjectInsertFormDto createProjectDomain);
    Projects ToDomainProjectUpdate(ProjectUpdateDto updateProjectDomain);
    IEnumerable<Projects> ToDomainProjectUpdateList(IEnumerable<ProjectUpdateDto> updateProjectDomain);
    ProjectShowDto? ToDtoProjectShow(Projects? projects);

    IEnumerable<ProjectShowDto> ToDtoProjectShow(IEnumerable<Projects?> projects);
    ProjectDeleteShowDto? ToDtoDeleteShow(Projects projects);

}
