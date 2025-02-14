
using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project.Status;

namespace Core.DTOs.Project;

public class ProjectShowDto
{
    [Required]
    public int Id { get; init; }

    [Required]
    [StringLength(75)]
    public string Title { get; init; } = null!;

    public StatusDisplayDto Status { get; set; } = null!;

    [Required]
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}