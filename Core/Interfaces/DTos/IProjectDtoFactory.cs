using Core.DTOs.Project;
using Domain;

namespace Core.Interfaces.DTos;

public interface IProjectDtoFactory
{
    Projects? ToDomainProjectInsert(ProjectInsertDto createProjectDomain);
    ProjectShowDto? ToDtoProjectShow(Projects projects);
    ProjectDeleteShowDto? ToDtoDeleteShow(Projects projects);
}