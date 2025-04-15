using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public UserService(IUserRepository userRepository, ILogger logger)
    {
        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));

        _logger = logger ??
            throw new ArgumentNullException(nameof(_logger));
    }

    public IEnumerable<User> GetUsers()
    {
        _logger.LogInformation("Geting all users");

        return _userRepository.GetUsers();
    }
}
