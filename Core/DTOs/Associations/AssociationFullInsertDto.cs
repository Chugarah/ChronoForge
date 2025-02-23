using System.ComponentModel.DataAnnotations;
using Core.DTOs.Project;
using Core.DTOs.ServicesContracts;

namespace Core.DTOs.Associations;

public class AssociationFullInsertDto
{
    [Required (ErrorMessage = "You need to provide a project data")]
    public ProjectInsertFormDto ProjectInsertFormDto { get; set; } = null!;

    public ServiceContractsInsertDto ServiceContractsInsertDto { get; set; } = null!;
}