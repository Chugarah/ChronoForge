using API.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

/// <summary>
/// Common helpers
/// </summary>
public class CommonHelpers : ICommonHelpers
{

    /// <summary>
    ///  Check if the error is a duplicate key error
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public bool IsDuplicateKeyError(DbUpdateException ex)
    {
        // Check if the exception is a SqlException and if the error number is 2601 or 2627
        // Why this numbers? Because they are the error numbers for duplicate key errors
        // in Entity Framework Core
        return ex.InnerException is SqlException sqlEx
               && (sqlEx.Number == 2601 || sqlEx.Number == 2627);
    }
}