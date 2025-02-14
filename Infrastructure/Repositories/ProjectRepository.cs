using Core.Interfaces.Data;
using Core.Interfaces.Project;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ProjectRepository(DataContext dbContext, IEntityFactory<Projects?, ProjectsEntity> factory)
    : BaseRepository<Projects, ProjectsEntity>(dbContext, factory),
        IProjectRepository
{
    
}