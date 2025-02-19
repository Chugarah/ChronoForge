using Core.DTOs.Project.Status;
using Domain;

namespace Core.Interfaces.DTos;

public interface IStatusDtoFactory
{
    Status? ToDomainStatusInsert(StatusInsertDto createDto);
    StatusDisplayDto? ToDtoStatusDisplay(Status? status);
}