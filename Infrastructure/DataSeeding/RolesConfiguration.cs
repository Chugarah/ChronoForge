using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.HasData(
            new Roles { Id = 1, Name= "No Roles" },
            new Roles { Id = 2, Name = "Project Manager" },
            new Roles { Id = 3, Name = "Customer" },
            new Roles { Id = 4, Name = "Client" }
        );
    }
}