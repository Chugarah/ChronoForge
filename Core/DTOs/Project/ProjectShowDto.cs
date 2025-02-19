
using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project.Status;
using Domain;

namespace Core.DTOs.Project;

public class ProjectShowDto
{
    [Required]
    public int Id { get; init; }

    [Required]
    public int StatusId { get; init; }

    [Required]
    public int ProjectManager { get; init; }

    [Required]
    [StringLength(75)]
    public string Title { get; init; } = null!;

    [StringLength(500)]
    public string? Description { get; init; } = null!;

    [Required]
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}