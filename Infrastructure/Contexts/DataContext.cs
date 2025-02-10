﻿using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

/// <summary>
/// Inspired by Hans :) Thanks
/// </summary>
/// <param name="options"></param>
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    /*
        # Navigate to your Infrastructure project directory
        cd Infrastructure

        # Add migration
        dotnet ef migrations add InitialMigration

        # Update database
        dotnet ef database update
     */

    public DbSet<StatusEntity> Status { get; set; }
    public DbSet<RolesEntity> Roles { get; set; }
    public DbSet<UsersEntity> Users { get; set; }
    public DbSet<ProjectsEntity> Projects { get; set; }
    public DbSet<CustomersEntity> Customers { get; set; }
    public DbSet<PaymentTypeEntity> PaymentType { get; set; }
    public DbSet<ServicesEntity> Services { get; set; }

    // Here is where we define our tables
    // Specify Connection String in the app settings.json, if using multiple presentation
    // layers, you can specify the connection string in the appsettings.json of the presentation layer
    // We could activate the lazy loading here as well
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Using Logs this to log sensitive data for developers
        optionsBuilder.EnableSensitiveDataLogging();
        // We can also enable caching here
        optionsBuilder.EnableServiceProviderCaching();
    }

    /// <summary>
    /// This is needed if we have multiple keys in a table to add the composite key (Two Primary Keys)
    /// Then this is needed to be added to the OnModelCreating. What should I do without Hans? :)
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<TableName>().HasKey(x => new { x.Key1, x.Key2 });
        // If we want to make a column unique but could also be set in our Entity as well and not just here
        // modelBuilder.Entity<TableName>().HasIndex(x => x.Key1).IsUnique();

        // Composite Key for Customers and a unique constraint for Name and ContactPersonId
        modelBuilder.Entity<CustomersEntity>(entity =>
        {
            // Composite Key for Customers ID
            entity.HasKey(e => new { e.Id });
            // Adding a unique constraint to the Name and ContactPersonId
            entity
                .HasIndex(e => new { e.Name, e.ContactPersonId })
                .IsUnique()
                .HasDatabaseName("IX_Customers_Name_ContactPersonId");
        });

        // Unique constraint for the Firstname and LastName in the Roles table
        //    modelBuilder.Entity<Users>(entity =>
        //    {
        //        entity
        //            .HasIndex(u => new { u.FirstName, u.LastName })
        //            .IsUnique()
        //            .HasDatabaseName("IX_Users_FirstName_LastName");
        //    });

        // Inspired by Robin
        // Setting the Identity column to start at 100 and increment by 1
        modelBuilder.Entity<ProjectsEntity>().Property(p => p.Id).UseIdentityColumn(100, 1);

        // Loading the configurations from the DataSeeding folder to seed the database
        // Inspired by Robin and Partially created by AI Phind
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
