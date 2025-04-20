namespace EclipseWorks.Domain.Models;

public class User : BaseModel
{
    public string Email { get; set; }

    public User()
    {
        Email = string.Empty;
    }

    public override void Validate()
    {
        if (string.IsNullOrEmpty(Email))
        {

        }
    }
}
