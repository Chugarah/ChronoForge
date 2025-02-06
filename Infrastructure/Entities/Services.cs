using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Index(nameof(CustomerId),
    nameof(PaymentTypeId),
    nameof(Name),
    IsUnique = true)]
public class Services
{
    [Key]
    public int Id { get; init; }

    // Navigation properties for EF Core relationships
    public int CustomerId { get; init; }
    [ForeignKey(nameof(CustomerId))]
    public Customers Customers { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int PaymentTypeId { get; init; }
    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentType PaymentType { get; init; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Name { get; init; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; init; }


}