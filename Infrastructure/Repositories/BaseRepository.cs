using System.Diagnostics;
using Core.Interfaces;
using Core.Interfaces.Data;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Inspired By Hans's tutorial: https://www.youtube.com/watch?v=lsEEYvCtFi4
/// This is the base repository class that all other repositories will inherit from.
/// I am skipping the Try Catch block and null checks;
/// We will move that into our service layer
/// I need to use AI Phind to implement BaseRepository
/// parameters and the Factory class
/// </summary>
public abstract class BaseRepository<TDomain, TEntity>(
    DataContext dataContext,
    IEntityFactory<TDomain, TEntity> factory
) : IBaseRepository<TDomain>
    where TDomain : class
    where TEntity : class
{
    /// <summary>
    /// Let's load in our DbSet
    /// This is the table that we will be working with, and it's using
    /// the TEntity that we have passed in as generic type
    /// </summary>
    private readonly DbSet<TEntity> _dbSet = dataContext.Set<TEntity>();

    /// <summary>
    /// This converts a domain object to an entity object
    /// using the factory that we have passed in.
    /// Domain -> Entity
    /// We are not doing any transaction management here, we have our
    /// UnitOfWork class for that
    /// </summary>
    /// <param name="domainEntity"></param>
    /// <returns></returns>
    public virtual async Task<TDomain> CreateAsync(TDomain domainEntity)
    {
        // Convert the domain object to an entity object
        var entity = factory.ToEntity(domainEntity);

        // Add the entity to the DbSet
        await _dbSet.AddAsync(entity);

        // Return the domain object
        return factory.ToDomain(entity);

    }
}
