namespace EclipseWorks.Domain.Models;

public class User : BaseModel
{
    public string Email { get; set; }

    public User()
    {
        Email = string.Empty;
    }

    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
