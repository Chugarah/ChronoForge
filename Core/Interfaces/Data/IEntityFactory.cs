namespace Core.Interfaces.Data;

public interface IEntityFactory<TDomain, TEntity>
{
    TDomain ToDomain(TEntity entity);
    TEntity ToEntity(TDomain domain);
}
