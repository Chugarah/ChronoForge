using Core.DTOs.Project.Status;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class StatusDtoFactory : IStatusDtoFactory
{
    // Creating from Domain object to Create a DTO object
    public Status? ToDomainStatusInsert(StatusInsertDto createDto) => new() { Name = createDto.Name };

    // Creating from Domain object to Display a DTO object
    public StatusDisplayDto? ToDtoStatusDisplay(Status? status) =>
        new() { Id = status!.Id, Name = status.Name };
}
