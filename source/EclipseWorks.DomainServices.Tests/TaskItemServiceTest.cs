using AutoFixture;
using EclipseWorks.Domain.Enuns;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EclipseWorks.DomainServices.Tests;

public class TaskItemServiceTest
{
    private readonly Mock<ITaskItemRepository> _taskItemRepositoryMock;
    private readonly Mock<ILogger<TaskItemService>> _loggerMock;
    private readonly ITaskItemService _taskItemService;
    private readonly IFixture _fixture;

    public TaskItemServiceTest()
    {
        _taskItemRepositoryMock = new Mock<ITaskItemRepository>();
        _loggerMock = new Mock<ILogger<TaskItemService>>();
        _fixture = new Fixture();

        _taskItemService = new TaskItemService(_taskItemRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Success()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync(taskItem);

        // Act
        await _taskItemService.CreateAsync(project, taskItem);

        // Assert
        _taskItemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_TaskItemInvalidDueDateError()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(-30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync(taskItem);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<TaskItemExceptions>(() => _taskItemService.CreateAsync(project, taskItem));

        // Assert
        Assert.Equal(TaskItemExceptions.TaskItemInvalidDueDateError.Message, exception.BusinessError.Message);
        _taskItemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_TaskItemInvalidProjectError()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, Guid.Empty)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync(taskItem);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<TaskItemExceptions>(() => _taskItemService.CreateAsync(project, taskItem));

        // Assert
        Assert.Equal(TaskItemExceptions.TaskItemInvalidProjectError.Message, exception.BusinessError.Message);
        _taskItemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_ProjectToManyTasksError()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        var taskItens = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .CreateMany(21);

        project.Tasks = taskItens.ToList();

        _taskItemRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync(taskItem);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _taskItemService.CreateAsync(project, taskItem));

        // Assert
        Assert.Equal(ProjectExceptions.ProjectToManyTasksError.Message, exception.BusinessError.Message);
        _taskItemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public async Task GetById_Success()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(taskItem);

        // Act
        await _taskItemService.GetById(It.IsAny<Guid>());

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetById_TaskItemNotFoundError()
    {
        // Arrange
        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as TaskItem);

        // Act
        BusinessException exception = await Assert.ThrowsAsync<TaskItemExceptions>(() => _taskItemService.GetById(It.IsAny<Guid>()));

        // Assert
        Assert.Equal(TaskItemExceptions.TaskItemNotFoundError.Message, exception.BusinessError.Message);
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByProjectIdAsync_Success()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItens = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(10))
            .With(x => x.ProjectId, project.Id)
            .CreateMany(10)
            .ToList();

        _taskItemRepositoryMock.Setup(x => x.GetByProjectIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(taskItens);

        // Act
        await _taskItemService.GetByProjectIdAsync(It.IsAny<Guid>());

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByProjectIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByProjectIdAsync_Success_TasksEmpty()
    {
        // Arrange
        _taskItemRepositoryMock.Setup(x => x.GetByProjectIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        // Act
        await _taskItemService.GetByProjectIdAsync(It.IsAny<Guid>());

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByProjectIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task RemoveFromProjectAsync_Success()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItens = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(10))
            .With(x => x.ProjectId, project.Id)
            .CreateMany(10)
            .ToList();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.RemoveFromProjectAsync(taskItem))
            .Returns(Task.CompletedTask);

        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(taskItem);

        // Act
        await _taskItemService.RemoveFromProjectAsync(It.IsAny<Guid>());

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepositoryMock.Verify(x => x.RemoveFromProjectAsync(taskItem), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(taskItem);

        _taskItemRepositoryMock.Setup(x => x.UpdateAsync(taskItem))
            .ReturnsAsync(taskItem);

        // Act
        await _taskItemService.UpdateAsync(taskItem);

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepositoryMock.Verify(x => x.UpdateAsync(taskItem), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Success_Audit()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var atualTaskItem = _fixture.Build<TaskItem>()
            .With(x => x.ProjectId, project.Id)
            .With(x => x.DueDate, DateTime.Now.AddDays(10))
            .With(x => x.Title, "Antigo Titulo")
            .With(x => x.Description, "Antiga Descrição")
            .With(x => x.Status, (Status)1)
            .Create();

        var newTaskItem = _fixture.Build<TaskItem>()
            .With(x => x.Id, atualTaskItem.Id)
            .With(x => x.ProjectId, project.Id)
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.Title, "Novo Titulo")
            .With(x => x.Description, "Nova Descrição")
            .With(x => x.Status, (Status)2)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(atualTaskItem);

        _taskItemRepositoryMock.Setup(x => x.UpdateAsync(atualTaskItem))
            .ReturnsAsync(atualTaskItem);

        // Act
        await _taskItemService.UpdateAsync(newTaskItem);

        // Assert
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepositoryMock.Verify(x => x.UpdateAsync(atualTaskItem), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_TaskItemNotFoundError()
    {
        // Arrange
        var project = _fixture.Create<Project>();

        var taskItem = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, project.Id)
            .Create();

        _taskItemRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as TaskItem);

        _taskItemRepositoryMock.Setup(x => x.UpdateAsync(taskItem))
            .ReturnsAsync(taskItem);

        // Act
        BusinessException exception =
            await Assert.ThrowsAsync<TaskItemExceptions>(() => _taskItemService.UpdateAsync(taskItem));

        // Assert
        Assert.Equal(TaskItemExceptions.TaskItemNotFoundError.Message, exception.BusinessError.Message);
        _taskItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _taskItemRepositoryMock.Verify(x => x.UpdateAsync(taskItem), Times.Never);
    }
}
