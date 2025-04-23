using System;
using AutoFixture;
using EclipseWorks.Domain.Enuns;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EclipseWorks.DomainServices.Tests;

public class ProjectServiceTest
{
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly Mock<ITaskItemRepository> _taskItemRepository;
    private readonly Mock<ILogger<ProjectService>> _logger;
    private readonly ProjectService _projectService;
    private readonly IFixture _fixture;

    public ProjectServiceTest()
    {
        _projectRepository = new Mock<IProjectRepository>();
        _taskItemRepository = new Mock<ITaskItemRepository>();
        _logger = new Mock<ILogger<ProjectService>>();
        _fixture = new Fixture();

        _projectService = new ProjectService(_projectRepository.Object, _taskItemRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task Create_Success()
    {
        // Arrange
        _projectRepository.Setup(x => x.CreateAsync(It.IsAny<Project>()))
            .ReturnsAsync(It.IsAny<Project>());

        var user = _fixture.Create<User>();

        var task = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, Guid.NewGuid)
            .Create();

        var project = _fixture.Build<Project>()
            .With(x => x.UserId, user.Id)
            .With(x => x.Tasks, [task])
            .Create();

        //Act
        await _projectService.CreateAsync(project, user);

        //Assert
        _projectRepository.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task Create_ProjectWithoutTaskError()
    {
        // Arrange
        _projectRepository.Setup(x => x.CreateAsync(It.IsAny<Project>()))
            .ReturnsAsync(It.IsAny<Project>());

        var user = _fixture.Create<User>();

        var project = _fixture.Build<Project>()
            .With(x => x.UserId, user.Id)
            .With(x => x.Tasks, [])
            .Create();

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.CreateAsync(project, user));

        Assert.Equal(ProjectExceptions.ProjectWithoutTaskError.Message, exception.BusinessError.Message);

        //Assert
        _projectRepository.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task Create_ProjectToManyTasksError()
    {
        // Arrange
        _projectRepository.Setup(x => x.CreateAsync(It.IsAny<Project>()))
            .ReturnsAsync(It.IsAny<Project>());

        var user = _fixture.Create<User>();

        var tasks = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, Guid.NewGuid)
            .CreateMany(21)
            .ToList();

        var project = _fixture.Build<Project>()
            .With(x => x.UserId, user.Id)
            .With(x => x.Tasks, tasks)
            .Create();

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.CreateAsync(project, user));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectToManyTasksError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task Create_ProjectIncorrectUserError()
    {
        // Arrange
        _projectRepository.Setup(x => x.CreateAsync(It.IsAny<Project>()))
            .ReturnsAsync(It.IsAny<Project>());

        var user = _fixture.Create<User>();

        var task = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, Guid.NewGuid)
            .Create();

        var project = _fixture.Build<Project>()
            .With(x => x.UserId, Guid.NewGuid)
            .With(x => x.Tasks, [task])
            .Create();

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.CreateAsync(project, user));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectIncorrectUserError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.CreateAsync(It.IsAny<Project>()), Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_Success()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Project>().AsEnumerable);

        //Act
        await _projectService.GetAllAsync();

        //Assert
        _projectRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Project());

        //Acte
        await _projectService.GetByIdAsync(It.IsAny<Guid>());

        //Assert
        _projectRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ProjectNotFoundError()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Project);

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.GetByIdAsync(It.IsAny<Guid>()));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectNotFoundError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserIdAsync_Success()
    {
        // Arrange
        var user = _fixture.Create<User>();

        var task = _fixture.Build<TaskItem>()
            .With(x => x.DueDate, DateTime.Now.AddDays(30))
            .With(x => x.ProjectId, Guid.NewGuid)
            .Create();

        var project = _fixture.Build<Project>()
            .With(x => x.UserId, user.Id)
            .With(x => x.Tasks, [task])
            .Create();

        _projectRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([project]);

        //Acte
        await _projectService.GetByUserIdAsync(Guid.NewGuid());

        //Assert
        _projectRepository.Verify(x => x.GetByUserIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserIdAsync_ProjectNotFoundForUserError()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Project>());

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.GetByUserIdAsync(It.IsAny<Guid>()));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectNotFoundForUserError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.GetByUserIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserIdAsync_ProjectNotFoundForUserErrorNull()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as IEnumerable<Project>);

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.GetByUserIdAsync(It.IsAny<Guid>()));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectNotFoundForUserError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.GetByUserIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Project());

        _projectRepository.Setup(x => x.RemoveAsync(It.IsAny<Project>()));

        _taskItemRepository.Setup(x => x.RemoveAllFromProjectAsync(It.IsAny<IEnumerable<TaskItem>>()));

        //Act
        await _projectService.RemoveAsync(It.IsAny<Guid>());

        //Assert
        _projectRepository.Verify(x => x.RemoveAsync(It.IsAny<Project>()), Times.Once);
        _taskItemRepository.Verify(x => x.RemoveAllFromProjectAsync(It.IsAny<IEnumerable<TaskItem>>()), Times.Once);
    }

    [Fact]
    public async Task Remove_ProjectNotFoundError()
    {
        // Arrange
        _projectRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Project);

        _projectRepository.Setup(x => x.RemoveAsync(It.IsAny<Project>()));

        _taskItemRepository.Setup(x => x.RemoveAllFromProjectAsync(It.IsAny<IEnumerable<TaskItem>>()));

        //Act
        BusinessException exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.RemoveAsync(It.IsAny<Guid>()));

        //Assert
        Assert.Equal(ProjectExceptions.ProjectNotFoundError.Message, exception.BusinessError.Message);
        _projectRepository.Verify(x => x.RemoveAsync(It.IsAny<Project>()), Times.Never);
        _taskItemRepository.Verify(x => x.RemoveAllFromProjectAsync(It.IsAny<IEnumerable<TaskItem>>()), Times.Never);
    }
}
