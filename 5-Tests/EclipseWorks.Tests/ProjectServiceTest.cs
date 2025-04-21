using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using EclipseWorks.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EclipseWorks.Domain.Services.Tests
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<ILogger<ProjectService>> _loggerMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _loggerMock = new Mock<ILogger<ProjectService>>();
            _projectService = new ProjectService(_projectRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProject_WhenProjectExists()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var expectedProject = new Project { Id = projectId, Title = "Test Project" };

            _projectRepositoryMock
                .Setup(repo => repo.GetByIdAsync(projectId))
                .ReturnsAsync(expectedProject);

            // Act
            var result = await _projectService.GetByIdAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProject.Id, result.Id);
            Assert.Equal(expectedProject.Title, result.Title);

            _loggerMock.Verify(
                logger => logger.LogInformation(It.Is<string>(msg => msg.Contains("Obtendo projeto com Id")), projectId),
                Times.Once);
            _loggerMock.Verify(
                logger => logger.LogInformation(It.Is<string>(msg => msg.Contains("Projeto localizado com sucesso"))),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            _projectRepositoryMock
                .Setup(repo => repo.GetByIdAsync(projectId))
                .ReturnsAsync((Project)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ProjectExceptions>(() => _projectService.GetByIdAsync(projectId));
            Assert.Equal((IEnumerable<char>?)ProjectExceptions.ProjectNotFoundError, exception.Message);

            _loggerMock.Verify(
                logger => logger.LogInformation(It.Is<string>(msg => msg.Contains("Obtendo projeto com Id")), projectId),
                Times.Once);
            _loggerMock.Verify(
                logger => logger.LogError(It.Is<string>(msg => msg.Contains("Projeto não encontrado"))),
                Times.Once);
        }
    }
}
