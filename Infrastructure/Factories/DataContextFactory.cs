using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Factories;

/// <summary>
/// Factory class for creating DataContext instances at design time.
/// This is used by Entity Framework Core tools for migrations and database updates.
/// Inspired by Hans's tutorial: https://www.youtube.com/watch?v=L_F6mNq2LSM
/// </summary>
public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    /// <summary>
    /// Creates a new instance of DataContext.
    /// This method is called by EF Core tools during design-time operations like adding migrations or updating database.
    /// https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    /// </summary>
    /// <param name="args">Command line arguments (not used in this implementation)</param>
    /// <returns>A new instance of DataContext configured with the application's database connection</returns>
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        // Used ChatGPT to create the connection string
        optionsBuilder.UseSqlServer("Server=localhost;Database=Chrono_Forge;Trusted_Connection=True;TrustServerCertificate=True;");
        return new DataContext(optionsBuilder.Options);
    }
}