using Core.DTOs.Project.User;

namespace Core.Interfaces.ServiceContractsI;

public interface IUserService
{
    Task<UserShowDto?> CreateUserAsync(UserInsertDto userInsertDto);
    Task<UserShowDto?> GetUserByIdAsync(int id);
    Task<UserShowDto> UpdateUserAsync(UserUpdateDto userUpdateDto);
}
