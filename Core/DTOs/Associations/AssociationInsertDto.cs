using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Associations;

public class AssociationInsertDto
{
    // https://stackoverflow.com/questions/3345348/how-to-specify-a-min-but-no-max-decimal-using-the-range-data-annotation-attribut
    [Required (ErrorMessage = "Project ID is required")]
    [Range (1, int.MaxValue, ErrorMessage = "Project ID must be an integer and greater than 0")]
    public int ProjectId { get; set; }
    [Required (ErrorMessage = "Service Contract ID is required")]
    [Range (1, int.MaxValue, ErrorMessage = "Service Contract ID must be an integer and greater than 0")]
    public int ServiceContractId { get; set; }
}