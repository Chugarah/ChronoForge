using System.ComponentModel.DataAnnotations;
using Domain;

namespace Core.DTOs.ServicesContracts;

public class ServiceContractsInsertDto
{
    // Adding a collection of Projects to the ServiceContractsInsertDtos
    public ICollection<Projects?> Projects { get; set; } = new List<Projects?>();

    [Required]
    public int CustomerId { get; init; }
    [Required]
    public int PaymentTypeId { get; init; }

    [StringLength(100)]
    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; init; } = null!;

    // The Range attribute is used to specify the numeric range that a property or
    // parameter value should fall within
    [Range(typeof(decimal), "0", "9999999999999999999")]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [DataType(DataType.Currency)]
    public decimal Price { get; init; }
}