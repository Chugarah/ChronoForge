using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class RolesConfiguration : IEntityTypeConfiguration<RolesEntity>
{
    public void Configure(EntityTypeBuilder<RolesEntity> builder)
    {
        builder.HasData(
            new RolesEntity { Id = 1, Name = "No Roles" },
            new RolesEntity { Id = 2, Name = "Project Manager" },
            new RolesEntity { Id = 3, Name = "Customer" },
            new RolesEntity { Id = 4, Name = "Client" }
        );
    }
}
