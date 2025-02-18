using Core.DTOs.Project.User;
using Domain;

namespace Core.Interfaces.DTos;

public interface IUserDtoFactory
{
    Users? ToDomainStatusInsert(UserInsertDto createDto);
    UserShowDto? ToDtoStatusDisplay(Users users);
}