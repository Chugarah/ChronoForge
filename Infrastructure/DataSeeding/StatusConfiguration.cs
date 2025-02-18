using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

/// <summary>
///  This class is used to seed the Status table in the database
///  The Status table is used to store the status of the project
///  This is partially created by AI Phind
/// </summary>
public class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.HasData(
            new StatusEntity { Id = 1, Name = "No Status" },
            new StatusEntity { Id = 2, Name = "Not Started" },
            new StatusEntity { Id = 3, Name = "In Progress" },
            new StatusEntity { Id = 4, Name = "Completed" }
        );
    }
}
