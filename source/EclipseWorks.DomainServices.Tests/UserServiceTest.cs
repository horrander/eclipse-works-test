using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EclipseWorks.DomainServices.Tests;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    private readonly IUserService _userService;

    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<UserService>>();

        _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllUserAsync_Success()
    {
        //Arrange
        _userRepositoryMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(new List<User>().AsEnumerable);

        //Act
        await _userService.GetUsersAsync();

        //Assert
        _userRepositoryMock.Verify(x => x.GetUsersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        //Arrange
        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User());

        //Act
        await _userService.GetByIdAsync(Guid.NewGuid());

        //Assert
        _userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_UserNotFoundError()
    {
        //Arrange
        _userRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as User);

        //Act
        BusinessException exception = await Assert.ThrowsAsync<UserException>(() => _userService.GetByIdAsync(Guid.NewGuid()));

        Assert.Equal(UserException.UserNotFoundError.Message, exception.BusinessError.Message);

        //Assert
        _userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
}
