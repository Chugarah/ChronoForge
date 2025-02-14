using Microsoft.EntityFrameworkCore;

namespace API.Interfaces;

/// <summary>
/// Common helpers
/// </summary>
public interface ICommonHelpers
{
    /// <summary>
    /// Check if the error is a duplicate key error
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    bool IsDuplicateKeyError(DbUpdateException ex);
}