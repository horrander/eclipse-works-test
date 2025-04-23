using EclipseWorks.Domain.Exceptions;

namespace EclipseWorks.Domain.Models;

public class TaskComment : BaseModel
{
    public string Comment { get; set; }
    public Guid TaskItemId { get; set; }
    public Guid UserId { get; set; }

    public TaskComment()
    {
        Comment = string.Empty;
    }

    public override void Validate()
    {
        if (string.IsNullOrEmpty(Comment))
        {
            throw new TaskCommentExceptions(TaskCommentExceptions.CommentInvalidError);
        }
        else if (Guid.Empty.Equals(TaskItemId))
        {
            throw new TaskCommentExceptions(TaskCommentExceptions.CommentInvalidTaskItemError);
        }
        else if (Guid.Empty.Equals(UserId))
        {
            throw new TaskCommentExceptions(TaskCommentExceptions.CommentInvalidUserError);
        }
    }
}
