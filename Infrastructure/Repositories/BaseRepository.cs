﻿using System.Diagnostics;
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
    /// Use Virtual to allow for overriding in derived classes
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
    /// The Code was Inspired by Hans and refactored by Phind AI
    /// Retrieves a single domain entity using type-safe domain predicates
    /// Flow: Domain Predicate → Entity Predicate Conversion → Database Query → Domain Object
    /// </summary>
    /// <param name="domainPredicate">LINQ expression against domain model</param>
    /// <returns>Matching domain object or null</returns>
    /// <remarks>
    /// AsNoTracking used for read-only operations
    /// Automatic predicate conversion maintains layer isolation
    /// </remarks>
    public virtual async Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> domainPredicate)
    {
        return await GetAsync(domainPredicate, includes: null);
    }

    /// <summary>
    /// The Code was Inspired by Hans and refactored by Phind AI
    /// Retrieves all domain entities using type-safe domain predicates, working as GetAsync
    /// but returning a list of entities instead of a single entity.
    /// Therefore, to make it optional,
    /// they need to change the parameter to be nullable and provide a default value of null.
    /// However, in C#, you can't have default values for Expression parameters.
    /// So the alternative is to create an overload that doesn't take the
    /// predicate and calls the existing method with a null predicate.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TDomain?>> GetAllAsync(Expression<Func<TDomain?, bool>> domainPredicate)
    {
        return await GetAllAsync(domainPredicate, includes: null);
    }

    /// <summary>
    /// Retrieves a single domain entity using type-safe domain predicates
    /// </summary>
    /// <param name="domainPredicate"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public virtual async Task<TDomain?> GetAsync(
        Expression<Func<TDomain?, bool>> domainPredicate,
        params Expression<Func<TDomain, object>>[]? includes
    )
    {
        // Convert domain predicate to entity predicate
        var entityPredicate = factory.CreateEntityPredicate(domainPredicate);
        // Start building the query
        var query = _dbSet.AsNoTracking();

        // Check if includes are provided
        if (includes != null)
        {
            // Include related entities
            query = includes
                .Select(factory.CreateEntityInclude!)
                .Aggregate(query, (current, entityInclude) => current.Include(entityInclude));
        }

        // Apply the entity predicate and get the first entity
        var entity = await query.FirstOrDefaultAsync(entityPredicate);
        // Return the domain object using the factory
        return entity != null ? factory.ToDomain(entity) : null;
    }

    /// <summary>
    /// Retrieves all domain entities using type-safe domain predicates
    /// The Difference between GetAsync and GetAllAsync is that GetAllAsync returns a list of entities
    /// This code is inspired by Hans and refactored by Phind AI
    /// </summary>
    /// <param name="domainPredicate"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    private async Task<IEnumerable<TDomain>> GetAllAsync(
        Expression<Func<TDomain?, bool>>? domainPredicate = null,
        params Expression<Func<TDomain, object>>[]? includes
    )
    {
        // Convert the domain predicate to an entity predicate
        var entityPredicate = factory.CreateEntityPredicate(domainPredicate);

        // Start building the query
        var query = _dbSet.AsNoTracking();

        // Apply the entity predicate
        query = query.Where(entityPredicate);

        // Check if includes are provided
        if (includes != null)
        {
            // Include related entities
            query = includes
                .Select(factory.CreateEntityInclude!)
                .Aggregate(query, (current, entityInclude) => current.Include(entityInclude));
        }

        // Get all entities that match the predicate
        var entity = await query.ToListAsync();
        // Got Partial help from Phind AI to finish this Line
        return entity.Select(e => factory.ToDomain(e)!);
    }

    /// <summary>
    ///  Updates an existing domain entity in the database
    /// </summary>
    /// <param name="domainEntity"></param>
    /// <returns></returns>
    public virtual Task<TDomain?> UpdateAsync(TDomain? domainEntity)
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
    public virtual Task<TDomain?> DeleteAsync(TDomain? domainEntity)
    {
        // Convert the domain object to an entity object
        var entity = factory.ToEntity(domainEntity);

        // Remove the entity from the DbSet
        _dbSet.Remove(entity);

        // Return the domain object
        return Task.FromResult(factory.ToDomain(entity));
    }
}
