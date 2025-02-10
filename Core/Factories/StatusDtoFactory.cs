using Core.DTOs.Project;
using Core.Interfaces.Project;
using Domain;

namespace Core.Factories;

public class StatusDtoFactory : IStatusDtoFactory
{
    // Creating from Domain object to Create a DTO object
    public Status ToDomain(StatusInsert createDto) => new() { Name = createDto.Name };

    // Creating from Domain object to Create a DTO object
    public StatusInsert ToCreateDto(Status status) => new() { Name = status.Name };

    // Creating from Domain object to Display a DTO object
    public StatusDisplay ToDisplayDto(Status status) =>
        new() { Id = status.Id, Name = status.Name };
}
