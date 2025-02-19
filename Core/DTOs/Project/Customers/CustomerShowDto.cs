using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Customers;

public class CustomerShowDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int ContactPersonId { get; set; }
}