namespace EclipseWorks.Domain.Models;

public class User : BaseModel
{
    public string Email { get; set; }

    public User()
    {
        Email = string.Empty;
    }

    public User(string email)
    {
        Email = email;
    }

    public override void Validate()
    {
        return;
    }
}
