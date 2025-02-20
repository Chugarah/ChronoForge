using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

// This class is used to create the Users table in the database
// adding the Index attribute to the class to make sure that the combination of FirstName and LastName is unique
[Index(nameof(FirstName), nameof(LastName), IsUnique = true)]
public sealed class UsersEntity
{
    [Key]
    public int Id { get; init; }
    public ICollection<RolesEntity> RolesEntity { get; set; } = new List<RolesEntity>();

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; init; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; init; } = null!;
}