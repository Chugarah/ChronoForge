using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PaymentTypeEntity
{
    [Key]
    public int Id { get; init; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; } = null!;

    [Required]
    [Column(TypeName = "char(10)")]
    public string Currency { get; init; } = null!;
}