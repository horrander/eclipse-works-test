using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project> CreateAsync(Project project);
    Task<Project?> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>?> GetByUserIdAsync(Guid userId);
    Task Remove(Guid id);
}
