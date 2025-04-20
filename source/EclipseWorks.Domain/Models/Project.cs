using EclipseWorks.Domain.Exceptions;

namespace EclipseWorks.Domain.Models;

public class Project : BaseModel
{
    private const int MaxNumerOfTasks = 20;

    public string Title { get; set; }
    public string Description { get; set; }
    public IList<TaskItem> Tasks { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }

    public Project()
    {
        Title = "Novo Projeto";
        Description = string.Empty;
        Tasks = new List<TaskItem>();
        User = new User();
    }

    public override void Validate()
    {
        if (!Tasks.Any())
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectWithoutTaskError);
        }

        if (Tasks.Count > MaxNumerOfTasks)
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectToManyTasksError);
        }

        User.Validate();

        foreach (var task in Tasks)
        {
            task.Validate();
        }
    }

    /// <summary>
    /// Validates and associates the user with the project
    /// </summary>
    /// <param name="user">User</param>
    public void AssociateUserToProject(User user)
    {
        user.Validate();

        if (UserId != user.Id)
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectIncorrectUserError);
        }

        User = user;
    }

    /// <summary>
    /// Validate if the number of tasks in a Project
    /// </summary>
    /// <param name="taskItem">Task item</param>
    /// <exception cref="ProjectExceptions"></exception> 
    public void ValidateMaxNumberOfTasks(TaskItem taskItem)
    {
        Tasks.Add(taskItem);

        if (Tasks.Count > MaxNumerOfTasks)
        {
            throw new ProjectExceptions(ProjectExceptions.ProjectToManyTasksError);
        }
    }
}
