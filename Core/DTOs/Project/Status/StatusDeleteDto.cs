using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Status;

public class StatusDeleteDto
{
    [Required]
    public int Id { get; set; }
}
