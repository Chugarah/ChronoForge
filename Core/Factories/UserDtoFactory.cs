using Core.DTOs.Project.User;
using Core.Interfaces.DTos;
using Domain;

namespace Core.Factories;

public class UserDtoFactory : IUserDtoFactory
{
    // Creating from Domain object to Create a DTO object
    public Users? ToDomainStatusInsert(UserInsertDto createDto) =>
        new() { FirstName = createDto.FirstName, LastName = createDto.LastName };

    // Creating from Domain object to Display a DTO object
    public UserShowDto? ToDtoStatusDisplay(Users users) =>
        new()
        {
            Id = users.Id,
            FirstName = users.FirstName,
            LastName = users.LastName,
        };
}
