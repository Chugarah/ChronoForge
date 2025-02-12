using System.Linq.Expressions;

namespace Core.Interfaces.Data;

public interface IEntityFactory<TDomain, TEntity>
{
    // Convert a domain predicate to an entity predicate
    // Used AI Phind to complete this line
    Expression<Func<TEntity, bool>> CreateEntityPredicate(Expression<Func<TDomain, bool>> domainPredicate);

    TDomain ToDomain(TEntity entity);
    TEntity ToEntity(TDomain domain);
}
