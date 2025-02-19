using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project.Status;

namespace Core.DTOs.Project;

public class ProjectInsertDto
{
    [Required]
    public int StatusId { get; set; }

    [Required]
    public int ProjectManager { get; set; }

    [Required]
    [StringLength(75)]
    public string Title { get; set; } = null!;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;


    // This is only valid if used in View connected to the domain
    [Required(ErrorMessage = "You need to provide this format YYYY-MM-DD")]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}