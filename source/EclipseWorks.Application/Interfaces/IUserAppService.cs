using EclipseWorks.Application.Dtos.Response;

namespace EclipseWorks.Application.Interfaces;

public interface IUserAppService
{
    Task<IEnumerable<UserResponse>> GetUsersAsync();
}
