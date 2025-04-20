using AutoMapper;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using EclipseWorks.Domain.Interfaces.Services;
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

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        var users = await _userService.GetUsersAsync();

        var usersResponse = _mapper.Map<IEnumerable<UserResponse>>(users);

        return usersResponse;
    }
}
