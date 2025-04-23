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
    public IList<Audit> Audits { get; set; }

    public TaskItem()
    {
        Title = "Nova Tarefa";
        Description = string.Empty;
        Status = Status.Pending;
        DueDate = DateTime.Now.AddDays(30);
        Priority = Priority.Medium;
        Audits = new List<Audit>();
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
        if (Title != newTask.Title)
        {
            Audits.Add(new(nameof(Title),
                Title,
                newTask.Title,
                Id));

            Title = newTask.Title;
        }
        if (Description != newTask.Description)
        {
            Audits.Add(new(nameof(Description),
                Description ?? string.Empty,
                newTask.Description ?? string.Empty,
                Id));

            Description = newTask.Description;
        }
        if (DueDate != newTask.DueDate)
        {
            Audits.Add(new(nameof(DueDate),
                DueDate?.ToLongDateString() ?? string.Empty,
                newTask.DueDate?.ToLongDateString() ?? string.Empty,
                Id));

            DueDate = newTask.DueDate;
        }
        if (Status != newTask.Status)
        {
            Audits.Add(new(nameof(Status),
                Status.ToString(),
                newTask.Status.ToString(),
                Id));

            Status = newTask.Status;
        }
    }
}
