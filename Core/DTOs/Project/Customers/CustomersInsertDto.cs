using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project.Customers;

public class CustomersInsertDto()
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int ContactPersonId { get; set; }
}