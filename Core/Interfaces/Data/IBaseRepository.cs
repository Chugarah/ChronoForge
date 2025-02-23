using System.Linq.Expressions;

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

    Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> domainPredicate,
        bool tracking,
        params Expression<Func<TDomain, object>>[]? includes);

    Task<IEnumerable<TDomain?>> GetAllAsync(Expression<Func<TDomain?, bool>> domainPredicate,
        bool tracking = false,
        params Expression<Func<TDomain, object>>[]? includes);

    Task<TDomain?> DeleteAsync(TDomain? domainEntity);

    Task<TDomain?> AttachAsync(TDomain? domainEntity);


    // Section to check for data and return of exist
    Task<bool> AnyAsync(Expression<Func<TDomain?, bool>> domainPredicate);
    Task<TDomain?> GetIfExistsAsync(Expression<Func<TDomain?, bool>> domainPredicate);
}
