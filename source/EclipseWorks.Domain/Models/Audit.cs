namespace EclipseWorks.Domain.Models;

public class Audit
{
    public Guid Id { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Property { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public Guid TaskItemId { get; set; }

    public Audit(string property, string oldValue, string newValue, Guid taskItemId)
    {
        ModifiedAt = DateTime.Now;
        Property = property;
        OldValue = oldValue;
        NewValue = newValue;
        TaskItemId = taskItemId;
    }
}
