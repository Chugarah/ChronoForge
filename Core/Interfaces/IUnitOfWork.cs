namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}