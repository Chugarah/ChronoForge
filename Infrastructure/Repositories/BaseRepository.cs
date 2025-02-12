using System.Diagnostics;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Interfaces.Data;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Base repository implementation following Clean Architecture principles
/// Inspired By: Hans's tutorial (https://www.youtube.com/watch?v=lsEEYvCtFi4)
/// Key Features:
/// 1. Generic CRUD operations for domain-driven design
/// 2. Automatic domain/entity conversion via factory pattern
/// 3. Transaction management delegation to UnitOfWork
/// 4. Clear layer separation (Core never references EF entities)
/// </summary>
/// <remarks>
/// Implementation notes:
/// - Uses <see cref="IEntityFactory{TDomain,TEntity}"/> for bi-directional conversions
/// - All database operations are async-first
/// - Predicate conversion handles domain-to-entity type translation
/// - Designed for extension not modification (open/closed principle)
/// </remarks>
public abstract class BaseRepository<TDomain, TEntity>(
    DataContext dataContext,
    IEntityFactory<TDomain?, TEntity> factory
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
    /// Creates a new domain entity in the database
    /// Flow: Domain Object → Entity Conversion → Database Insert → Return Managed Domain Object
    /// </summary>
    /// <param name="domainEntity">Domain model instance to persist</param>
    /// <returns>Managed domain object with any database-generated values</returns>
    /// <remarks>
    /// Uses factory for conversion to ensure layer separation
    /// Transaction management handled by UnitOfWork
    /// </remarks>
    public virtual async Task<TDomain?> CreateAsync(TDomain? domainEntity)
    {
        // Convert the domain object to an entity object
        var entity = factory.ToEntity(domainEntity);

        // Add the entity to the DbSet
        await _dbSet.AddAsync(entity);

        // Return the domain object
        return factory.ToDomain(entity);
    }

    /// <summary>
    /// Retrieves a single domain entity using type-safe domain predicates
    /// Flow: Domain Predicate → Entity Predicate Conversion → Database Query → Domain Object
    /// </summary>
    /// <param name="domainPredicate">LINQ expression against domain model</param>
    /// <returns>Matching domain object or null</returns>
    /// <remarks>
    /// AsNoTracking used for read-only operations
    /// Automatic predicate conversion maintains layer isolation
    /// </remarks>
    public async Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> domainPredicate)
    {
        // Convert the domain predicate to an entity predicate
        var entityPredicate = factory.CreateEntityPredicate(domainPredicate);

        // Get the entity from the DbSet
        var entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(entityPredicate);

        // Convert the entity back to a domain object
        return entity != null ? factory.ToDomain(entity) : null;
    }

    /// <summary>
    ///  Updates an existing domain entity in the database
    /// </summary>
    /// <param name="domainEntity"></param>
    /// <returns></returns>
    public Task<TDomain?> UpdateAsync(TDomain? domainEntity)
    {
        // Convert the domain object to an entity object
        var entity = factory.ToEntity(domainEntity);

        // Update the entity in the DbSet
        _dbSet.Update(entity);

        // Return the domain object
        return Task.FromResult(factory.ToDomain(entity));
    }

    /// <summary>
    /// Deletes an existing domain entity in the database
    /// </summary>
    /// <param name="domainEntity"></param>
    /// <returns></returns>
    public Task<TDomain?> DeleteAsync(TDomain? domainEntity)
    {
        // Convert the domain object to an entity object
        var entity = factory.ToEntity(domainEntity);

        // Remove the entity from the DbSet
        _dbSet.Remove(entity);

        // Return the domain object
        return Task.FromResult(factory.ToDomain(entity));
    }

}
