using System.ComponentModel.DataAnnotations;
using Domain;

namespace Core.DTOs.Project.User;

public class UserInsertDto
{
    [Required]
    [StringLength(75)]
    public string FirstName { get; init; } = null!;
    [StringLength(75)]
    public string LastName { get; init; } = null!;
}