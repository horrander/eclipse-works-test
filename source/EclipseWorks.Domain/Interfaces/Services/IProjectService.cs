using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface IProjectService
{
    /// <summary>
    /// Obtem todos os projetos 
    /// </summary>
    /// <returns>Lista de Projetos</returns> 
    Task<IEnumerable<Project>> GetAllAsync();

    /// <summary>
    /// Cria um novo projeto
    /// </summary>
    /// <param name="project">Projeto</param>
    Task<Project> CreateAsync(Project project, User user);

    /// <summary>
    /// Get a project by id
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <returns>Project</returns>
    Task<Project> GetByIdAsync(Guid id);

    /// <summary>
    /// Get all projects of a specific User
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <returns>List of projects</returns>
    Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId);
}
