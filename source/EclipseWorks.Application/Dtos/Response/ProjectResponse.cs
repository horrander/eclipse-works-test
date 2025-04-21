using System;

namespace EclipseWorks.Application.Dtos.Response;

public class ProjectResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<TaskItemResponse> Tasks { get; set; }
    public Guid UserId { get; set; }

    public ProjectResponse()
    {
        Title = string.Empty;
        Description = string.Empty;
        Tasks = [];
    }
}
