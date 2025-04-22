using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;
using Microsoft.Extensions.Logging;

namespace EclipseWorks.Domain.Services;

public class TaskCommentService : ITaskCommentService
{
    private readonly ITaskCommentRepository _taskCommentRepository;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<TaskCommentService> _logger;

    public TaskCommentService(ITaskCommentRepository taskCommentRepository,
        ITaskItemRepository taskItemRepository,
        IUserRepository userRepository,
        ILogger<TaskCommentService> logger)
    {
        _taskCommentRepository = taskCommentRepository ??
            throw new ArgumentNullException(nameof(taskCommentRepository));

        _taskItemRepository = taskItemRepository ??
            throw new ArgumentNullException(nameof(taskItemRepository));

        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));

        _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TaskComment> CreateAsync(TaskComment taskComment)
    {
        _logger.LogInformation("Iniciando criação de comentário para a tarefa {}", taskComment.TaskItemId);

        ArgumentNullException.ThrowIfNull(taskComment);

        taskComment.Validate();

        await ValidateUser(taskComment.UserId);

        await ValidateTask(taskComment.TaskItemId);

        await _taskCommentRepository.CreateAsync(taskComment);

        _logger.LogInformation("Comentário criado com sucesso");

        return taskComment;
    }

    /// <summary>
    /// Valida se o id do usuário informado pertence a um usuário válido
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <exception cref="TaskCommentExceptions"></exception> 
    private async Task ValidateUser(Guid userId)
    {
        _logger.LogInformation("Validando se o usuário informado está valido - User Id: {}", userId);

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new TaskCommentExceptions(TaskCommentExceptions.CommentInvalidUserError);
        }

        _logger.LogInformation("Usuário válido");
    }

    /// <summary>
    /// Valida se o id da task informado pertence a uma task válida
    /// </summary>
    /// <param name="taskId">Task Id</param>
    /// <exception cref="TaskCommentExceptions"></exception> 
    private async Task ValidateTask(Guid taskId)
    {
        _logger.LogInformation("Validando se a task informada está valida - Task Id: {}", taskId);

        var taskItem = await _taskItemRepository.GetByIdAsync(taskId);

        if (taskItem == null)
        {
            throw new TaskCommentExceptions(TaskCommentExceptions.CommentInvalidTaskItemError);
        }

        _logger.LogInformation("Task valida");
    }
}
