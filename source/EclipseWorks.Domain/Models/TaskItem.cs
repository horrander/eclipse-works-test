using EclipseWorks.Domain.Enuns;
using EclipseWorks.Domain.Exceptions;

namespace EclipseWorks.Domain.Models;

public class TaskItem : BaseModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public Guid ProjectId { get; set; }
    public IEnumerable<TaskComment>? Comments { get; set; }

    public TaskItem()
    {
        Title = "Nova Tarefa";
        Status = Status.Pending;
        DueDate = DateTime.Now.AddDays(30);
        Priority = Priority.Medium;
    }

    public override void Validate()
    {
        if (DueDate <= DateTime.Now)
        {
            throw new TaskItemExceptions(TaskItemExceptions.TaskItemInvalidDueDateError);
        }

        if (Id != Guid.Empty && ProjectId == Guid.Empty)
        {
            throw new TaskItemExceptions(TaskItemExceptions.TaskItemInvalidProjectError);
        }
    }

    /// <summary>
    /// Update only the allowed properties
    /// </summary>
    /// <param name="newTask">New Task</param>
    public void UpdateAllowedProperties(TaskItem newTask)
    {
        Title = newTask.Title;
        Description = newTask.Description;
        DueDate = newTask.DueDate;
        Status = newTask.Status;
    }
}
