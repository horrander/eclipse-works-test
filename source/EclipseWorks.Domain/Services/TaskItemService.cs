using System;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly ILogger<TaskItemService> _logger;

    public TaskItemService(ITaskItemRepository taskItemRepository,
        ILogger<TaskItemService> logger)
    {
        _taskItemRepository = taskItemRepository ??
            throw new ArgumentNullException(nameof(taskItemRepository));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TaskItem> CreateAsync(Project project, TaskItem task)
    {
        _logger.LogInformation("Adicionando Task {} ao Projeto {}", task.Title, project.Title);

        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(project);

        task.Validate();

        project.ValidateMaxNumberOfTasks(task);

        await _taskItemRepository.CreateAsync(task);

        _logger.LogInformation("Task adicionada com sucesso");

        return task;
    }

    public async Task<TaskItem> GetById(Guid taskId)
    {
        _logger.LogInformation("Obtendo a task com Id {}", taskId);

        var taskItem = await _taskItemRepository.GetByIdAsync(taskId);

        if (taskItem == null)
        {
            throw new TaskItemExceptions(TaskItemExceptions.TaskItemNotFoundError);
        }

        _logger.LogInformation("Task localizada");

        return taskItem;
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
    {
        _logger.LogInformation("Obtendo todos as Tasks do projeto Id {}", projectId);

        var tasks = await _taskItemRepository.GetByProjectIdAsync(projectId);

        if (!tasks.Any())
        {
            _logger.LogInformation("Nenhuma tarefa localizada para o projeto informado");
        }

        return tasks;
    }

    public async Task RemoveFromProjectAsync(Guid taskId)
    {
        _logger.LogInformation("Removendo task {}", taskId);

        var task = await GetById(taskId);

        await _taskItemRepository.RemoveFromProjectAsync(task);

        _logger.LogInformation("Task removida com sucesso");
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        _logger.LogInformation("Atualizando a Task {}", task.Id);

        ArgumentNullException.ThrowIfNull(task);

        var atualTask = await _taskItemRepository.GetByIdAsync(task.Id);

        if (atualTask == null)
        {
            throw new TaskItemExceptions(TaskItemExceptions.TaskItemNotFoundError);
        }

        atualTask.UpdateAllowedProperties(task);

        await _taskItemRepository.UpdateAsync(atualTask);

        _logger.LogInformation("Task Atualizada com sucesso");

        return atualTask;
    }
}
