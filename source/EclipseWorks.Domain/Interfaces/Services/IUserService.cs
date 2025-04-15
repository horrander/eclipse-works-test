using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface IUserService
{
    IEnumerable<User> GetUsers();
}
