using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();

    /// <summary>
    /// Get a user by Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>User</returns>
    Task<User> GetByIdAsync(Guid id);
}
