namespace EclipseWorks.Domain.Exceptions;

public class TaskItemExceptions : BusinessException
{
    public TaskItemExceptions(BusinessError error) : base(error) { }

    public static readonly BusinessError TaskItemInvalidDueDateError =
        new("TaskItemInvalidDueDateError", "Data de vencimento da tarefa está inválida ou é menor que a data atual");

    public static readonly BusinessError TaskItemNotFoundError =
        new("TaskItemNotFoundError", "Task não encontrada");

    public static readonly BusinessError TaskItemInvalidProjectError =
        new("TaskItemInvalidProjectError", "Task está associada a um projeto invalido");
}
