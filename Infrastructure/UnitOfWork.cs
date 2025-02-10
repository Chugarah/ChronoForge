using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

/// <summary>
/// This class is used to create a unit of work
/// Inspired by https://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/
/// https://stackoverflow.com/questions/54671253/registering-iunitofwork-as-service-in-net-core
/// </summary>
public class UnitOfWork(DataContext dataContext, IDbContextTransaction transaction) : IUnitOfWork
{
    private IDbContextTransaction _transaction = transaction;

    /// <summary>
    /// This method is used to begin a transaction
    /// </summary>
    public async Task BeginTransactionAsync()
    {
        // Begin the transaction and store it in the _transaction variable
        _transaction = await dataContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// This method is used to commit the transaction
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        // Commit the transaction
        await _transaction.CommitAsync();
    }

    /// <summary>
    /// This method is used to roll back the transaction
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        // Rollback the transaction
        await _transaction.RollbackAsync();
    }

    /// <summary>
    /// This method is used to save changes to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Save changes to the database
       return await dataContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// This method is used to dispose the transaction and the data context
    /// </summary>
    public void Dispose()
    {
        // Dispose the transaction if it exists
        _transaction?.Dispose();

        // Dispose the data context
        dataContext?.Dispose();

        // Suppress finalization.
        // We don't need the destructor to run after
        // we've manually disposed of the resources.
        GC.SuppressFinalize(this);
    }
}
