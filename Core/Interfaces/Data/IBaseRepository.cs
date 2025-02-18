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

    Task<TDomain?> GetAsync(Expression<Func<TDomain?, bool>> domainPredicate);

    Task<IEnumerable<TDomain?>> GetAllAsync(Expression<Func<TDomain?, bool>> domainPredicate);

    Task<TDomain?> DeleteAsync(TDomain? domainEntity);
}
