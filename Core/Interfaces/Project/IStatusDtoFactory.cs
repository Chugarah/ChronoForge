using Core.DTOs.Project;
using Domain;

namespace Core.Interfaces.Project;

public interface IStatusDtoFactory
{
    Status ToDomain(StatusInsert createDto);
    StatusInsert ToCreateDto(Status status);
    StatusDisplay ToDisplayDto(Status status);
}