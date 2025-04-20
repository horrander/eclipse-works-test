namespace EclipseWorks.Application.Dtos.Request;

public class CreateProjectRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<CreateTaskItemRequest> Tasks { get; set; }
    public Guid UserId { get; set; }

    public CreateProjectRequest()
    {
        Title = string.Empty;
        Description = string.Empty;
        Tasks = [];
    }
}
