using System;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Services;

public interface ITaskItemService
{
    /// <summary>
    /// Create a new Task Item
    /// </summary>
    /// <param name="task">Task</param>
    /// <returns>New Task</returns> 
    Task<TaskItem> CreateAsync(Project project, TaskItem task);

    /// <summary>
    /// Update a existing Task
    /// </summary>
    /// <param name="task">Task</param>
    /// <returns>Existing Task</returns>
    Task<TaskItem> UpdateAsync(TaskItem task);

    /// <summary>
    /// Get a list of task from a project
    /// </summary>
    /// <param name="projectId">Project Id</param>
    /// <returns>List of Tasks</returns>
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);

    /// <summary>
    /// Remove a Task from a project
    /// </summary>
    /// <param name="projectId">Project Id</param>
    /// <returns></returns>
    Task RemoveFromProjectAsync(Guid projectId);

    /// <summary>
    /// Get a sigle Task by Id
    /// </summary>
    /// <param name="id">Task Id</param>
    /// <returns>Task</returns>
    Task<TaskItem> GetById(Guid id);
}
