using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.User;

public class UserUpdateDto(int id)
{
    public int Id { get; } = id;

    [Required]
    [StringLength(75)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(75)]
    public string LastName { get; set; } = null!;
}
