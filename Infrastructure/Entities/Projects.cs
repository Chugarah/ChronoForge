using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;
// This class represents the Projects table in the database
// adding the Index attribute to the class to make sure that the combination of ProjectManager and Title is unique
[Index(nameof(ProjectManager), nameof(Title), IsUnique = true)]
public class Projects
{
    [Key]
    public int Id { get; init; }
    // Navigation properties for EF Core relationships
    public int StatusId { get; init; }
    [ForeignKey(nameof(StatusId))]
    public Status Status { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int ProjectManager { get; init; }
    [ForeignKey(nameof(ProjectManager))]
    public Users Users { get; init; } = null!;

    [Column(TypeName = "nvarchar(75)")]
    public string Title { get; init; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }



}