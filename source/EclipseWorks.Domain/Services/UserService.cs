using EclipseWorks.Domain.Exceptions;
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

    public async Task<User> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Obtendo usuário a partir do Id {}", id);

        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            _logger.LogError("Usuário com ID {} não encontrado", id);

            throw new UserException(UserException.UserNotFoundError);
        }

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        _logger.LogInformation("Geting all users");

        return await _userRepository.GetUsersAsync();
    }
}
