using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Repositories;

public interface ITaskCommentRepository
{
    Task CreateAsync(TaskComment taskComment);
}
