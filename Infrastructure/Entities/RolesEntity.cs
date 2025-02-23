using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[Index(nameof(Name), IsUnique = true)]
public class RolesEntity
{
    [Key]
    public int Id { get; init; }
    public virtual ICollection<UsersEntity> UsersEntity { get; set; } = new List<UsersEntity>();

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; init; } = null!;
}