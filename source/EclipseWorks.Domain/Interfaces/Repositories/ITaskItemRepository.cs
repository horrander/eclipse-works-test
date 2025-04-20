using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Repositories;

public interface ITaskItemRepository
{
    Task<TaskItem> CreateAsync(TaskItem task);

    Task<TaskItem> UpdateAsync(TaskItem task);

    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);

    Task RemoveFromProjectAsync(TaskItem taskItem);

    Task<TaskItem?> GetById(Guid id);
}
