using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
}
