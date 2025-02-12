using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project;

public class StatusInsert
{
    [Required]
    [StringLength(50)]
    public string Name { get; init; } = null!;
}