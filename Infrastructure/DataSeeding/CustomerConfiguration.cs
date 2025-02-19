using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomersEntity>
{
    public void Configure(EntityTypeBuilder<CustomersEntity> builder)
    {
        builder.HasData(
            new CustomersEntity
            {
                Id = 1,
                Name = "No Contact Person",
                ContactPersonId = 1,
            }
        );
    }
}