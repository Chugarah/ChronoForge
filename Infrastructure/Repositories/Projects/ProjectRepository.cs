using Core.Interfaces.Data;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Projects;

public class ProjectRepository(
    DataContext dbContext,
    IEntityFactory<Domain.Projects?, ProjectsEntity> factory
) : BaseRepository<Domain.Projects, ProjectsEntity>(dbContext, factory), IProjectRepository { }
