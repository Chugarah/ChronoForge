using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Core.DTOs.Associations;

public class AssociationResultDto
{
    [Required]
    public bool Success { get; set; }
    [Required]
    public string Message { get; set; } = null!;
    [Required]
    public HttpStatusCode StatusCode { get; set; }
}