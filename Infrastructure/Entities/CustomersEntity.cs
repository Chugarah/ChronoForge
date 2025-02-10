using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[Index(nameof(Name), IsUnique = true)]
public class CustomersEntity
{
    [Key]
    public int Id { get; init; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int ContactPersonId { get; init; }
    [ForeignKey(nameof(ContactPersonId))]
    public UsersEntity UsersEntity { get; init; } = null!;
}