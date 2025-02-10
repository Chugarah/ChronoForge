using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class PaymentTypesConfiguration : IEntityTypeConfiguration<PaymentTypeEntity>
{
    public void Configure(EntityTypeBuilder<PaymentTypeEntity> builder)
    {
        builder.HasData(
            new PaymentTypeEntity {Id = 1 ,Name = "No Roles", Currency = "None"!},
            new PaymentTypeEntity {Id = 2 ,Name = "Hourly Rate", Currency = "SEK"},
            new PaymentTypeEntity {Id = 3 ,Name = "Per Project", Currency = "SEK"},
            new PaymentTypeEntity {Id = 4 ,Name = "Other Type", Currency = "SEK"}
        );
    }
}