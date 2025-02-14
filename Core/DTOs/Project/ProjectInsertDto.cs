using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project.Status;

namespace Core.DTOs.Project;

public class ProjectInsertDto
{
    [Required]
    public int StatusId { get; set; }

    [Required]
    public int ProjectManager { get; init; }

    [Required]
    [StringLength(75)]
    public string Title { get; init; } = null!;
    [Required(ErrorMessage = "You need to provide this format YYYY-MM-DD")]
    public DateOnly StartDate { get; init; }
    [Required(ErrorMessage = "You need to provide this format YYYY-MM-DD")]
    public DateOnly EndDate { get; init; }
}