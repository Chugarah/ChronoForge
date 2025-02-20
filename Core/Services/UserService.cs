using System.Data.Common;
using Core.DTOs.Project.User;
using Core.Interfaces.Data;
using Core.Interfaces.DTos;
using Core.Interfaces.Project;
using Core.Interfaces.ServiceContractsI;
using Domain;

namespace Core.Services;

public class UserService(
    IUserDtoFactory userDtoFactory,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IUserService
{
    /// <summary>
    /// Create a new user in the database
    /// </summary>
    /// <param name="userInsertDto"></param>
    /// <returns></returns>
    public async Task<UserShowDto?> CreateUserAsync(UserInsertDto userInsertDto)
    {
        try
        {
            // Convert the DTO to a domain object
            var userInsert = userDtoFactory.ToDomainStatusInsert(userInsertDto);

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Create the project in the database
            await userRepository.CreateAsync(userInsert);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the created project as a display DTO
            return userDtoFactory.ToDtoStatusDisplay(userInsert);
        }
        catch (Exception)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    ///  Get a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Single User</returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserShowDto?> GetUserByIdAsync(int id)
    {
        try
        {
            // Get the project from the database
            var user = await userRepository.GetAsync(u => u != null && u.Id == id);

            // Convert the project to a display DTO
            return user != null ? userDtoFactory.ToDtoStatusDisplay(user) : null;
        }
        catch (DbException ex)
        {
            // Throw an exception with a message
            throw new Exception("Could not get the User from the database:", ex);
        }
    }

    /// <summary>
    /// Update a user in the database
    /// </summary>
    /// <param name="userUpdateDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserShowDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        try
        {
            // Get the user from the database
            var user =
                await userRepository.GetAsync(p => p!.Id == userUpdateDto.Id)
                ?? throw new Exception("Could not find the user in the database");

            // Update the user with the new values
            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;

            #region BEGIN TRANSACTION

            // Begin Transaction to ensure that all operations are successful
            await unitOfWork.BeginTransactionAsync();

            // Update the user in the database
            await userRepository.UpdateAsync(user);

            // Save the changes to the database
            await unitOfWork.SaveChangesAsync();

            // Commit the transaction to ensure that all operations are successful
            await unitOfWork.CommitTransactionAsync();

            #endregion END TRANSACTION

            // Return the updated user as a display DTO
            return userDtoFactory.ToDtoStatusDisplay(user)!;
        }
        catch (DbException ex)
        {
            // Rollback the transaction if an error occurs
            await unitOfWork.RollbackTransactionAsync();

            // Throw an exception with a message
            throw new Exception("Could not update the user in the database:", ex);
        }
    }
}
