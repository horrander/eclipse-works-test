namespace EclipseWorks.Application.Dtos.Response;

public class AuditResponse
{
    public DateTime ModifiedAt { get; set; }
    public string Property { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public Guid TaskItemId { get; set; }

    public AuditResponse()
    {
        Property = string.Empty;
        OldValue = string.Empty;
        NewValue = string.Empty;
    }
}
