namespace EclipseWorks.Application.Dtos.Request;

public class CreateTaskCommentRequest
{
    public string Comment { get; set; }
    public Guid TaskItemId { get; set; }
    public Guid UserId { get; set; }

    public CreateTaskCommentRequest()
    {
        Comment = string.Empty;
    }
}
