using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class UsersConfiguration : IEntityTypeConfiguration<UsersEntity>
{
    public void Configure(EntityTypeBuilder<UsersEntity> builder)
    {
        builder.HasData(
            new UsersEntity
            {
                Id = 1,
                FirstName = "No User is set",
                LastName = "Starcraft",
            }
        );
    }
}
