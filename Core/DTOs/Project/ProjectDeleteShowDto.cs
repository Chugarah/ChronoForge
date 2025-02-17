using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project;

public class ProjectDeleteShowDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
}