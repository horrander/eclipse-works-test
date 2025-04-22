namespace EclipseWorks.Application.Dtos.Response;

public class TaskItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public IEnumerable<TaskCommentResponse>? Comments { get; set; }

    public TaskItemResponse()
    {
        Title = string.Empty;
    }
}
