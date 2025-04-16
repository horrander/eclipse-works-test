using AutoMapper;
using EclipseWorks.Application.Interfaces;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Application.ApplicationServices;

public class UserAppService : IUserAppService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserAppService> _logger;

    public UserAppService(IUserService userService,
        IMapper mapper,
        ILogger<UserAppService> logger)
    {
        _userService = userService ??
            throw new ArgumentNullException(nameof(userService));

        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        try
        {
            var users = await _userService.GetUsersAsync();

            var usersResponse = _mapper.Map<IEnumerable<User>>(users);

            return usersResponse;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
