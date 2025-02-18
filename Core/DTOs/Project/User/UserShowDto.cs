using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.User;

public class UserShowDto
{
    public int Id { get; init; }
    [Required]
    [StringLength(75)]
    public string FirstName { get; init; } = null!;
    [StringLength(75)]
    public string LastName { get; init; } = null!;
}