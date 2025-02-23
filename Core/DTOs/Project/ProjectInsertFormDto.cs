using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project.Status;
using Domain;

namespace Core.DTOs.Project;

public class ProjectInsertFormDto ()
{
    [Required]
    public int StatusId { get; set; }

    public ICollection<ServiceContracts> ServiceContracts { get; set; } = [];

    [Required]
    public int ProjectManager { get; set; }

    [Required]
    [StringLength(75)]
    public string Title { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; } = string.Empty;


    // This is only valid if used in View connected to the domain
    [Required(ErrorMessage = "You need to provide this format YYYY-MM-DD")]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}