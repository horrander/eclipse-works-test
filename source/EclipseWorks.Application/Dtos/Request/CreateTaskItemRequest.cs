namespace EclipseWorks.Application.Dtos.Request;

public class CreateTaskItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public Guid ProjectId { get; set; }
}
