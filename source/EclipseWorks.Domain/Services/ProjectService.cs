using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository projectRepository,
        ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository ??
            throw new ArgumentNullException(nameof(projectRepository));

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
}
