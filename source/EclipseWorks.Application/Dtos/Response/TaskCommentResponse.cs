using System;

namespace EclipseWorks.Application.Dtos.Response;

public class TaskCommentResponse
{
    public DateTime CreatedAt { get; set; }
    public string Comment { get; set; }
    public Guid UserId { get; set; }

    public TaskCommentResponse()
    {
        Comment = string.Empty;
    }
}
