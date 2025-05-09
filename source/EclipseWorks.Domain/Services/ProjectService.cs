using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository projectRepository,
        ITaskItemRepository taskItemRepository,
        ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository ??
            throw new ArgumentNullException(nameof(projectRepository));

        _taskItemRepository = taskItemRepository ??
            throw new ArgumentNullException(nameof(taskItemRepository));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Project> CreateAsync(Project project, User user)
    {
        _logger.LogInformation("Iniciando criação de novo projeto: {title}", project.Title);

        ArgumentNullException.ThrowIfNull(project);

        project.Validate();

        project.AssociateUserToProject(user);

        await _projectRepository.CreateAsync(project);

        _logger.LogInformation("Projeto criado com sucesso");

        return project;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        _logger.LogInformation("Obtendo todos os projetos");

        return await _projectRepository.GetAllAsync();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Obtendo projeto com Id {}", id);

        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
        {
            _logger.LogError("Projeto não encontrado");

            throw new ProjectExceptions(ProjectExceptions.ProjectNotFoundError);
        }

        _logger.LogInformation("Projeto localizado com sucesso");

        return project;
    }

    public async Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId)
    {
        _logger.LogInformation("Obtendo dos os projetos do usuário {}", userId);

        var projects = await _projectRepository.GetByUserIdAsync(userId);

        if (projects == null || !projects.Any())
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectNotFoundForUserError);
        }

        return projects;
    }

    public async Task RemoveAsync(Guid projectId)
    {
        _logger.LogInformation("Removendo projeto com Id {}", projectId);

        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectNotFoundError);
        }

        _logger.LogInformation("Validando se o projeto possui tasks em andamento");

        project.ValidateTasksStatusBeforeRemove();

        await _projectRepository.RemoveAsync(project);

        await _taskItemRepository.RemoveAllFromProjectAsync(project.Tasks);

        _logger.LogInformation("Projeto e suas tasks removidos com sucesso");
    }
}
