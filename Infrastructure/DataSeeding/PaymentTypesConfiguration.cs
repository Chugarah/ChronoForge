using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataSeeding;

public class PaymentTypesConfiguration : IEntityTypeConfiguration<PaymentType>
{
    public void Configure(EntityTypeBuilder<PaymentType> builder)
    {
        builder.HasData(
            new PaymentType {Id = 1 ,Name = "No Roles", Currency = "None"!},
            new PaymentType {Id = 2 ,Name = "Hourly Rate", Currency = "SEK"},
            new PaymentType {Id = 3 ,Name = "Per Project", Currency = "SEK"},
            new PaymentType {Id = 4 ,Name = "Other Type", Currency = "SEK"}
        );
    }
}