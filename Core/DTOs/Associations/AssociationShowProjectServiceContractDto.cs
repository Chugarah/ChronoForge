using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Associations;

public class AssociationShowProjectServiceContractDto
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Customer id is required")]
    public int CustomerId { get; set; }

    [Range(typeof(int), "1", "999999999")]
    [Required(ErrorMessage = "Payment type id is required")]
    public int PaymentTypeId { get; set; }

    [Range(typeof(int), "1", "999999999")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; init; } = null!;

    [Required(ErrorMessage = "Price is required")]
    [Range(typeof(decimal), "0", "9999999999999999999")]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [DataType(DataType.Currency)]
    public decimal Price { get; init; }
}
