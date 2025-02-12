using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Status;

public class StatusDelete
{
    [Required]
    public int Id { get; set; }
}
