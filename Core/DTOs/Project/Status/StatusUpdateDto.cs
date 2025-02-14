using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Status;

public class StatusUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;
}