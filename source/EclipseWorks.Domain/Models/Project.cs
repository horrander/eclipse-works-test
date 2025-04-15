namespace EclipseWorks.Domain.Models;

public class Project : BaseModel
{
    public IList<Task> Tasks { get; set; }
    public User User { get; set; }

    public Project()
    {
        Tasks = new List<Task>();
        User = new User();
    }

    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
