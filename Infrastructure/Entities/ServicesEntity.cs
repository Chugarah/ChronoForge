using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Index(nameof(CustomerId),
    nameof(PaymentTypeId),
    nameof(Name),
    IsUnique = true)]
public sealed class ServicesEntity
{
    [Key]
    public int Id { get; init; }
    // Navigation properties for EF Core relationships
    public ICollection<ProjectsEntity> ProjectsEntity { get; init; } = new List<ProjectsEntity>();

    public int CustomerId { get; init; }
    [ForeignKey(nameof(CustomerId))]
    public CustomersEntity CustomersEntity { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int PaymentTypeId { get; init; }
    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentTypeEntity PaymentTypeEntity { get; init; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Name { get; init; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; init; }


}