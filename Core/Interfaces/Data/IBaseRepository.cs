﻿using System.Linq.Expressions;

namespace Core.Interfaces.Data;

/// <summary>
///  This interface is used to create a generic repository
///  that will be used by all other repositories
/// </summary>
/// <typeparam name="TDomain"></typeparam>
public interface IBaseRepository<TDomain>
    where TDomain : class
{
    Task<TDomain?> CreateAsync(TDomain? domainEntity);
    Task<TDomain?> UpdateAsync(TDomain? domainEntity);
    Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> predicate);
    Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> predicate, params Expression<Func<TDomain, object>>[]? includes);
    Task<TDomain?> DeleteAsync(TDomain? domainEntity);
}