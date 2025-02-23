using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;
// This class represents the Projects table in the database
// adding the Index attribute to the class to make sure that the combination of ProjectManager and Title is unique
[Index(nameof(ProjectManager), nameof(Title), IsUnique = true)]
public class ProjectsEntity
{
    [Key]
    public int Id { get; init; }
    public virtual List<ServiceContractsEntity> ServiceContractsEntity { get; set; } = [];

    public int StatusId { get; init; }
    [ForeignKey(nameof(StatusId))]
    public virtual StatusEntity StatusEntity { get; init; } = null!;

    // Navigation properties for EF Core relationships
    public int ProjectManager { get; init; }
    [ForeignKey(nameof(ProjectManager))]
    public virtual UsersEntity UsersEntity { get; init; } = null!;

    [Column(TypeName = "nvarchar(75)")]
    public string Title { get; init; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }



}