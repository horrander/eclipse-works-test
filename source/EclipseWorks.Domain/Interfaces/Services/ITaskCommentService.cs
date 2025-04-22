using System;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface ITaskCommentService
{
    /// <summary>
    /// Create a task comment and a associate a task
    /// </summary>
    /// <param name="taskComment">Task comment</param>
    /// <returns></returns>
    Task<TaskComment> CreateAsync(TaskComment taskComment);
}
