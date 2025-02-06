using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[Index(nameof(Name), IsUnique = true)]
public class Roles
{
    [Key]
    public int Id { get; init; }
    public ICollection<Users> Users { get; init; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; init; } = null!;
}