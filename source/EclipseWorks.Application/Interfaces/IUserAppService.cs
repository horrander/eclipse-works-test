using EclipseWorks.Domain.Models;

namespace EclipseWorks.Application.Interfaces;

public interface IUserAppService
{
    Task<IEnumerable<User>> GetUsersAsync();
}
