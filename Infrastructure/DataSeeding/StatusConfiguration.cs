using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

/// <summary>
///  This class is used to seed the Status table in the database
///  The Status table is used to store the status of the project
///  This is partially created by AI Phind
/// </summary>
public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData(
            new Status { Id = 1, Name= "No Status" },
            new Status { Id = 2, Name = "Not Started" },
            new Status { Id = 3, Name = "In Progress" },
            new Status { Id = 4, Name = "Completed" }
        );
    }
}