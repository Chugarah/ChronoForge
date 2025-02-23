using Core.Interfaces.Data;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Projects;

public class ProjectRepository(
    DataContext dbContext,
    IEntityFactory<Domain.Projects?, ProjectsEntity> factory
) : BaseRepository<Domain.Projects, ProjectsEntity>(dbContext, factory), IProjectRepository
{
    /// <summary>
    /// Attaching a project to the database
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public new async Task<Domain.Projects?> AttachAsync(Domain.Projects? project)
    {
        // Attach the project to the database
        var attached = await base.AttachAsync(project);
        // If the project is not attached, throw an exception
        return attached ?? throw new InvalidOperationException("Attach operation failed");
    }

}
