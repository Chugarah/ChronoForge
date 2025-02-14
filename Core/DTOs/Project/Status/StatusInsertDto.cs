using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Status;

public class StatusInsertDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; init; } = null!;
}