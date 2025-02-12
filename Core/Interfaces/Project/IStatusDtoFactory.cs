using Core.DTOs.Project;
using Core.DTOs.Project.Status;
using Domain;

namespace Core.Interfaces.Project;

public interface IStatusDtoFactory
{
    Status? ToDomain(StatusInsert createDto);
    StatusInsert ToCreateDto(Status status);
    StatusDisplay? ToDisplayDto(Status status);
}