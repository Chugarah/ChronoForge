using Core.Interfaces;
using Core.Interfaces.Data;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

/// <summary>
/// Using AI Phind to create Summary
/// Coordinates database operations and transactions across multiple repositories
/// </summary>
/// <remarks>
/// Implements Unit of Work pattern to:
/// <para>- Manage transaction lifecycle</para>
/// <para>- Ensure atomic operations across repositories</para>
/// <para>- Centralize database change tracking</para>
/// </remarks>
/// <param name="dataContext">Entity Framework Core database context</param>
/// <param name="transaction">Initial transaction state</param>
/// <seealso cref="https://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/"/>
/// <seealso cref="https://stackoverflow.com/questions/54671253/registering-iunitofwork-as-service-in-net-core"/>
public class UnitOfWork(DataContext dataContext, IDbContextTransaction transaction) : IUnitOfWork
{
    private IDbContextTransaction _transaction = transaction;

    /// <summary>
    /// Initiates a new database transaction
    /// </summary>
    /// <remarks>
    /// Creates an explicit transaction if none exists. Subsequent calls will
    /// reuse the existing transaction scope until commit/rollback.
    /// </remarks>
    public async Task BeginTransactionAsync()
    {
        // Begin the transaction and store it in the _transaction variable
        _transaction = await dataContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commits all changes made within the transaction scope
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when no active transaction exists
    /// </exception>
    public async Task CommitTransactionAsync()
    {
        // Commit the transaction
        await _transaction.CommitAsync();
    }

    /// <summary>
    /// Aborts the current transaction and discards pending changes
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        // Rollback the transaction
        await _transaction.RollbackAsync();
    }

    /// <summary>
    /// Persists all pending changes to the database
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Number of affected records</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Save changes to the database
       return await dataContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Releases managed database resources
    /// </summary>
    /// <remarks>
    /// Disposes both the active transaction (if any) and the DbContext.
    /// Should typically be called at the end of a request lifecycle.
    /// </remarks>
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
