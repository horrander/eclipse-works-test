namespace EclipseWorks.Application.Dtos.Request;

public class UpdateTaskItemRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Status { get; set; }
}
