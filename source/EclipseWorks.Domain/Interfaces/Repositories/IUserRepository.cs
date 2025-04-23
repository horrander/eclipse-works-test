using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get all non-removed Users
    /// </summary>
    /// <returns>List of user</returns> 
    Task<IEnumerable<User>> GetUsersAsync();

    /// <summary>
    /// Get a user by Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>User</returns>
    Task<User?> GetByIdAsync(Guid id);
}
