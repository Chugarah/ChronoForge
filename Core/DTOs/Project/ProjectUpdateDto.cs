using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project;

public class ProjectUpdateDto(int id)
{
    // Decided to initialize the ID in the constructor
    // to avoid touching the ID in the update method
    public int Id { get;} = id;

    [Required]
    public int StatusId { get; set; }
    [Required]
    public int ProjectManager { get; set; }
    [Required]
    [StringLength(75)]
    public string Title { get; set; } = null!;
    [StringLength(500)]
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}