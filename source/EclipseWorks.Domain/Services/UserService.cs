using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        _logger.LogInformation("Geting all users");

        return await _userRepository.GetUsersAsync();
    }
}
