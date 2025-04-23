using AutoFixture;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EclipseWorks.DomainServices.Tests;

public class TaskCommentServiceTest
{
    private readonly Mock<ITaskCommentRepository> _taskCommentRepository;
    private readonly Mock<ITaskItemRepository> _taskItemRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ILogger<TaskCommentService>> _logger;
    private readonly TaskCommentService _taskCommentService;
    private readonly IFixture _fixture;

    public TaskCommentServiceTest()
    {
        _taskCommentRepository = new Mock<ITaskCommentRepository>();
        _taskItemRepository = new Mock<ITaskItemRepository>();
        _userRepository = new Mock<IUserRepository>();
        _logger = new Mock<ILogger<TaskCommentService>>();
        _fixture = new Fixture();

        _taskCommentService = new TaskCommentService(
            _taskCommentRepository.Object,
            _taskItemRepository.Object,
            _userRepository.Object,
            _logger.Object);
    }

    [Fact]
    public async Task CreateAsync_Success()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var taskItem = _fixture.Create<TaskItem>();
        var taskComment = _fixture.Build<TaskComment>()
            .With(x => x.UserId, user.Id)
            .With(x => x.TaskItemId, taskItem.Id)
            .Create();

        _userRepository.Setup(x => x.GetByIdAsync(taskComment.UserId)).ReturnsAsync(user);
        _taskItemRepository.Setup(x => x.GetByIdAsync(taskComment.TaskItemId)).ReturnsAsync(taskItem);
        _taskCommentRepository.Setup(x => x.CreateAsync(taskComment)).Returns(Task.CompletedTask);

        // Act
        await _taskCommentService.CreateAsync(taskComment);

        // Assert
        _userRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskCommentRepository.Verify(x => x.CreateAsync(It.IsAny<TaskComment>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_CommentInvalidUserError()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var taskItem = _fixture.Create<TaskItem>();
        var taskComment = _fixture.Build<TaskComment>()
            .With(x => x.UserId, user.Id)
            .With(x => x.TaskItemId, taskItem.Id)
            .Create();

        _userRepository.Setup(x => x.GetByIdAsync(taskComment.UserId)).ReturnsAsync(null as User);
        _taskItemRepository.Setup(x => x.GetByIdAsync(taskComment.TaskItemId)).ReturnsAsync(taskItem);
        _taskCommentRepository.Setup(x => x.CreateAsync(taskComment)).Returns(Task.CompletedTask);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<TaskCommentExceptions>(() => _taskCommentService.CreateAsync(taskComment));

        // Assert
        Assert.Equal(TaskCommentExceptions.CommentInvalidUserError.Message, exception.BusinessError.Message);
        _userRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_CommentInvalidTaskItemError()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var taskItem = _fixture.Create<TaskItem>();
        var taskComment = _fixture.Build<TaskComment>()
            .With(x => x.UserId, user.Id)
            .With(x => x.TaskItemId, taskItem.Id)
            .Create();

        _userRepository.Setup(x => x.GetByIdAsync(taskComment.UserId)).ReturnsAsync(user);
        _taskItemRepository.Setup(x => x.GetByIdAsync(taskComment.TaskItemId)).ReturnsAsync(null as TaskItem);
        _taskCommentRepository.Setup(x => x.CreateAsync(taskComment)).Returns(Task.CompletedTask);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<TaskCommentExceptions>(() => _taskCommentService.CreateAsync(taskComment));

        // Assert
        Assert.Equal(TaskCommentExceptions.CommentInvalidTaskItemError.Message, exception.BusinessError.Message);
        _userRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskCommentRepository.Verify(x => x.CreateAsync(It.IsAny<TaskComment>()), Times.Never);
    }
}
