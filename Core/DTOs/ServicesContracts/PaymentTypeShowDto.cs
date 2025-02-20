using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.ServicesContracts;

public class PaymentTypeShowDto
{
    public int Id { get; init; }
    [Required]
    [StringLength(70)]
    public string Name { get; init; } = null!;
    [Required]
    [StringLength(3)]
    public string Currency { get; init; } = null!;
}