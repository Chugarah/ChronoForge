using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[Index(nameof(Name), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id {get; init;}

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name {get ; init;} = null!;

    public ICollection<ProjectsEntity> ProjectsEntity { get; set; } = [];
}